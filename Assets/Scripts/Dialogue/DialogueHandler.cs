using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueHandler : MonoBehaviour {
    [SerializeField] private GameObject[] DialoguePoints;
    [SerializeField] private TextAsset[] textFiles;
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private float displayInterval = 2f;

    [SerializeField] private GameObject DialogueBox;

    private bool isCoroutineRunning = false;
    private int currentDialogueIndex = -1; // Initialize with an invalid index
    private bool[] dialogueDisplayed;

    private void Start() {
        dialogueDisplayed = new bool[DialoguePoints.Length];
    }

    void Update() {
        CheckItemContact();
        if (currentDialogueIndex != -1 && !isCoroutineRunning) {
            StartCoroutine(DisplayTextRoutine());
        }
    }

    void CheckItemContact() {
        foreach (GameObject dialoguePoint in DialoguePoints) {
            if (dialoguePoint != null) {
                float contactDistance = 2.0f; // Adjust the contact distance as needed
                bool inContact = Physics.CheckSphere(dialoguePoint.transform.position, contactDistance, LayerMask.GetMask("Player"));

                if (inContact) {//player in contact with the dialogue point
                    int dialogueIndex = System.Array.IndexOf(DialoguePoints, dialoguePoint);
                    if (currentDialogueIndex != dialogueIndex && !dialogueDisplayed[dialogueIndex]) {
                        currentDialogueIndex = dialogueIndex;
                        LoadTextFile();
                        dialogueDisplayed[dialogueIndex] = true; // Mark dialogue as displayed
                        StartTextDisplay();
                    }
                    return; // Exit the loop after finding the first contact
                }
            }
        }
        currentDialogueIndex = -1;//Player not in contact with any dialogue point
    }

    void StartTextDisplay() {
        DialogueBox.SetActive(true);
        if (!isCoroutineRunning) {
            StartCoroutine(DisplayTextRoutine());
        }
    }

    IEnumerator DisplayTextRoutine() {
        isCoroutineRunning = true;

        if (textFiles.Length > 0 && currentDialogueIndex >= 0 && currentDialogueIndex < textFiles.Length) {
            TextAsset selectedFile = textFiles[currentDialogueIndex];
            string[] lines = selectedFile.text.Split('\n');

            foreach (string line in lines) {
                // Check for bold and italic formatting
                string formattedLine = FormatText(line);

                textDisplay.text = formattedLine;
                yield return new WaitForSeconds(displayInterval);
                textDisplay.text = ""; // Clear the text before moving to the next line
            }

            currentDialogueIndex = -1; // Reset dialogue index after displaying
            DialogueBox.SetActive(false);
        }

        isCoroutineRunning = false;
    }


    void LoadTextFile() {
        if (currentDialogueIndex >= 0 && currentDialogueIndex < textFiles.Length) {
            TextAsset selectedFile = textFiles[currentDialogueIndex];
            string[] lines = selectedFile.text.Split('\n');
        }
    }

    // Function to format text with bold and italic tags
    string FormatText(string inputText) {
        inputText = inputText.Replace("||", "<b>") // Replace " with <b> for bold
                             .Replace("*", "<i>");   // Replace * with <i> for italic
        return inputText;
    }
}
