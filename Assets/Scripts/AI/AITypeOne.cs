using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AITypeOne : MonoBehaviour
{
    public Transform PlayerObj;
    public GameObject EnemyPrefab;
    [System.NonSerialized] public float detectionRadius = 30f;
    [System.NonSerialized] public float detectionInterval = 4f;
    [System.NonSerialized] public float spawnDelay = 2f;
    private float lastDetectionTime;
    private Vector3 detectedPlayerPosition;

    private void Update() {
        //checks if the time elapsed since the last detection is greater than or equal to the detection interval
        if(Time.time- lastDetectionTime >= detectionInterval) {
            lastDetectionTime = Time.time;//update the last detection time to the current time
            
            if (isPlayerWithinRadius()) {//check if the palyer is within the detection radius
                detectedPlayerPosition = PlayerObj.position;//record the position of the last detected player
                StartCoroutine(SpawnEnemyAfterDelay(spawnDelay, detectedPlayerPosition));
            }
        }
    }

    IEnumerator SpawnEnemyAfterDelay(float delay, Vector3 playerPosition) {
        yield return new WaitForSeconds(delay);
        Instantiate(EnemyPrefab, playerPosition, Quaternion.identity);
    }
    bool isPlayerWithinRadius() {
        float distance = Vector3.Distance(PlayerObj.position, transform.position);
        return distance <= detectionRadius;

    }
}
