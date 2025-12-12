using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject[] choiceButtons;
    private TextMeshProUGUI[] choiceTexts;

    [Header("Character Portrait")]
    [SerializeField] private Image characterPortrait;
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private Sprite maraPortrait;
    [SerializeField] private Sprite selenePortrait;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    private Story currentStory;
    private bool dialogueIsPlaying = false;

    private static DialogueManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        // Get the text components from choice buttons
        choiceTexts = new TextMeshProUGUI[choiceButtons.Length];
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            choiceTexts[i] = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        // Hide all choice buttons at start
        foreach (GameObject button in choiceButtons)
        {
            button.SetActive(false);
        }
    }

    private void Update()
    {
        // Return right away if dialogue isn't playing
        if (!dialogueIsPlaying)
        {
            return;
        }

        // Only allow continuing if there are no choices currently displayed
        if (currentStory.currentChoices.Count == 0)
        {
            // Handle continuing to next line with Enter or left mouse click
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
            {
                ContinueStory();
            }
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        Debug.Log("EnterDialogueMode called! Activating dialogue panel...");
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        Debug.Log("DialoguePanel active state: " + dialoguePanel.activeSelf);

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        // Hide all choice buttons
        foreach (GameObject button in choiceButtons)
        {
            button.SetActive(false);
        }

        // Clear character info
        if (characterNameText != null)
            characterNameText.text = "";

        Debug.Log("Dialogue ended - panel hidden");
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            // Set text for the current dialogue line
            string currentLine = currentStory.Continue();
            dialogueText.text = currentLine;

            // Update character portrait based on who's speaking
            UpdateCharacterPortrait(currentLine);

            // Display choices, if any, for this dialogue line
            DisplayChoices();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    private void UpdateCharacterPortrait(string dialogueLine)
    {
        // Check if the line starts with a character name
        if (dialogueLine.StartsWith("MARA:"))
        {
            if (characterNameText != null)
                characterNameText.text = "MARA";

            if (characterPortrait != null)
            {
                if (maraPortrait != null)
                {
                    characterPortrait.sprite = maraPortrait;
                    characterPortrait.color = Color.white;
                }
                else
                {
                    // Placeholder: Light blue color for Mara
                    characterPortrait.sprite = null;
                    characterPortrait.color = new Color(0.5f, 0.8f, 1f, 1f);
                }
            }
        }
        else if (dialogueLine.StartsWith("SELENE:"))
        {
            if (characterNameText != null)
                characterNameText.text = "SELENE";

            if (characterPortrait != null)
            {
                if (selenePortrait != null)
                {
                    characterPortrait.sprite = selenePortrait;
                    characterPortrait.color = Color.white;
                }
                else
                {
                    // Placeholder: Orange color for Selene (cat)
                    characterPortrait.sprite = null;
                    characterPortrait.color = new Color(1f, 0.6f, 0.3f, 1f);
                }
            }
        }
        else
        {
            // Narrator or no specific character
            if (characterNameText != null)
                characterNameText.text = "";
        }
    }

    private void DisplayChoices()
    {
        Debug.Log("DisplayChoices() called. Number of choices: " + currentStory.currentChoices.Count);

        if (currentStory.currentChoices.Count > choiceButtons.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: "
                + currentStory.currentChoices.Count);
        }

        int index = 0;
        // Enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach (Choice choice in currentStory.currentChoices)
        {
            Debug.Log("Activating choice button " + index + " with text: " + choice.text);
            choiceButtons[index].SetActive(true);
            choiceTexts[index].text = choice.text;

            // Remove old listeners and add new one
            Button button = choiceButtons[index].GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            int choiceIndex = index; // Capture for lambda
            button.onClick.AddListener(() => MakeChoice(choiceIndex));

            index++;
        }

        // Hide all remaining buttons
        for (int i = index; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].SetActive(false);
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        ContinueStory();
    }

    public bool IsDialoguePlaying()
    {
        return dialogueIsPlaying;
    }
}
