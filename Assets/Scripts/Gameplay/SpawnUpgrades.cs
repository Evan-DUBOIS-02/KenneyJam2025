using TMPro;
using UnityEngine;

public class SpawnUpgrades : MonoBehaviour
{
    [Header("Upgrades")]
    public GameObject[] upgradeBoostPrefabs;
    public float yAxis;
    public float scaleUTBoxCollider;

    [Header("Equilibrage")]
    public float timeBetweenSpawnUpgradeTowers;
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
        if(GameManager.Instance._gameStarted == false)
            return;
        
        if (timeBetweenSpawnUpgradeTowers < 0)
        {
            float generateZ1 = Random.Range(zMin, zMax);
            float generateX1 = GenerateX(1, generateZ1);
            int randomUpgrade = Random.Range(0, upgradeBoostPrefabs.Length);
            Instantiate(upgradeBoostPrefabs[randomUpgrade], new Vector3(generateX1, yAxis, generateZ1), Quaternion.identity);
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
}
