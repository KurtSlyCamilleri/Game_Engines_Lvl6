using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour {
    [SerializeField] private GameObject videoPlayerGameObject;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private VideoClip videoClip;
    [SerializeField] private RenderTexture renderTexture;

    private void Start() {
        
    }

    public void StartCutscene() {
        // Make sure all required components are assigned
        if (videoPlayer == null || rawImage == null || videoClip == null || renderTexture == null) {
            Debug.LogError("Please assign all components in the inspector.");
            return;
        }

        // Assign video clip to the video player
        videoPlayer.clip = videoClip;

        // Assign render texture to the target texture of the video player
        videoPlayer.targetTexture = renderTexture;

        // Assign render texture to the raw image texture
        rawImage.texture = renderTexture;

        // Add a listener for the videoPlayer to call a method when the video is finished
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayerGameObject.SetActive(true);

        // Play the video
        videoPlayer.Play();
    }
    
    private System.Action onVideoFinishedAction;

    public void SetOnVideoFinishedAction(System.Action action) {
        onVideoFinishedAction = action;
    }

    private void OnVideoFinished(VideoPlayer vp) {
        // Deactivate the videoPlayerGameObject when the video is finished
        videoPlayerGameObject.SetActive(false);

        // Remove the listener to avoid potential issues
        videoPlayer.loopPointReached -= OnVideoFinished;

        // Perform the custom action if set
        onVideoFinishedAction?.Invoke();


    }
}
