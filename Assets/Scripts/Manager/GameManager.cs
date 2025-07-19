using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum WorldType
{
    Medieval,
    Futurist
}

public class GameManager: MonoBehaviour
{
    public float PERCENT_RATIO = 0.05f;
    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void EndGame(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene);
    }
}