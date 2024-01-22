using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    [SerializeField] GameObject Background;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject Cutscene1Controller;
    [SerializeField] GameObject Cutscene2Controller;
    [SerializeField] Scene nextScene;

    [SerializeField] GameObject loadingScreen;

    [SerializeField] GameObject audioSourceObject; // Drag your GameObject with AudioSource here
    [SerializeField] AudioClip audioClip1;
    [SerializeField] AudioClip audioClip2;

    private AudioSource audioSource;


    private void Start() {
        VideoPlayerController videoController = Cutscene1Controller.GetComponent<VideoPlayerController>();

        if (audioSourceObject == null) {
            Debug.LogError("AudioSourceObject is not assigned.");
            return;
        }

        audioSource = audioSourceObject.GetComponent<AudioSource>();

        if (videoController != null) {
            PlayAudioClip(audioClip2);
            videoController.StartCutscene();
            videoController.SetOnVideoFinishedAction(() => {
                PlayAudioClip(audioClip1);
                Background.SetActive(true);
                Title.SetActive(true);
            });
        } else {
            Debug.LogError("VideoPlayerController script not found on Cutscene1Controller GameObject.");
        }
    }
    void PlayAudioClip(AudioClip clip) {
        if (clip != null && audioSource != null) {
            audioSource.clip = clip;
            audioSource.Play();
        } else {
            Debug.LogError("AudioClip not assigned or AudioSource not found.");
        }
    }

    public void StartGame() {
        VideoPlayerController videoController = Cutscene2Controller.GetComponent<VideoPlayerController>();

        if (videoController != null) {
            // Show loading screen
            PlayAudioClip(audioClip2);

            videoController.StartCutscene();
            videoController.SetOnVideoFinishedAction(() => {
                ShowLoadingScreen();
                // Load the scene asynchronously
                StartCoroutine(LoadSceneAsync("LevelDesignNew"));
            });
        } else {
            Debug.LogError("VideoPlayerController script not found on Cutscene1Controller GameObject.");
        }
    }

    IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone) {
            yield return null;
        }
    }

    void ShowLoadingScreen() {
        loadingScreen.SetActive(true);
    }

    public void OpenOptions() {
        //ShowLoadingScreen();
        //SceneManager.LoadScene("Options");
    }

    public void OpenCredits() {
        //ShowLoadingScreen();
        //SceneManager.LoadScene("Credits");
    }

    public void GameExit() {
        Application.Quit();
    }
}
