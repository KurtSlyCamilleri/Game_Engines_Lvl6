using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerOrbit : MonoBehaviour {
    public Transform target; // Object to orbit around
    public float OrbitAngle = 45f; // Adjust the orbit angle as needed
    public float DistanceOffsetX = 0.5f; // Adjust the distance offset as needed
    public float DistanceOffsetZ = 0.5f;
    public float HeightOffset = 0.5f; // Adjust the height offset as needed
    public Collider WallDetector;
    private bool between;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {//rotate around target when tab is pressed
            RotateAroundTarget();
        }

        //CheckIfTargetInFront();
        //CheckObjectsBehindCamera();

        Vector3 targetPosition;
        targetPosition.x = target.position.x + DistanceOffsetX;
        targetPosition.y = target.position.y + HeightOffset;
        targetPosition.z = target.position.z + DistanceOffsetZ;
        transform.position = targetPosition;

        if(between == false) {
            Debug.Log("Between = false");
        } else {
            Debug.Log("Between = true");
        }
    }

    void RotateAroundTarget() {
        float targetAngle = Mathf.Round(transform.eulerAngles.y / OrbitAngle) * OrbitAngle + OrbitAngle;
        float rotationAmount = targetAngle - transform.eulerAngles.y;
        Debug.Log(targetAngle);
        transform.RotateAround(target.position, Vector3.up, rotationAmount); // Orbit around the target
    }

    //clipping check not yet implemented enough to keep in code, ignore this

    /*
    //checking for clipping
    void CheckIfTargetInFront() {
        // Raycast to check if the target is in front of the camera
        RaycastHit frontHit;
        Vector3 frontRayDirection = target.position - transform.position;

        if (Physics.Raycast(transform.position, frontRayDirection, out frontHit)) {
            if (frontHit.transform != target) {
                Debug.Log("There is something between the camera and the target");
                between = true;

            } else {
                Debug.Log("There is nothing between the camera and the target");
                between = false;
            }
        }
    }
    void CheckObjectsBehindCamera() {
        // Raycast to check if there are objects behind the camera
        RaycastHit behindHit;
        Vector3 behindRayDirection = -transform.forward; // Direction from camera towards its back

        if (Physics.Raycast(transform.position, behindRayDirection, out behindHit)) {
            Debug.Log("There is an object behind the camera");
            // Handle the case when an object is behind the camera
            // You may want to adjust variables or perform specific actions here
        } else {
            Debug.Log("There is no object behind the camera");
            // Handle the case when there is no object behind the camera
        }
    }
    */
}
