using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager: MonoBehaviour
{
    public WorldType _worldType;
    public Image _mask;
    public GameObject _interactUI;
    public string _nameScene;
    
    public GameObject _minionPrefab;
    private float minionCooldown = 0f;
    private float minionStartCooldown = 0.5f;
    private int numberOfMinionsToLunch = 0;

    private void Start()
    {
        minionCooldown = minionStartCooldown;
    }

    private void Update()
    {
        if(minionCooldown > 0)
            minionCooldown -= Time.deltaTime;
        else if (numberOfMinionsToLunch > 0)
            LunchMinion();
    }

    public void IncreaseTerrain(int energieNumber)
    {
        if(_worldType == WorldType.Futurist)
            energieNumber = -energieNumber;
        
        _mask.fillAmount += (energieNumber*GameManager.Instance.PERCENT_RATIO);
        if(_mask.fillAmount >= 0.9 || _mask.fillAmount <= 0.1)
            GameManager.Instance.EndGame(_nameScene);
        BorderGenerator.Instance.UpdateBorders();
    }

    public void IncreaseMinionToLunch()
    {
        numberOfMinionsToLunch++;
    }

    private void LunchMinion()
    {
        GameObject minion = null;
        if(_worldType ==  WorldType.Futurist)
            minion = Instantiate(_minionPrefab, transform.position, Quaternion.Euler(0, -90, 0));
        else
            minion = Instantiate(_minionPrefab, transform.position, Quaternion.Euler(0, 90, 0));
        
        minion.transform.SetParent(transform);
        minion.layer = gameObject.layer;
        minion.GetComponent<TowerMinion>().RegisterTower(this);
        minionCooldown = minionStartCooldown;
        numberOfMinionsToLunch--;
    }

    public void DisplayInteract(bool show)
    {
        _interactUI.SetActive(show);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(other.GetComponentInChildren<PlayerAction>().collectibles > 0)
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