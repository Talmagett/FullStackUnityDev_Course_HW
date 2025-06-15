using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SampleGame.App
{
    public sealed class GameClient
    {
        private readonly string _uri;
        private string _token;

        public GameClient(string uri)
        {
            _uri = uri;
        }

        public async UniTask<bool> Authorize()
        {
            UnityWebRequest request = UnityWebRequest.Get($"{_uri}/authorize");
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
                return false;

            string token = request.downloadHandler.text;
            if (string.IsNullOrEmpty(token))
                return false;

            _token = token;
            Debug.Log($"Authroized {token}");
            return true;
        }

        public async UniTask<bool> Save(string json)
        {
            if (string.IsNullOrEmpty(_token))
                return false;

            UnityWebRequest request = UnityWebRequest.Put($"{_uri}/save", json);
            request.SetRequestHeader("Authorization", _token);
      
            await request.SendWebRequest();
            return request.result == UnityWebRequest.Result.Success;
        }

        public async UniTask<(bool, string)> Load()
        {
            if (string.IsNullOrEmpty(_token))
                return (false, null);

            
            UnityWebRequest request = UnityWebRequest.Get($"{_uri}/load");
            request.SetRequestHeader("Authorization", _token);

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
                return (false, null);

            string json = request.downloadHandler.text;
            return json == null ? (false, null) : (true, json);
        }
    }
}