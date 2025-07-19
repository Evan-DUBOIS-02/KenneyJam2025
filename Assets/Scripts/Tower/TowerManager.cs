using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager: MonoBehaviour
{
    public WorldType _worldType;
    public Image _mask;
    public GameObject _interactUI;
    public string _nameScene;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _worldType == WorldType.Futurist)
        {
            IncreaseTerrain(1);
        }
    }

    public void IncreaseTerrain(int energieNumber)
    {
        if(_worldType == WorldType.Futurist)
            energieNumber = -energieNumber;
        
        _mask.fillAmount += (energieNumber*GameManager.Instance.PERCENT_RATIO);
        Debug.Log("Fill amount : " + _mask.fillAmount);
        if(_mask.fillAmount >= 0.9 || _mask.fillAmount <= 0.1)
            GameManager.Instance.EndGame(_nameScene);
        BorderGenerator.Instance.UpdateBorders();
    }

    private void DisplayInteract(bool show)
    {
        _interactUI.SetActive(show);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayInteract(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayInteract(false);
        }
    }
}