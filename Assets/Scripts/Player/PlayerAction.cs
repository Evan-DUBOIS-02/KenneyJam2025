using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public KeyCode takeKey = KeyCode.A;
    public KeyCode dropKey = KeyCode.E;


    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(takeKey))
        {
            
            Debug.Log("Take");
        }

        if (Input.GetKeyDown(dropKey))
        {
            
            Debug.Log("Drop");
        }
    }


}
