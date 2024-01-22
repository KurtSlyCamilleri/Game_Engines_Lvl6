using System.Collections;
using UnityEngine;

public class TimeoutPlatform : MonoBehaviour {
    //in the future, consolidate timing approach to be more consistent

    public GameObject Player;
    public float shakeDuration = 0.5f;
    [System.NonSerialized] public float shakeMagnitude = 0.5f;
    private bool isShaking = false;
    [SerializeField] private float TimeOut = 3f;
    [SerializeField] GameManager gameManager;

    private void Update() {
        //if(gameManager.playerHealth <= 0) {
            
        //}
    }

    private void TimerElapsed() {
        this.gameObject.SetActive(false); // Destroy the platform when the timer elapses
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == Player && !isShaking) {
            StartCoroutine(ShakePlatform());
            Invoke("TimerElapsed", TimeOut);
        }
    }

    private IEnumerator ShakePlatform() {
        isShaking = true;
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        while (elapsed < shakeDuration) {
            float x = originalPosition.x + Random.Range(0, shakeMagnitude);
            transform.position = new Vector3(x, originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
        isShaking = false;
        TimerElapsed();
    }

    /*
     * code summary:
     * - this controls a platform's behaviour in response to collisions with a specified player GameObject. 
     * - When a collision occurs, the platform initiates a shaking animation and sets a timer to destroy itself after 2 seconds. 
     * - The shaking effect is achieved through a coroutine, changing the platform's position randomly within a specified range for a set duration. 
     * - After the shaking, the platform is destroyed, removing it from the scene.
     * - The script ensures that the platform only shakes and destroys itself once, preventing continuous shaking. 
     * - The implementation uses Unity's MonoBehaviour and coroutine functionalities for controlled animations and actions.
    */

}
