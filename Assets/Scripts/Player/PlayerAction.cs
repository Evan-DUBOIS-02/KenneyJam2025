using UnityEngine;

public class PlayerAction : MonoBehaviour
{

    public KeyCode dropKey = KeyCode.E;
    public int collectibles = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectibles"))
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
                  
            }
        }

    }


}
