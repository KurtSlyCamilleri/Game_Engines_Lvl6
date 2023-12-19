using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]GameObject cutscene;
    [SerializeField] Scene nextScene;
    
    public void StartGame() {
        VideoPlayer videoPlayer = cutscene.GetComponent<VideoPlayer>();

        
        videoPlayer.loopPointReached += delegate {
            cutscene.SetActive(false);
            SceneManager.LoadScene("SampleScene");
        };

        cutscene.SetActive(true);
        videoPlayer.Play();
        
    }
}
