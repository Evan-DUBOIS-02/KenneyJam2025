using TMPro;
using UnityEngine;

public class UpgradeTowerManager : MonoBehaviour
{

    [Header("UI Management")]
    private bool isInteractable = false;
    public float timerUntilUTSpawn;
    public TextMeshProUGUI textMeshProUGUI;
    public BoxCollider boxCollider;

    private void Start()
    {
    }

    public void Update()
    {
        if (!isInteractable)
        {
            if (timerUntilUTSpawn < 0)
            {
                boxCollider.enabled = true;
                //couleur/transparence?
                textMeshProUGUI.text = "";
                isInteractable = true;
            }
            else
            {
                timerUntilUTSpawn -= Time.deltaTime;
                textMeshProUGUI.text = Mathf.Round(timerUntilUTSpawn).ToString();
            }
        }
        else
            Debug.Log("fini");
        
    }
}
