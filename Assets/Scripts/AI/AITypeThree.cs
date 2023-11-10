using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AITypeThree : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 10f;
    public float stopFleeDistance = 30f;
    public float exploreRadius = 5f;
    public float ventDetectionRadius = 2;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private bool isPlayerInSight = false;
    private bool isTouchingVent = false;
    public LayerMask ventLayer;


    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update() {
        isPlayerInSight = IsPlayerInSight();
        if(isPlayerInSight && !isTouchingVent) {
            PlayerInSight();
            ExploreRandomly();
        } else {
            Destroy(gameObject);
        }

        CheckForVent();
        IsTouchingVent();
    }
    void PlayerInSight() {
        float distance = Vector3.Distance(transform.position, player.position);
        if(distance < fleeDistance) {
            Vector3 fleeDirection = transform.position - player.position;
            Vector3 newPosition = transform.position + fleeDirection.normalized * fleeDistance;
            agent.SetDestination(newPosition);
        }else if(distance> stopFleeDistance) {
            agent.ResetPath();
        }
    }
    void ExploreRandomly() {
        if(!agent.pathPending && agent.remainingDistance < 0.5f) {
            Vector2 randomPoint = Random.insideUnitCircle * exploreRadius;
            targetPosition = new Vector3(transform.position.x + randomPoint.x, transform.position.y, transform.position.z + randomPoint.y);
            agent.SetDestination(targetPosition);
        }
    }
    bool IsPlayerInSight() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, player.position - transform.position, out hit)) {
            return hit.collider.gameObject == player.gameObject;
        }
        return false;
    }

    
    void CheckForVent() {
        RaycastHit ventDetect; 
        if(Physics.Raycast(transform.position, transform.forward, out ventDetect, ventDetectionRadius, ventLayer)) {
            agent.SetDestination(ventDetect.point);
            isPlayerInSight = false;
        }
    }

    void IsTouchingVent() {
        RaycastHit ventHit;
        if(Physics.Raycast(transform.position, transform.forward, out ventHit, 0.1f, ventLayer)) {
            isTouchingVent = true;
        }
    }
        
}
