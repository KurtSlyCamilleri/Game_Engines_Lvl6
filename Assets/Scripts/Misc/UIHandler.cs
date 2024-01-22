using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour {
    [SerializeField] public GameObject Cig1, Cig2, Cig3;
    [SerializeField] public GameObject Pick1, Pick2, Pick3, Pick4;
    public GameManager gameManager;
    public GameObject PausePanel;
    public PlayerMovement playerMovement;
    public bool isPaused = false;

    private float originalVolume; // Store the original volume

    // Update is called once per frame
    void Update() {
        UpdateCigaretteVisibility();
        UpdatePickVisibility();

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
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

    void UpdatePickVisibility() {
        Pick1.SetActive(gameManager.PickAmount >= 1);
        Pick2.SetActive(gameManager.PickAmount >= 2);
        Pick3.SetActive(gameManager.PickAmount >= 3);
        Pick4.SetActive(gameManager.PickAmount >= 4);
    }

    public void Pause() {
        PausePanel.SetActive(true);
        originalVolume = AudioListener.volume; // Store the original volume
        AudioListener.volume = 0; // Set volume to 0 when paused
        Time.timeScale = 0;
        isPaused = true;
        playerMovement.SelectWithMouse();
    }

    public void Continue() {
        PausePanel.SetActive(false);
        AudioListener.volume = originalVolume; // Restore the original volume
        Time.timeScale = 1;
        isPaused = false;
        playerMovement.TurnWithMouse();
    }
}
