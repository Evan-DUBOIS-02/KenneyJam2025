using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectricBall : MonoBehaviour
{
    [Header("ElectricBall")]
    public GameObject electricBallPrefab;
    public float yAxis;
    public float scaleBallCollider;

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
        if (!GameManager.Instance._gameStarted)
            return;
        
        //JOUEUR 1 (LEFT)
        if(currentNumberOfElectricBall1 < maxNumberOfElectricBall)
        {
            if(timeBetweenSpawn1 < 0)
            {
                float generateZ1 = Random.Range(zMin, zMax);
                float generateX1 = GenerateX(1, generateZ1);
                Instantiate(electricBallPrefab, new Vector3(generateX1, yAxis, generateZ1), Quaternion.identity);
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
                float generateX2 = GenerateX(2, generateZ2);
                Instantiate(electricBallPrefab, new Vector3(generateX2, yAxis, generateZ2), Quaternion.identity);
                currentNumberOfElectricBall2 += 1;
                timeBetweenSpawn2 = spawnTime;
            }
            timeBetweenSpawn2 -= Time.deltaTime;
        }
    }

    public float GenerateX(int id, float zValue)
    {
        float generateX = 0.0f;
        if(id == 1)
        {
            switch (zValue)
            {
                case float z when (z < zMax && z > borderBot[0].transform.position.z):
                    generateX = Random.Range(xMin, borderTop[0].transform.position.x);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(xMin, borderTop[0].transform.position.x);
                    }
                    break;

                case float z when (z < borderBot[0].transform.position.z && z > borderBot[1].transform.position.z):
                    generateX = Random.Range(xMin, borderTop[1].transform.position.x);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(xMin, borderTop[1].transform.position.x);
                    }
                    break;

                case float z when (z < borderBot[1].transform.position.z && z > borderBot[2].transform.position.z):
                    generateX = Random.Range(xMin, borderTop[2].transform.position.x);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(xMin, borderTop[2].transform.position.x);
                    }
                    break;

                case float z when (z < borderBot[2].transform.position.z && z > borderBot[3].transform.position.z):
                    generateX = Random.Range(xMin, borderTop[3].transform.position.x);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(xMin, borderTop[3].transform.position.x);
                    }
                    break;

                case float z when (z < borderBot[3].transform.position.z && z > zMin):
                    generateX = Random.Range(xMin, borderTop[4].transform.position.x);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(xMin, borderTop[4].transform.position.x);
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
                    generateX = Random.Range(borderTop[0].transform.position.x, xMax);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(borderTop[0].transform.position.x, xMax);
                    }
                    break;

                case float z when (z < borderBot[0].transform.position.z && z > borderBot[1].transform.position.z):
                    generateX = Random.Range(borderTop[1].transform.position.x, xMax);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(borderTop[1].transform.position.x, xMax);
                    }
                    break;

                case float z when (z < borderBot[1].transform.position.z && z > borderBot[2].transform.position.z):
                    generateX = Random.Range(borderTop[2].transform.position.x, xMax);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(borderTop[2].transform.position.x, xMax);
                    }
                    break;

                case float z when (z < borderBot[2].transform.position.z && z > borderBot[3].transform.position.z):
                    generateX = Random.Range(borderTop[3].transform.position.x, xMax);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(borderTop[3].transform.position.x, xMax);
                    }
                    break;

                case float z when (z < borderBot[3].transform.position.z && z > zMin):
                    generateX = Random.Range(borderTop[4].transform.position.x, xMax);
                    while (Physics.CheckSphere(new Vector3(generateX, yAxis, zValue), scaleBallCollider))
                    {
                        generateX = Random.Range(borderTop[4].transform.position.x, xMax);
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
        Debug.Log("Decrement for player "  + playerID);
        if (playerID == 1)
            currentNumberOfElectricBall1 -= 1;
        else if (playerID == 2)
            currentNumberOfElectricBall2 -= 1;
        else
            Debug.Log("playerID incorrect en changement current electric ball");
    }
}
