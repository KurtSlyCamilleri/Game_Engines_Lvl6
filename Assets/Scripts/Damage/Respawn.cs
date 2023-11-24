using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public GameManager GameManager;
    public GameObject[] SpawnPoints;
    public GameObject Player;
    private int lastSpawnPointIndex = -1; // Initialize to -1 to indicate no spawn point initially
    private bool isTouchingSpawn = false;

    void Update() {
        detectLastSpawnObject();
        if (GameManager.playerHealth <= 0 && isTouchingSpawn == false) { // if player dies, respawn
            Spawn();
        }
    }

    private void Spawn() {
        MovePlayerToLastSpawnPoint();
        GameManager.playerHealth = 3;
        LoadingScreen();
        //call loading
    }

    private void detectLastSpawnObject() {// find spawn location
        for (int i = 0; i < SpawnPoints.Length; i++) {
            Collider spawnCollider = SpawnPoints[i].GetComponent<Collider>();

            if (spawnCollider.bounds.Contains(Player.transform.position)) {
                Debug.Log("Player hit spawn point at index: " + i);
                lastSpawnPointIndex = i;
            }
        }
    }

    private void MovePlayerToLastSpawnPoint() {
        Player.transform.position = SpawnPoints[lastSpawnPointIndex].transform.position;
    }

    private void UpdateIsTouchingSpawn() {
        isTouchingSpawn = false;
        foreach (GameObject spawnPoint in SpawnPoints) {
            Collider spawnCollider = spawnPoint.GetComponent<Collider>();
            if (spawnCollider.bounds.Contains(Player.transform.position)) {
                isTouchingSpawn = true;
                return;
            }
        }
    }

    private void LoadingScreen() {
        //to be done tomorrow
    }
}