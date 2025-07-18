using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.Z;
    public KeyCode downKey = KeyCode.S;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

    
        if (Input.GetKey(leftKey) )
            direction += Vector3.left;

        if (Input.GetKey(rightKey))
            direction += Vector3.right;

        if (Input.GetKey(upKey ))
            direction += Vector3.forward;

        if (Input.GetKey(downKey))
            direction += Vector3.back;

       
        
        
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}

   
