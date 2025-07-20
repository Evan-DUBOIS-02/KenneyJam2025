using UnityEngine;

public class Repeat : MonoBehaviour
{
    
    public GameObject wallPrefab;        // Le morceau de mur à répéter
    public int repeatCount = 5;          // Combien de fois répéter
    public Vector3 repeatDirection = Vector3.right; // Direction de répétition
    public Transform container;          // Parent qui contiendra les blocs

    public float spacing = 1f; // Ajuste selon la taille de ton prefab

    void Start()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            Vector3 offset = new Vector3(0, 0, i * spacing);
            Instantiate(wallPrefab, transform.position + offset, Quaternion.identity, transform);
        }
    }

    public void BuildWall()
    {
        ClearOldWall();

        for (int i = 0; i < repeatCount; i++)
        {
            Vector3 offset = repeatDirection * i;
            Instantiate(wallPrefab, transform.position + offset, Quaternion.identity, container);
        }
    }

    public void ClearOldWall()
    {
        if (container == null) return;

        foreach (Transform child in container)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}


