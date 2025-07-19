using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectricBall : MonoBehaviour
{
    [Header("ElectricBall")]
    public GameObject electricBallPrefab;

    [Header("Equilibrage")]
    public float timeBetweenSpawn1;
    public float timeBetweenSpawn2;
    public float spawnTime;

    public float currentNumberOfElectricBall1;
    public float currentNumberOfElectricBall2;
    public float maxNumberOfElectricBall;

    [Header("Prop-Ecran")]
    public int xMin;
    public int xMax;
    public int zMin;
    public int zMax;

    [Header("Collider GameObjects")]
    public List<GameObject> borderTop; //indice 0 = borderTop du haut, incremente de haut en bas
    public List<GameObject> borderBot; //indice 0 = borderBot du haut, incremente de haut en bas

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetweenSpawn1 = spawnTime;
        timeBetweenSpawn2 = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        //JOUEUR 1 (LEFT)
        if(currentNumberOfElectricBall1 < maxNumberOfElectricBall)
        {
            if(timeBetweenSpawn1 < 0)
            {
                float generateZ1 = Random.Range(zMin, zMax);
                Debug.Log("Generate 1");
                float generateX1 = GenerateX(1, generateZ1);
                Instantiate(electricBallPrefab, new Vector3(generateX1, 0, generateZ1), Quaternion.identity);
                currentNumberOfElectricBall1 += 1;
                timeBetweenSpawn1 = spawnTime;
            }
            timeBetweenSpawn1 -= Time.deltaTime;
        }

        //JOUEUR 2 (RIGHT)
        if (currentNumberOfElectricBall2 < maxNumberOfElectricBall)
        {
            if (timeBetweenSpawn2 < 0)
            {
                float generateZ2 = Random.Range(zMin, zMax);
                Debug.Log("Generate 2");
                float generateX2 = GenerateX(2, generateZ2);
                Instantiate(electricBallPrefab, new Vector3(generateX2, 0, generateZ2), Quaternion.identity);
                currentNumberOfElectricBall2 += 1;
                timeBetweenSpawn2 = spawnTime;
            }
            timeBetweenSpawn2 -= Time.deltaTime;
        }
    }

    public float GenerateX(int id, float zValue)
    {
        float generateX = Random.Range(xMin, xMax);
        if(id == 1)
        {
            switch (zValue)
            {
                case float z when (z < zMax && z > borderBot[0].transform.position.z):
                    while (generateX > borderTop[0].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[0].transform.position.z && z > borderBot[1].transform.position.z):
                    while (generateX > borderTop[1].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[1].transform.position.z && z > borderBot[2].transform.position.z):
                    while (generateX > borderTop[2].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[2].transform.position.z && z > borderBot[3].transform.position.z):
                    while (generateX > borderTop[3].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[3].transform.position.z && z > zMin):
                    while (generateX > borderTop[4].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                default:
                    break;
            }
        }

        else if(id == 2)
        {
            switch (zValue)
            {
                case float z when (z < zMax && z > borderBot[0].transform.position.z):
                    while (generateX < borderTop[0].transform.position.x && Physics.CheckSphere(new Vector3(generateX, 0, zValue), 5))
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[0].transform.position.z && z > borderBot[1].transform.position.z):
                    while (generateX < borderTop[1].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[1].transform.position.z && z > borderBot[2].transform.position.z):
                    while (generateX < borderTop[2].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[2].transform.position.z && z > borderBot[3].transform.position.z):
                    while (generateX < borderTop[3].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                case float z when (z < borderBot[3].transform.position.z && z > zMin):
                    while (generateX < borderTop[4].transform.position.x)
                    {
                        generateX = Random.Range(xMin, xMax);
                    }
                    break;

                default:
                    break;
            }
        }

            return generateX;
    }
    public void DecrementCurrentElectricBall(int playerID)
    {
        if (playerID == 1)
            currentNumberOfElectricBall1 -= 1;
        else if (playerID == 2)
            currentNumberOfElectricBall2 -= 1;
        else
            Debug.Log("playerID incorrect en changement current electric ball");
    }
}
