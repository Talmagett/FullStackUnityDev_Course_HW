using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private string loadingSceneName;
    void Start()
    {
        SceneManager.LoadScene(loadingSceneName);
    }
}