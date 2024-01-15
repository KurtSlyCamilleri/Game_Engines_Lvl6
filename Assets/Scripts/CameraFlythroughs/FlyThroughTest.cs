using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FlyThroughTest : MonoBehaviour {
    
    public GameObject[] FlyThroughPoints;
    [SerializeField] private CinemachineDollyCart[] dollyCarts;
    public CinemachineVirtualCamera[] virtualCameras;
    public GameObject Player;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField]private Rigidbody playerRigidbody;
    private bool isCutsceneActive = false;

    void Start() {
        foreach (var dollyCart in dollyCarts) {
            dollyCart.m_Speed = 0f;
        }
        playerRigidbody = Player.GetComponent<Rigidbody>();
    }

    void Update() {
        if (!isCutsceneActive) {
            CheckFlyThroughPoints();
        }
    }

    void CheckFlyThroughPoints() {
        for (int i = 0; i < FlyThroughPoints.Length; i++) {
            Collider flyThroughCollider = FlyThroughPoints[i].GetComponent<Collider>();
            if (flyThroughCollider.bounds.Contains(Player.transform.position)) {
                FlyThroughPoints[i].SetActive(false);
                StartCoroutine(ActivateFlyThroughMethod(i));
                break;
            }
        }
    }

    IEnumerator ActivateFlyThroughMethod(int index) {
        isCutsceneActive = true;
        playerRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        playerMovement.isParalyzed = true;

        switch (index) {
            case 0:
                yield return StartCoroutine(FlyThrough1());
                break;
            case 1:
                yield return StartCoroutine(FlyThrough2());
                break;
            case 2:
                yield return StartCoroutine(FlyThrough3());
                break;
            default:
                yield break;
        }

        isCutsceneActive = false;
        playerRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
        playerRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        playerMovement.isParalyzed = false;
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
