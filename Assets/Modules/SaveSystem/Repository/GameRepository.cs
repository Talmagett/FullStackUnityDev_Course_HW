using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace SampleGame.App
{
    public sealed class GameRepository : IGameRepository
    {
        private readonly string _filePath;
        private readonly string _aesPassword;
        private readonly byte[] _aesSalt;

        public GameRepository(string filePath, string aesPassword, byte[] aesSalt)
        {
            _filePath = filePath;
            _aesPassword = aesPassword;
            _aesSalt = aesSalt;
        }
        
        public Dictionary<string, string> GetState()
        {
            if (!File.Exists(_filePath))
                return new Dictionary<string, string>();

            byte[] bytes = File.ReadAllBytes(_filePath);
            //byte[] bytes = AesEncryptor.Decrypt(encryptedBytes, _aesPassword, _aesSalt);
            string json = Encoding.UTF8.GetString(bytes);
            Debug.Log($"Loaded state: {json}");

            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            if (result == null)
                return new Dictionary<string, string>();
            
            return result;
        }

        public void SetState(Dictionary<string, string> gameState)
        {
            string json = JsonConvert.SerializeObject(gameState);
            Debug.Log($"Save state: {json}");

            byte[] bytes = Encoding.UTF8.GetBytes(json);
            //byte[] encryptedBytes = AesEncryptor.Encrypt(bytes, _aesPassword, _aesSalt);
            File.WriteAllBytes(_filePath, bytes);
        }
    }
}