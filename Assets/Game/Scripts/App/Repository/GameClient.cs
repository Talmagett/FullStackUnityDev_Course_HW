using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SampleGame.App
{
    public sealed class GameClient
    {
        private readonly string _uri;

        public GameClient(string uri)
        {
            _uri = uri;
        }

        public async UniTask<bool> Save(int version, string json)
        {
            UnityWebRequest request = UnityWebRequest.Put($"{_uri}/save?version={version}", json);
            //request.SetRequestHeader("Authorization", _token);
            try
            {
                await request.SendWebRequest();
                return request.result == UnityWebRequest.Result.Success;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async UniTask<(bool, string)> Load(int version)
        {
            UnityWebRequest request = UnityWebRequest.Get($"{_uri}/load?version={version}");
            //request.SetRequestHeader("Authorization", _token);
            try
            {
                await request.SendWebRequest();
                if (request.result != UnityWebRequest.Result.Success)
                    return (false, null);
                if(request.responseCode != 200)
                    return (false, null);
                string json = request.downloadHandler.text;
                return json == null ? (false, null) : (true, json);
            }
            catch (Exception e)
            {
                return (false, null);
            }
        }
    }
}