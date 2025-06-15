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

        public GameRepository(GameClient client, string filePath)
        {
            _client = client;
            _filePath = filePath;
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
            //await File.WriteAllTextAsync(_filePath, json); // optional: overwrite latest
            await _client.Save(json);
            Debug.Log($"Saved version {version}");
        }

        public async UniTask<Dictionary<string, string>> GetVersionedState(int version)
        {
            string path = GetVersionedPath(version);
            if (!File.Exists(path))
            {
                Debug.LogError($"Save file for version {version} not found.");
                return new Dictionary<string, string>();
            }

            string json = await File.ReadAllTextAsync(path);
            if (string.IsNullOrEmpty(json))
                return new Dictionary<string, string>();
            Debug.Log($"Loaded version {version}");
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
                ?? new Dictionary<string, string>();
        }

        public async UniTask<Dictionary<string, string>> GetState()
        {
            //Get local state:
            long localSaveTime = -1;
            Dictionary<string, string> localState;

            if (File.Exists(_filePath))
            {
                string json = await File.ReadAllTextAsync(_filePath);
                if (json == null)
                {
                    localState = new Dictionary<string, string>();
                }
                else
                {
                    localState = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    if (localState == null)
                    {
                        localState = new Dictionary<string, string>();
                    }
                    else
                    {
                        string saveTimeString = localState[SAVE_TIME_KEY];
                        localSaveTime = long.Parse(saveTimeString);
                    }
                }
            }
            else
            {
                localState = new Dictionary<string, string>();
            }

            //Get remote state:
            long remoteSaveTime = -1;
            Dictionary<string, string> remoteState;

            var (success, remoteJson) = await _client.Load();
            if (success)
            {
                remoteState = JsonConvert.DeserializeObject<Dictionary<string, string>>(remoteJson);
                if (remoteState != null)
                {
                    remoteSaveTime = long.Parse(remoteState[SAVE_TIME_KEY]);
                }
                else
                {
                    remoteState = new Dictionary<string, string>();
                }
            }
            else
            {
                remoteState = new Dictionary<string, string>();
            }


            //Compare:
            if (localSaveTime >= remoteSaveTime)
            {
                Debug.Log("Select local state");
                return localState;
            }
            else
            {
                Debug.Log("Select remote state");
                return remoteState;
            }
        }

        public UniTask<Dictionary<string, string>> GetLastState()
        {
            return GetVersionedState(GetLastVersion());
        }
    }
}

// UnityWebRequest request = UnityWebRequest.Get($"{_uri}/load");
// await request.SendWebRequest();
//
// if (request.result != UnityWebRequest.Result.Success)
//     return (false, null);
//
// string json = request.downloadHandler.text;
// var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json)
//                  ?? new Dictionary<string, string>();
// return (true, dictionary);


// private readonly string _filePath;
// private readonly string _aesPassword;
// private readonly byte[] _aesSalt;
//
// public GameRepository(string filePath, string aesPassword, byte[] aesSalt)
// {
//     _filePath = filePath;
//     _aesPassword = aesPassword;
//     _aesSalt = aesSalt;
// }
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