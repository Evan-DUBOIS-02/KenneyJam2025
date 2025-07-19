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
    }
}
