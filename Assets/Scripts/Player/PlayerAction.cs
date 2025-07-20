using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public KeyCode dropKey = KeyCode.E;
    public int collectibles = 0;
    public int playerID;
    public float speedBoost;
    public float baseSpeed;
    public float speedBoostTime;
    public float freezeBoostTime;
    SpawnElectricBall spawnElectricBall;

    public bool hasTowerUpgrade = false;
    public GameObject _otherPlayerSnowman;
    public GameObject _otherPlayerBaseModel;


    [Header("Animation")]    
    private Animator _animator;
    private TowerManager _currentTower;
    private PlayerMouvement _playerMouvement;

    [SerializeField]
    private PlayerMouvement _otherPlayerMovement;

    [Header("UI")]
    public Sprite _emptySprite;
    public Sprite _fullSprite;
    public Image[] _powerUI;
    public GameObject NotReadyButton;
    public GameObject ReadyButton;
    public GameObject TutorialUI;


    [Header("Audio")]
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
        _playerMouvement = GetComponent<PlayerMouvement>();
        baseSpeed = _playerMouvement.speed;
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

        else if (other.CompareTag("BoostSpeed"))
        {
            Destroy(other.gameObject);
            StartCoroutine(TimeWithMoreSpeed());
        }

        else if(other.CompareTag("BoostFreeze"))
        {
            Destroy(other.gameObject);
            StartCoroutine(TimeFreezeOtherPlayer());
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

    private IEnumerator TimeWithMoreSpeed()
    {
        _playerMouvement.speed = speedBoost;
        yield return new WaitForSeconds(speedBoostTime);
        _playerMouvement.speed = baseSpeed;
    }

    private IEnumerator TimeFreezeOtherPlayer()
    {
        _otherPlayerBaseModel.SetActive(false);
        _otherPlayerSnowman.SetActive(true);
        _otherPlayerMovement.speed = 0;
        _otherPlayerMovement.isStun = true;
        yield return new WaitForSeconds(freezeBoostTime);
        _otherPlayerBaseModel.SetActive(true);
        _otherPlayerSnowman.SetActive(false);
        _otherPlayerMovement.speed = baseSpeed;
        _otherPlayerMovement.isStun = false;
    }
}
