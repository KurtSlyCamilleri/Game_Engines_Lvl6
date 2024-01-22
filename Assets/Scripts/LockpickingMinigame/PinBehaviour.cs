using UnityEngine;
using System.Collections.Generic;

public class PinBehaviour : MonoBehaviour {
    [SerializeField] private GameObject Pick;
    [SerializeField] private List<GameObject> PinEndColliders;
    [SerializeField] private List<GameObject> CloseToPointColliders;
    [SerializeField] private List<GameObject> PickPointColliders;
    [SerializeField] private PickBehaviour pickBehaviour;
    [SerializeField] private GameObject lockpicking;

    private List<bool> isPinnedList = new List<bool>();
    private List<bool> successfullyPickedList = new List<bool>();
    private List<int> unsuccessfulPickingsList = new List<int>();

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject payloadObject;
    [SerializeField] private EnablePicking enablePicking;
    [SerializeField] private PlayerMovement playerMovement;


    [SerializeField] private AudioSource PinAudioSource;
    [SerializeField] private AudioClip pinCorrectSlot;
    [SerializeField] private AudioClip pinMoving;


    public bool finished = false;

    private void Start() {
        for (int i = 0; i < PinEndColliders.Count; i++) {//initialize lists for each pin
            isPinnedList.Add(false);
            successfullyPickedList.Add(false);
            unsuccessfulPickingsList.Add(0);
        }
        playerMovement.isParalyzed = true;
    }

    public void Update() {
        bool allPinsSuccessfullyPicked = true;

        for (int i = 0; i < PinEndColliders.Count; i++) {
            if (CheckCollision(i) && !isPinnedList[i] && !Input.GetKey(KeyCode.Space)) {
                ParentPin(i);
                PlayPinMovingSound();  // Play pin moving sound
            }
            if (Input.GetKeyDown(KeyCode.Space) && isPinnedList[i]) {
                UnparentPin(i);
                //StopPinMovingSound();  // Play pin moving sound again
            }
            CheckIfCloseToPick(i);
            CheckIfPicked(i);
            if (!successfullyPickedList[i]) {//check if the current pin is not successfully picked
                allPinsSuccessfullyPicked = false;
            }
        }
        if (allPinsSuccessfullyPicked) {//if all pins are successfullyy picked do this
            finished = true;
            playerMovement.isParalyzed = false;
            PlayPinCorrectSlotSound();
            AccessPayload();
            
            lockpicking.SetActive(false);
        }
    }

    private void AccessPayload() {
        if (payloadObject != null) {
            MonoBehaviour[] scripts = payloadObject.GetComponents<MonoBehaviour>();//get all scripts attached to the gameobject
            foreach (MonoBehaviour script in scripts) {//iterate through each script and check for the runpayload method
                System.Reflection.MethodInfo method = script.GetType().GetMethod("RunPayload");
                if (method != null) {//invoke the runpayload method dynamically
                    method.Invoke(script, null);
                    return;
                }
            }

        }
    }

    public bool IsAnyPinned() {
        return isPinnedList.Contains(true);//check if any pin is currently pinned
    }

    private bool CheckCollision(int index) {
        BoxCollider2D pickEndBoxCollider = Pick.GetComponent<BoxCollider2D>();
        BoxCollider2D pinEndBoxCollider = PinEndColliders[index].GetComponent<BoxCollider2D>();

        if (pickEndBoxCollider != null && pinEndBoxCollider != null) {
            return pickEndBoxCollider.bounds.Intersects(pinEndBoxCollider.bounds);
        }
        return false;
    }

    private void ParentPin(int index) {
        if (!isPinnedList[index] && !successfullyPickedList[index]) {
            PinEndColliders[index].transform.parent = Pick.transform;
            isPinnedList[index] = true;
        }
    }

    private void UnparentPin(int index) {
        if (CheckIfPicked(index)) {
            PlayPinCorrectSlotSound();
            successfullyPickedList[index] = true;
            
        } else {
            unsuccessfulPickingsList[index]++;//counts how many unsuccessful pickings there are
            if (unsuccessfulPickingsList[index] % 5 == 0) {//check if 5 unsuccessful pickings have occured
                gameManager.PickAmount --;
            }
        }
        PinEndColliders[index].transform.parent = null;//unparent the pin
        pickBehaviour.ResetPickPosition();
        isPinnedList[index] = false;
    }

    private void CheckIfCloseToPick(int index) {
        BoxCollider2D closeToPointBoxCollider = CloseToPointColliders[index].GetComponent<BoxCollider2D>();
        if (closeToPointBoxCollider != null && closeToPointBoxCollider.bounds.Intersects(PinEndColliders[index].GetComponent<BoxCollider2D>().bounds)) {
            // Debug.Log("Close to pick collider collided for pin " + index + "!");
            //to be replaced with shaking
        }
    }

    private bool CheckIfPicked(int index) {
        BoxCollider2D pickPointBoxCollider = PickPointColliders[index].GetComponent<BoxCollider2D>();
        if (pickPointBoxCollider != null && pickPointBoxCollider.bounds.Intersects(PinEndColliders[index].GetComponent<BoxCollider2D>().bounds)) {
            return true;
        }
        return false;
    }

    private void PlayPinMovingSound() {
        if (PinAudioSource != null && pinMoving != null && !PinAudioSource.isPlaying) {
            PinAudioSource.clip = pinMoving;
            PinAudioSource.Play();
        }
    }


    private void PlayPinCorrectSlotSound() {
        if (PinAudioSource != null && pinCorrectSlot != null && !PinAudioSource.isPlaying) {
            PinAudioSource.clip = pinCorrectSlot;
            PinAudioSource.Play();
        }
    }
}

//PlayPinCorrectSlotSound();  // Play pin correct slot sound