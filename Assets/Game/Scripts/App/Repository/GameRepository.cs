using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using Modules.Ecryption;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class GameRepository : IGameRepository
    {
        private static readonly DateTime originTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        private const string SAVE_TIME_KEY = "SaveTime";

        private readonly GameClient _client;
        private readonly string _filePath;
        private const string VERSION_KEY = "CurrentSaveVersion";

        private readonly string _aesPassword;
        private readonly byte[] _aesSalt;

        public GameRepository(GameClient client, string filePath, string aesPassword, byte[] aesSalt)
        {
            _client = client;
            _filePath = filePath;
            _aesPassword = aesPassword;
            _aesSalt = aesSalt;
        }
        private string GetVersionedPath(int version) =>
            $"{Path.GetDirectoryName(_filePath)}/GameState_v{version}.json";

        private int GetLastVersion()
        {
            return PlayerPrefs.GetInt(VERSION_KEY, 0);
        }

        private void SetLastVersion(int version)
        {
            PlayerPrefs.SetInt(VERSION_KEY, version);
        }

        public async UniTask SetState(Dictionary<string, string> gameState)
        {
            int version = GetLastVersion() + 1;
            SetLastVersion(version);

            TimeSpan time = DateTime.Now.ToUniversalTime() - originTime;
            gameState[SAVE_TIME_KEY] = time.TotalSeconds.ToString("F0");

            string json = JsonConvert.SerializeObject(gameState);

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            byte[] encryptedBytes = AesEncryptor.Encrypt(bytes, _aesPassword, _aesSalt);
            string base64Encrypted = Convert.ToBase64String(encryptedBytes);

            await File.WriteAllTextAsync(GetVersionedPath(version), base64Encrypted);
            Debug.Log($"Saved version {version} locally");
            var success = await _client.Save(version, base64Encrypted);
            if (success)
                Debug.Log($"Saved version {version} remotely");
        }

        public async UniTask<Dictionary<string, string>> GetVersionedState(int version)
        {
            string path = GetVersionedPath(version);

            var (success, remoteEncryptedBase64) = await _client.Load(version);
            if (success)
            {
                try
                {
                    byte[] encryptedBytes = Convert.FromBase64String(remoteEncryptedBase64);
                    byte[] decryptedBytes = AesEncryptor.Decrypt(encryptedBytes, _aesPassword, _aesSalt);
                    string decryptedJson = Encoding.UTF8.GetString(decryptedBytes);

                    var remoteState = JsonConvert.DeserializeObject<Dictionary<string, string>>(decryptedJson);
                    Debug.Log($"Loaded version {version} remotely (AES)");
                    return remoteState ?? new Dictionary<string, string>();
                }
                catch (Exception e)
                {
                    Debug.LogError($"AES Decryption failed from remote: {e}");
                }
            }

            if (!File.Exists(path))
            {
                Debug.LogError($"Save file for version {version} not found.");
                return new Dictionary<string, string>();
            }

            try
            {
                string localBase64 = await File.ReadAllTextAsync(path);
                byte[] encryptedBytes = Convert.FromBase64String(localBase64);
                byte[] decryptedBytes = AesEncryptor.Decrypt(encryptedBytes, _aesPassword, _aesSalt);
                string decryptedJson = Encoding.UTF8.GetString(decryptedBytes);

                var localState = JsonConvert.DeserializeObject<Dictionary<string, string>>(decryptedJson);
                Debug.Log($"Loaded version {version} locally (AES)");
                return localState ?? new Dictionary<string, string>();
            }
            catch (Exception e)
            {
                Debug.LogError($"AES Decryption failed from local: {e}");
                return new Dictionary<string, string>();
            }
        }

        public UniTask<Dictionary<string, string>> GetLastState()
        {
            return GetVersionedState(GetLastVersion());
        }
    }
}