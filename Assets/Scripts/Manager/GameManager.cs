using System;
using UnityEngine;

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
}