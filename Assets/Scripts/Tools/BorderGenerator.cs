using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorderGenerator : MonoBehaviour
{
    private static BorderGenerator _instance;
    public static BorderGenerator Instance
    {
        get { return _instance; }
    }
    
    public List<GameObject> _bordersTop;
    public List<GameObject> _bordersBot;
    public List<Image> _images;
    public GameObject _player1; // gauche
    public GameObject _player2; // droite

    private void Awake()
    {
        _instance = this;
    }

    public void UpdateBorders()
    {
        for (int i = 0; i < _images.Count; i++)
        {
            GameObject border = _bordersTop[i];
            border.transform.position = new Vector3(
                Mathf.Lerp(-96, 96, _images[i].fillAmount),
                border.transform.position.y,
                border.transform.position.z);
        }

        for (int i = 0; i < _bordersBot.Count; i++)
        {
            GameObject border = _bordersBot[i];
            border.transform.position = new Vector3(
                (_bordersTop[i].transform.position.x + _bordersTop[i+1].transform.position.x)/2,
                border.transform.position.y,
                border.transform.position.z
            );
            border.transform.localScale = new Vector3(
                border.transform.localScale.x,
                border.transform.localScale.y,
                _bordersTop[i].transform.position.x - _bordersTop[i+1].transform.position.x + 1.5f
            );
        }
        
        CorrectPlayerPosition();
    }

    private void CorrectPlayerPosition()
    {
        // Check for player 1
        float zValue = _player1.transform.position.z;
        float dir = 0f;
        switch (zValue)
        {
            case float z when (z < 47 && z > _bordersBot[0].transform.position.z):
                dir = (_player1.transform.position - _bordersTop[0].transform.position).x;
                if (dir > 0)
                {
                    _player1.transform.position = new Vector3(
                        _bordersTop[0].transform.position.x - (_player1.transform.position - _bordersTop[0].transform.position).x,
                        _player1.transform.position.y,
                        _player1.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[0].transform.position.z && z > _bordersBot[1].transform.position.z):
                dir = (_player1.transform.position - _bordersTop[1].transform.position).x;
                if (dir > 0)
                {
                    _player1.transform.position = new Vector3(
                        _bordersTop[1].transform.position.x - (_player1.transform.position - _bordersTop[1].transform.position).x,
                        _player1.transform.position.y,
                        _player1.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[1].transform.position.z && z > _bordersBot[2].transform.position.z):
                dir = (_player1.transform.position - _bordersTop[2].transform.position).x;
                if (dir > 0)
                {
                    _player1.transform.position = new Vector3(
                        _bordersTop[2].transform.position.x - (_player1.transform.position - _bordersTop[2].transform.position).x,
                        _player1.transform.position.y,
                        _player1.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[2].transform.position.z && z > _bordersBot[3].transform.position.z):
                dir = (_player1.transform.position - _bordersTop[3].transform.position).x;
                if (dir > 0)
                {
                    _player1.transform.position = new Vector3(
                        _bordersTop[3].transform.position.x - (_player1.transform.position - _bordersTop[3].transform.position).x,
                        _player1.transform.position.y,
                        _player1.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[3].transform.position.z && z > -47):
                dir = (_player1.transform.position - _bordersTop[4].transform.position).x;
                if (dir > 0)
                {
                    _player1.transform.position = new Vector3(
                        _bordersTop[4].transform.position.x - (_player1.transform.position - _bordersTop[4].transform.position).x,
                        _player1.transform.position.y,
                        _player1.transform.position.z
                    );
                }
                break;
        }
        // Check for player 2
        zValue = _player2.transform.position.z;
        dir = 0f;
        
        switch (zValue)
        {
            case float z when (z < 47 && z > _bordersBot[0].transform.position.z):
                dir = (_player2.transform.position - _bordersTop[0].transform.position).x;
                if (dir < 0)
                {
                    _player2.transform.position = new Vector3(
                        _bordersTop[0].transform.position.x - (_player2.transform.position - _bordersTop[0].transform.position).x,
                        _player2.transform.position.y,
                        _player2.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[0].transform.position.z && z > _bordersBot[1].transform.position.z):
                dir = (_player2.transform.position - _bordersTop[1].transform.position).x;
                if (dir < 0)
                {
                    _player2.transform.position = new Vector3(
                        _bordersTop[1].transform.position.x - (_player2.transform.position - _bordersTop[1].transform.position).x,
                        _player2.transform.position.y,
                        _player2.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[1].transform.position.z && z > _bordersBot[2].transform.position.z):
                dir = (_player2.transform.position - _bordersTop[2].transform.position).x;
                if (dir < 0)
                {
                    _player2.transform.position = new Vector3(
                        _bordersTop[2].transform.position.x - (_player2.transform.position - _bordersTop[2].transform.position).x,
                        _player2.transform.position.y,
                        _player2.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[2].transform.position.z && z > _bordersBot[3].transform.position.z):
                dir = (_player2.transform.position - _bordersTop[3].transform.position).x;
                if (dir < 0)
                {
                    _player2.transform.position = new Vector3(
                        _bordersTop[3].transform.position.x - (_player2.transform.position - _bordersTop[3].transform.position).x,
                        _player2.transform.position.y,
                        _player2.transform.position.z
                    );
                }
                break;
            case float z when (z < _bordersBot[3].transform.position.z && z > -47):
                dir = (_player2.transform.position - _bordersTop[4].transform.position).x;
                if (dir < 0)
                {
                    _player2.transform.position = new Vector3(
                        _bordersTop[4].transform.position.x - (_player2.transform.position - _bordersTop[4].transform.position).x,
                        _player2.transform.position.y,
                        _player2.transform.position.z
                    );
                }
                break;
        }
    }
}
