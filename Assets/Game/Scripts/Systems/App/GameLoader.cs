using Cysharp.Threading.Tasks;
using Game.App.Scene;
using UnityEngine;
using Zenject;
public class GameLoader : MonoBehaviour
{
    [Inject] private SceneNavigator sceneNavigator;

    private void Awake()
    {
        sceneNavigator.OpenMenu().Forget();
    }
}