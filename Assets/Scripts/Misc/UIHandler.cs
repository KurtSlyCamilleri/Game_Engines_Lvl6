using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] public GameObject Cig1, Cig2, Cig3;
    public GameManager gameManager;
    public GameObject PausePanel;
    public PlayerMovement playerMovement;
    private bool isPaused = false;

    // Update is called once per frame
    void Update() {
        UpdateCigaretteVisibility();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(isPaused) {
                Continue();
            } else {
                Pause();
            }
        }
    }

    void UpdateCigaretteVisibility() {
        Cig1.SetActive(gameManager.playerHealth >= 1);
        Cig2.SetActive(gameManager.playerHealth >= 2);
        Cig3.SetActive(gameManager.playerHealth >= 3);
    }

    //TODO LATER: fix the mouse toggle issues(will likely have to put the RMB toggle functions in their own method and call a bool)


    public void Pause() {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    public void Continue() {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

}
