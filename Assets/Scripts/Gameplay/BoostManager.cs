using TMPro;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    [Header("UI Management")]
    private bool isInteractable = false;
    public float timerUntilUTSpawn;
    public TextMeshProUGUI textMeshProUGUI;
    private BoxCollider boxCollider;

    private void Start()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    public void Update()
    {
        if (!isInteractable)
        {
            if (timerUntilUTSpawn < 0.5)
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
        
    }
}
