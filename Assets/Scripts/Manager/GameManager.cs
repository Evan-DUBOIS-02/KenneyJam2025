using NUnit.Framework;
using System;
using System.Collections.Generic;
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

    [SerializeField]
    private TowerManager[] _towerPlayer1;
    [SerializeField]
    private TowerManager[] _towerPlayer2;

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

    public void DowngradeRandomTower(int idAttaquant)
    {
        if(idAttaquant == 1)
        {
            List<int> _possibleId = new List<int>();
            for(int i = 0; i < _towerPlayer2.Length; i++)
            {
                if( _towerPlayer2[i]._currentLevel != 0)
                {
                    _possibleId.Add(i);
                }
            }
            int id = _possibleId[UnityEngine.Random.Range(0, _possibleId.Count)];
            _towerPlayer2[id].DowngradeTower();
        }

        else if(idAttaquant == 2)
        {
            List<int> _possibleId = new List<int>();
            for (int i = 0; i < _towerPlayer1.Length; i++)
            {
                if (_towerPlayer1[i]._currentLevel != 0)
                {
                    _possibleId.Add(i);
                }
            }
            int id = _possibleId[UnityEngine.Random.Range(0, _possibleId.Count)];
            _towerPlayer1[id].DowngradeTower();
        }
    }
}