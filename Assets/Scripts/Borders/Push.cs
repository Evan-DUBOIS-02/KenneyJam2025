using System;
using System.Collections;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Push : MonoBehaviour
{
    public float pushDuration = 0.3f;
    
    public float pushDistance = 10f;

    public bool horizontal = true;
    
    private Vector3 backwards;

    public AudioSource audioSourceBounce;
    public AudioClip soundBounce;

    public float minRangePitch = 0.8f;
    public float maxRangePitch = 1.2f;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Contact ! ");
            audioSourceBounce.PlayOneShot(soundBounce);
            audioSourceBounce.pitch = URandom.Range(minRangePitch, maxRangePitch);
            Vector3 direction = other.transform.position - transform.position;

            if (horizontal)
            {
                float directionX = Mathf.Sign(direction.x);
                backwards = new Vector3(directionX * pushDistance, 0f, 0f);
            }
            else
            {
                float directionZ = Mathf.Sign(direction.z);
                backwards = new Vector3(0f, 0f, directionZ * pushDistance);
            }


            Vector3 startPosition = other.transform.position;
            Vector3 targetPosition = startPosition + backwards;


            StartCoroutine(MoveSmoothly(other.transform, startPosition, targetPosition, pushDuration));

            other.GetComponent<PlayerMouvement>().PlayPushAnimation();
        }



    }

     private System.Collections.IEnumerator MoveSmoothly(Transform obj, Vector3 start, Vector3 target, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            obj.position = Vector3.Lerp(start, target, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        obj.position = target;
    }
}
