using UnityEngine;
using UnityEngine.UI;

public class DialogueTester : MonoBehaviour
{
    [Header("Ink JSON for Testing")]
    [SerializeField] private TextAsset inkJSON;

    [Header("Test Button")]
    [SerializeField] private Button testButton;

    private void Start()
    {
        Debug.Log("DialogueTester Start() called");

        if (testButton != null)
        {
            Debug.Log("Test button found! Adding onClick listener...");
            testButton.onClick.RemoveAllListeners();
            testButton.onClick.AddListener(StartTestDialogue);
            Debug.Log("onClick listener added. Button interactable: " + testButton.interactable);
        }
        else
        {
            Debug.LogError("Test button is NULL! Assign it in the Inspector!");
        }
    }

    public void StartTestDialogue()
    {
        Debug.Log("StartTestDialogue() called!");

        if (inkJSON == null)
        {
            Debug.LogError("InkJSON is missing!");
            return;
        }

        if (DialogueManager.GetInstance() == null)
        {
            Debug.LogError("DialogueManager instance is NULL!");
            return;
        }

        Debug.Log("Starting test dialogue with InkJSON: " + inkJSON.name);
        DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
    }
}
