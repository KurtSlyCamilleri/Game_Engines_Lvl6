using System.Collections;
using UnityEngine;

public class TimeoutPlatform : MonoBehaviour {
    //in the future, consolidate timing approach to be more consistent
    
    public GameObject Player;
    public float shakeDuration = 0.5f;
    [System.NonSerialized] public float shakeMagnitude = 0.5f; 
    [System.NonSerialized] public float fallSpeed = 70; 
    private bool isShaking = false;

    private void TimerElapsed() {
        StartCoroutine(FallPlatform());
    }
    private void OnCollisionEnter(Collision collision) { // handle collision events with the player
        if (collision.gameObject == Player && !isShaking) { // check if the collision is with the player and the platform is not already shaking
            StartCoroutine(ShakePlatform()); // start the coroutine to shake the platform
            Invoke("TimerElapsed", 2f); // Invoke the timerElapsed function after a delay of 2 seconds
        }
    }
    private IEnumerator ShakePlatform() {
        isShaking = true; 
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < shakeDuration) { 
            float x = originalPosition.x + Random.Range(0, shakeMagnitude); // calculate a random position for the shake value
            transform.position = new Vector3(x, originalPosition.y, originalPosition.z); // set the new position of the platform
            elapsed += Time.deltaTime; // increment the elapsed time
            yield return null; //wait for the next frame
        }
        transform.position = originalPosition; //reset the platform position
        isShaking = false; 
        TimerElapsed();
    }
    private IEnumerator FallPlatform() {
        Vector3 originalPosition = transform.position; // store the original position of the platform
        Vector3 targetPosition = new Vector3(originalPosition.x, -10, originalPosition.z); // designation for the fall

        float startTime = Time.time; // record the start time of the fall

        while (transform.position.y > targetPosition.y){ // move the platform to the target position smootly over time
            float t = (Time.time - startTime) * fallSpeed; // calculate interpolation factor
            transform.position = Vector3.Lerp(originalPosition, targetPosition, t);// move platform with linear interpolation
            yield return null; //wait for the next frame
        }

        transform.position = targetPosition;
    }

    /*
     * code summary:
     * - this controls a platform's behaviour in response t collisions with a specified player GameObject. 
     * - When a collision occurs, the platform initiates a shaking animation and sets a timer to make the platform fall after 2 seconds. 
     * - The shaking effect is achieved through a coroutine, changing the platform's position randomly within a specified range for a set duration. 
     * - After the shaking, the platform falls smoothly to a designated position using linear interpolation. 
     * - The fall speed is calculated by the 'fallSpeed' variable.
     * - The script ensures that the platform only falls once, preventing continuous shaking. 
     * - The implementation uses unity's monobehaviour and coroutine functionalities for controlled animations and actions
    */

}