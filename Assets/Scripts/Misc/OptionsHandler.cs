using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsHandler : MonoBehaviour
{
    public void GoBack() {
        SceneManager.LoadScene("Main_Menu_UI");
    }
}
