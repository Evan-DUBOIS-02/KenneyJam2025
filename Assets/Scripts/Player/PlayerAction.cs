using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public KeyCode dropKey = KeyCode.E;
    public int collectibles = 0;
    public int playerID;
    SpawnElectricBall spawnElectricBall;

    public bool hasTowerUpgrade = false;
    
    private Animator _animator;
    private TowerManager _currentTower;
    
    public Sprite _emptySprite;
    public Sprite _fullSprite;
    public Image[] _powerUI;
    public GameObject NotReadyButton;
    public GameObject ReadyButton;
    public GameObject TutorialUI;

    public AudioSource audioSourceBall;
    public AudioClip soundBall;

    public AudioSource audioSourceTower;
    public AudioClip soundTower;
    
    [NonSerialized]
    public bool _isReady = false;

    private void Start()
    {
        spawnElectricBall = GameObject.FindWithTag("BallManager").GetComponent<SpawnElectricBall>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!GameManager.Instance._isAllPlayerReady && Input.GetKeyDown(dropKey))
        {
            _isReady = !_isReady;
            UpdateReadyUI(_isReady);
            GameManager.Instance.UpdatePlayerReady(playerID, _isReady);
        }
        else if (GameManager.Instance._gameStarted)
        {
            for (int i = 0; i < _powerUI.Length; i++)
            {
                _powerUI[i].gameObject.SetActive(true);
                ReadyButton.SetActive(false);
            }
            TutorialUI.SetActive(false);
        }
        
        if (Input.GetKeyDown(dropKey) && collectibles > 0 && _currentTower != null)
        {
            collectibles--;
            UpdateUI();
            _currentTower.IncreaseMinionToLunch();
            _animator.SetTrigger("Interact");
            audioSourceTower.PlayOneShot(soundTower);
            if(collectibles <= 0)
                _currentTower.DisplayInteract(false);
        }
    }

    public int maxCollectibles = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectibles") && collectibles < maxCollectibles)
        {
            Destroy(other.gameObject);
            spawnElectricBall.DecrementCurrentElectricBall(playerID);
            collectibles++;
            UpdateUI();
            audioSourceBall.PlayOneShot(soundBall);
        }
        else if (other.CompareTag("Tower"))
        {
            _currentTower = other.GetComponent<TowerManager>();
        }

        else if (other.CompareTag("UpgradeTower"))
        {
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tower"))
        {
            _currentTower = null;
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < collectibles; i++)
        {
            _powerUI[i].sprite = _fullSprite;
        }

        for (int i = collectibles; i < maxCollectibles; i++)
        {
            _powerUI[i].sprite = _emptySprite;
        }
    }

    private void UpdateReadyUI(bool ready)
    {
        ReadyButton.SetActive(ready);
        NotReadyButton.SetActive(!ready);
    }
}
