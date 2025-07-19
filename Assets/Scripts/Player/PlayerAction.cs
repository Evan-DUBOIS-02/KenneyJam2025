using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{

    public KeyCode dropKey = KeyCode.E;
    public int collectibles = 0;
    public int playerID;
    SpawnElectricBall spawnElectricBall;
    
    private Animator _animator;
    private TowerManager _currentTower;
    
    public Sprite _emptySprite;
    public Sprite _fullSprite;
    public Image[] _powerUI;


    public AudioSource audioSourceBall;
    public AudioClip soundBall;

    public AudioSource audioSourceTower;
    public AudioClip soundTower;

    private void Start()
    {
        spawnElectricBall = GameObject.FindWithTag("BallManager").GetComponent<SpawnElectricBall>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(dropKey) && collectibles > 0 && _currentTower != null)
        {
            collectibles--;
            UpdateUI();
            _currentTower.IncreaseMinionToLunch();
            _animator.SetTrigger("Interact");
            audioSourceTower.PlayOneShot(soundTower);
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
}
