using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

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

    [SerializeField]
    private GameObject _levelUpVFX;
    [SerializeField]
    private GameObject _levelDownVFX;


    // xp
    [Header("XP")]
    public int _numberOfOrbToLevelUp = 3;
    public float[] _levelMult;
    public GameObject[] _levelPrefab;
    public GameObject UI;
    [NonSerialized]
    public int _currentLevel = 0;
    private int _numberOfOrb = 0;
    public Image _imgFill;

    public AudioSource audioSourceBreakWall;
    public AudioClip soundBreakWall;

    public AudioSource audioSourceUpgrade;
    public AudioClip soundUpgrade;

    public AudioSource audioSourceDowngrade;
    public AudioClip soundDowngrade;

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
        audioSourceBreakWall.PlayOneShot(soundBreakWall);
        if (_worldType == WorldType.Futurist)
            energieNumber = -energieNumber;
        
        _mask.fillAmount += energieNumber*(GameManager.Instance.PERCENT_RATIO * _levelMult[_currentLevel]);
        if(_mask.fillAmount >= 0.9 || _mask.fillAmount <= 0.1)
            GameManager.Instance.EndGame(_nameScene);
        
        BorderGenerator.Instance.UpdateBorders();
    }

    public void IncreaseMinionToLunch()
    {
        numberOfMinionsToLunch++;
        
        _numberOfOrb++;
        _imgFill.fillAmount = _numberOfOrb/(float)_numberOfOrbToLevelUp;
        if (_numberOfOrb == _numberOfOrbToLevelUp && _currentLevel < _levelPrefab.Length)
        {
            UpgradeTower();
        }
    }

    public void UpgradeTower()
    {
        audioSourceUpgrade.PlayOneShot(soundUpgrade);
        _numberOfOrb = 0;
        _imgFill.fillAmount = 0f;
        if(_currentLevel == 0)
            GetComponent<MeshRenderer>().enabled = false;
        else
            _levelPrefab[_currentLevel-1].SetActive(false);
            
        _levelPrefab[_currentLevel].SetActive(true);
        _currentLevel++;
        Destroy(Instantiate(_levelUpVFX, transform.position, Quaternion.identity), 3f);
    }

    public void DowngradeTower()
    {
        audioSourceDowngrade.PlayOneShot(soundDowngrade);
        _numberOfOrb = 0;
        _imgFill.fillAmount = 0f;
        
        if (_currentLevel == 0)
            return;
        
        _levelPrefab[_currentLevel - 1].SetActive(false);
        
        _currentLevel--;
        if(_currentLevel == 0)
            GetComponent<MeshRenderer>().enabled = true;
        else
            _levelPrefab[_currentLevel - 1].SetActive(true);

        Destroy(Instantiate(_levelDownVFX, transform.position, Quaternion.identity), 3f);
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