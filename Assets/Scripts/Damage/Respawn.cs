using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {
    public GameManager GameManager;
    public GameObject[] SpawnPoints;
    public GameObject Player;
    public PlayerMovement PlayerMovement;
    private int lastSpawnPointIndex = 0;
    private bool isTouchingSpawn = false;


    void Update() {
        UpdateIsTouchingSpawn();
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
                
                //Debug.Log("Player hit spawn point at index: " + i);
                lastSpawnPointIndex = i;
            }
        }
    }

    private void MovePlayerToLastSpawnPoint() {
        //Debug.Log("testing mode enabled, immortal");
        Rigidbody playerRigidbody = Player.GetComponent<Rigidbody>();
        playerRigidbody.position = SpawnPoints[lastSpawnPointIndex].transform.position;
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