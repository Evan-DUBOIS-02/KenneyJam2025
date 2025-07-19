using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public KeyCode dropKey = KeyCode.E;
    public int collectibles = 0;

    public int maxCollectibles = 3;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectibles") && collectibles < maxCollectibles )
        {
            Destroy(other.gameObject);
            collectibles++;
        }
    }

    void OnTriggerStay(Collider other)
    { 
        if (other.CompareTag("Tower"))
        {

            if (Input.GetKeyDown(dropKey) && collectibles > 0)
            {
                collectibles--;
                other.GetComponent<TowerManager>().IncreaseTerrain(1);
                                 
            }
        }

    }


}
