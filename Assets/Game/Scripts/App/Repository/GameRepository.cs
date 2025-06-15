using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
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
            await File.WriteAllTextAsync(GetVersionedPath(version), json);
            Debug.Log($"Saved version {version} locally");
            var success = await _client.Save(version, json);
            if (success)
                Debug.Log($"Saved version {version} remotely");
        }

        public async UniTask<Dictionary<string, string>> GetVersionedState(int version)
        {
            string path = GetVersionedPath(version);
            
            //Get remote state:
            Dictionary<string, string> remoteState;
            var (success, remoteJson) = await _client.Load(version);
            if (success)
            {
                remoteState = JsonConvert.DeserializeObject<Dictionary<string, string>>(remoteJson);
                Debug.Log($"Loaded version {version} remotely");
                return remoteState;
            }

            if (!File.Exists(path))
            {
                Debug.LogError($"Save file for version {version} not found.");
                return new Dictionary<string, string>();
            }

            string json = await File.ReadAllTextAsync(path);
            if (string.IsNullOrEmpty(json))
                return new Dictionary<string, string>();
            Debug.Log($"Loaded version {version} locally");

            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                ?? new Dictionary<string, string>();
        }

        public UniTask<Dictionary<string, string>> GetLastState()
        {
            return GetVersionedState(GetLastVersion());
        }
    }
}


//
// public Dictionary<string, string> GetState()
// {
//     if (!File.Exists(_filePath))
//         return new Dictionary<string, string>();
//
//     byte[] encryptedBytes = File.ReadAllBytes(_filePath);
//     byte[] bytes = AesEncryptor.Decrypt(encryptedBytes, _aesPassword, _aesSalt);
//     string json = Encoding.UTF8.GetString(bytes);
//     Debug.Log($"Loaded state: {json}");
//
//     var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
//     if (result == null)
//         return new Dictionary<string, string>();
//     
//     return result;
// }
//
// public void SetState(Dictionary<string, string> gameState)
// {
//     string json = JsonConvert.SerializeObject(gameState);
//     Debug.Log($"Save state: {json}");
//
//     byte[] bytes = Encoding.UTF8.GetBytes(json);
//     byte[] encryptedBytes = AesEncryptor.Encrypt(bytes, _aesPassword, _aesSalt);
//     File.WriteAllBytes(_filePath, encryptedBytes);
// }