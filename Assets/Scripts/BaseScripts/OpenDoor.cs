using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : Interactable{
    [SerializeField]private GameObject Example;
    private bool isCoroutineRunning = false;

    public override void Use() {
        if (!isCoroutineRunning) {
            StartCoroutine(HideExample());
        }
        Debug.Log("OpenDoor");
    }
    private IEnumerator HideExample() {
        Example.SetActive(true);
        yield return new WaitForSeconds(2f);
        Example.SetActive(false);

        isCoroutineRunning = false;
    }
}
