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
                _bordersTop[i].transform.position.x - _bordersTop[i+1].transform.position.x
            );
        }
        
        CorrectPlayerPosition();
    }

    private void CorrectPlayerPosition()
    {
        // Check for player 1
        int nearestWallIndex = -1;
        float nearestWallDistance = Mathf.Infinity;
        
        for (int i = 0; i < _bordersTop.Count; i++)
        {
            float dir = (_player1.transform.position - _bordersTop[i].transform.position).x;
            if (dir > 0)
            {
                float dist = Vector3.Distance(_player1.transform.position, _bordersTop[i].transform.position);
                if (dist < nearestWallDistance)
                {
                    nearestWallDistance = dist;
                    nearestWallIndex = i;
                }
            }
        }

        if (nearestWallIndex != -1)
        {
            _player1.transform.position = new Vector3(
                _bordersTop[nearestWallIndex].transform.position.x - (_player1.transform.position - _bordersTop[nearestWallIndex].transform.position).x,
                _player1.transform.position.y,
                _player1.transform.position.z
            );
        }
        
        // Check for player 2
        nearestWallIndex = -1;
        nearestWallDistance = Mathf.Infinity;
        
        for (int i = 0; i < _bordersTop.Count; i++)
        {
            float dir = (_player2.transform.position - _bordersTop[i].transform.position).x;
            if (dir < 0)
            {
                float dist = Vector3.Distance(_player2.transform.position, _bordersTop[i].transform.position);
                if (dist < nearestWallDistance)
                {
                    nearestWallDistance = dist;
                    nearestWallIndex = i;
                }
            }
        }

        if (nearestWallIndex != -1)
        {
            _player2.transform.position = new Vector3(
                _bordersTop[nearestWallIndex].transform.position.x - (_player2.transform.position - _bordersTop[nearestWallIndex].transform.position).x,
                _player2.transform.position.y,
                _player2.transform.position.z
            );
        }
    }
}
