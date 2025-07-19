using TMPro;
using UnityEngine;

public class SpawnUpgrades : MonoBehaviour
{
    [Header("Upgrades")]
    public GameObject upgradeTowersPrefab;
    public float yAxis;
    public float scaleUTBoxCollider;

    [Header("UI Management")]
    public float timer = 5f;

    [Header("Equilibrage")]
    public float timeBetweenSpawnUpgradeTowers;
    public float timerUntilUTSpawn;
    public float spawnTimeUpgradeTowers;

    [Header("Prop-Ecran")]
    public int xMin;
    public int xMax;
    public int zMin;
    public int zMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetweenSpawnUpgradeTowers = spawnTimeUpgradeTowers;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBetweenSpawnUpgradeTowers < 0)
        {
            float generateZ1 = Random.Range(zMin, zMax);
            float generateX1 = GenerateX(1, generateZ1);
            GameObject UTPrefab = Instantiate(upgradeTowersPrefab, new Vector3(generateX1, yAxis, generateZ1), Quaternion.identity);
            Debug.Log("Instantiate");
            CountdownAndSpawnUT(UTPrefab);
            timeBetweenSpawnUpgradeTowers = spawnTimeUpgradeTowers;
        }
        timeBetweenSpawnUpgradeTowers -= Time.deltaTime;
    }

    public float GenerateX(int id, float zValue)
    {
        float generateX = 0.0f;

        generateX = Random.Range(xMin, xMax);
        while (Physics.CheckBox(new Vector3(generateX, yAxis, zValue), new Vector3(scaleUTBoxCollider, scaleUTBoxCollider, scaleUTBoxCollider)))
        {
            generateX = Random.Range(xMin, xMax);
        }

        return generateX;
    }

    public void CountdownAndSpawnUT(GameObject UTPrefab)
    {
        Debug.Log("Get in fct");
        string textTimerUT = UTPrefab.GetComponent<TextMeshProUGUI>().text;
        while (timerUntilUTSpawn < 0)
        {
            timerUntilUTSpawn -= Time.deltaTime;
            textTimerUT = Mathf.Round(timer).ToString();
            Debug.Log("While");
        }
        UTPrefab.GetComponent<Collider>().enabled = true;
    }
}
