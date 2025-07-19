using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectricBall : MonoBehaviour
{
    [Header("ElectricBall")]
    public GameObject electricBallPrefab;

    [Header("Equilibrage")]
    public float timeBetweenSpawn;
    public float spawnTime;

    public float currentNumberOfElectricBall1;
    public float currentNumberOfElectricBall2;
    public float maxNumberOfElectricBall;

    [Header("Prop-Ecran")]
    public int xMin;
    public int xMax;
    public int zMin;
    public int yMax;

    [Header("Collider GameObjects")]
    public List<GameObject> borderTop; //indice 0 = borderTop du haut, incremente de haut en bas
    public List<GameObject> borderBot; //indice 0 = borderBot du haut, incremente de haut en bas

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetweenSpawn = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //JOUEUR 1
        if(currentNumberOfElectricBall1 < maxNumberOfElectricBall)
        {
            if(timeBetweenSpawn < 0)
            {
                float generateZ1 = Random.Range(zMin, yMax);
                float generateX1 = GenerateX(1, generateZ1);
                Instantiate(electricBallPrefab, new Vector3(generateX1, 0, generateZ1), Quaternion.identity);
                currentNumberOfElectricBall1 += 1;
                timeBetweenSpawn = spawnTime;
            }
            timeBetweenSpawn -= Time.deltaTime;
        }
    }

    public float GenerateX(int id, float zValue)
    {
        float generateX1 = Random.Range(xMin, xMax);
        switch (zValue)
        {
            case float z when (z < yMax && z > borderBot[0].transform.position.z):
                while (generateX1 < borderTop[0].transform.position.x)
                {
                    generateX1 = Random.Range(xMin, xMax);
                }
                break;

            case float z when (z < borderBot[0].transform.position.z && z > borderBot[1].transform.position.z):
                while (generateX1 < borderTop[1].transform.position.x)
                {
                    generateX1 = Random.Range(xMin, xMax);
                }
                break;

            case float z when (z < borderBot[1].transform.position.z && z > borderBot[2].transform.position.z):
                while (generateX1 < borderTop[2].transform.position.x)
                {
                    generateX1 = Random.Range(xMin, xMax);
                }
                break;

            case float z when (z < borderBot[2].transform.position.z && z > borderBot[3].transform.position.z):
                while (generateX1 < borderTop[3].transform.position.x)
                {
                    generateX1 = Random.Range(xMin, xMax);
                }
                break;

            case float z when (z < borderBot[3].transform.position.z && z > zMin):
                while (generateX1 < borderTop[4].transform.position.x)
                {
                    generateX1 = Random.Range(xMin, xMax);
                }
                break;

            default:
                break;
        }
        return generateX1;
    }
}
