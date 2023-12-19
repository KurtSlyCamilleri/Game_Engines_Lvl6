using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FlyThroughTest : MonoBehaviour {
    public GameObject[] FlyThroughPoints;
    [SerializeField] private CinemachineDollyCart[] dollyCarts;
    public CinemachineVirtualCamera[] virtualCameras;
    public GameObject Player;

    void Start() {
        foreach (var dollyCart in dollyCarts) {//set the speed of each dolly cart to - to ensure they start stationary
            dollyCart.m_Speed = 0f;
        }
    }

    void Update() {
        CheckFlyThroughPoints();
    }

    void CheckFlyThroughPoints() {
        for (int i = 0; i < FlyThroughPoints.Length; i++) {
            Collider flyThroughCollider = FlyThroughPoints[i].GetComponent<Collider>();//get the collider of the current flythrough point
            if (flyThroughCollider.bounds.Contains(Player.transform.position)) {//check if the player's position is within the bounds of the flythrough point
                FlyThroughPoints[i].SetActive(false);//deactivate flythroughpoint after being triggered
                StartCoroutine(ActivateFlyThroughMethod(i));//start the activation coroutine for the currennt flythough point
                break;//exit the loop after activating the first flythrough point to avoid redundant activations
            }
        }
    }


    IEnumerator ActivateFlyThroughMethod(int index) {
        switch (index) {//switch between different flythough points based on the index
            case 0:
                yield return StartCoroutine(FlyThrough1());//star the coroutine for the first flythrough point
                break;
            case 1:
                yield return StartCoroutine(FlyThrough2());//start the coroutine for the second flythrough point
                break;
            // Add more cases for additional flythrough points as needed
            case 2:
                yield return StartCoroutine(FlyThrough3());
                break;
            default:
                yield break;
        }
    }

    IEnumerator FlyThrough1() {
        dollyCarts[0].m_Speed = 10f;
        virtualCameras[0].Priority = 0;
        virtualCameras[1].Priority = 100;
        yield return new WaitForSeconds(10);
        virtualCameras[0].Priority = 100;
        virtualCameras[1].Priority = 0;
    }

    IEnumerator FlyThrough2() {
        dollyCarts[1].m_Speed = 10f;
        virtualCameras[0].Priority = 0;
        virtualCameras[2].Priority = 100;
        yield return new WaitForSeconds(10);
        virtualCameras[0].Priority = 100;
        virtualCameras[2].Priority = 0;
    }

    IEnumerator FlyThrough3() {
        dollyCarts[2].m_Speed = 10f;
        virtualCameras[0].Priority = 0;
        virtualCameras[3].Priority = 100;
        yield return new WaitForSeconds(15);
        virtualCameras[0].Priority = 100;
        virtualCameras[3].Priority = 0;
    }
}