using System;
using TMPro;
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

    public TMP_Text _countDownText;
    public TMP_Text _countDownTextBg;
    
    private static GameManager _instance;

    [NonSerialized]
    public bool _gameStarted = false;
    [NonSerialized]
    public bool _isAllPlayerReady = false;
    private bool _player1Ready = false;
    private bool _player2Ready = false;
    private float _countDown = 4f;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if (_isAllPlayerReady)
        {
            if (_countDown > 0)
            {
                _countDown -= Time.deltaTime;
                _countDownText.text =  ((int)_countDown).ToString();
                _countDownTextBg.text = ((int)_countDown).ToString();
            }
            else
            {
                _gameStarted = true;
                _countDownText.text = "";
                _countDownTextBg.text = "";
            }
        }
    }

    public void UpdatePlayerReady(int id, bool ready)
    {
        if (id == 1)
            _player1Ready = ready;
        else if (id == 2)
            _player2Ready = ready;

        if (_player1Ready && _player2Ready)
            _isAllPlayerReady = true;
    }

    public void EndGame(string _nameScene)
    {
        SceneManager.LoadScene(_nameScene);
    }
}