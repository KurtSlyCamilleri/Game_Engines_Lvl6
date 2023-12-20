using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoCutscenes : MonoBehaviour {
    public GameObject[] colCutscenes; // Cutscenes triggered by colliders
    public GameObject[] cutsceneTriggers; // Colliders to trigger the cutscenes
    public GameObject player; // Reference to the player GameObject
    private bool[] colliderTriggered; // Flag to track whether each collider has triggered a cutscene

    void Start() {
        colliderTriggered = new bool[cutsceneTriggers.Length];
    }

    // Update is called once per frame
    void Update() {
        CheckCutsceneTriggers();
    }

    void CheckCutsceneTriggers() {
        for (int i = 0; i < cutsceneTriggers.Length; i++) {
            Collider triggerCollider = cutsceneTriggers[i].GetComponent<Collider>();

            if (triggerCollider.bounds.Contains(player.transform.position) && !colliderTriggered[i]) {
                Debug.Log("Player entered trigger collider for Cutscene: " + colCutscenes[i].name);//log the cutscene name to the console for debugging
                PlayCutscene(colCutscenes[i], triggerCollider, i);//play cutscene
                colliderTriggered[i] = true;//set the flag to indicate that the collider has triggered a cutscene
                triggerCollider.enabled = false;//add this to flythrough afterwards
            }
        }
    }


    void PlayCutscene(GameObject cutscene, Collider triggerCollider, int index) {
        VideoPlayer videoPlayer = cutscene.GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += delegate {
            cutscene.SetActive(false);
        };

        cutscene.SetActive(true);
        videoPlayer.Play();
    }
}