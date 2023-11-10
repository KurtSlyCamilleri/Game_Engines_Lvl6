using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITypeTwo : MonoBehaviour{
    public GameObject player;
    public GameObject enemyPrefab;
    private bool enemySpawned = false;
    private NavMeshAgent enemyNavMeshAgent;

    private void Update() {
        DetectionRadiusOne();
    }
    void DetectionRadiusOne() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 15f);

        foreach (Collider collider in colliders) {
            if(collider.gameObject == player && !enemySpawned) {
                SpawnEnemy();
            }
        }

        if (enemySpawned) {
            MoveEnemy();
        }
    }
    private void SpawnEnemy() {
        enemyPrefab = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        enemySpawned=true;
        enemyNavMeshAgent = enemyPrefab.GetComponent<NavMeshAgent>();
        ModifyNavMesh();
        MoveEnemy();
    }

    private void ModifyNavMesh() {
        Vector3 position = enemyPrefab.transform.position;
        NavMeshHit hit;

        if(NavMesh.SamplePosition(position, out hit, 1.0f, NavMesh.AllAreas)) {
            NavMesh.SetAreaCost(hit.mask, 2f);
        }
    }
    private void MoveEnemy() {
        if(enemyNavMeshAgent != null && enemyNavMeshAgent.isOnNavMesh) {
            enemyNavMeshAgent.destination = player.transform.position;
        }
    }
}
