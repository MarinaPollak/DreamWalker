using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class DialogueUISetup : MonoBehaviour
{
    [MenuItem("DreamWalker/Setup Dialogue UI")]
    public static void CreateDialogueUI()
    {
        // Find or create Canvas
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasObj = new GameObject("Canvas");
            canvas = canvasObj.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();

            CanvasScaler scaler = canvasObj.GetComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            scaler.matchWidthOrHeight = 0.5f;
        }

        // Create a container for the entire dialogue system
        GameObject dialogueContainer = new GameObject("DialogueContainer");
        dialogueContainer.transform.SetParent(canvas.transform, false);
        RectTransform containerRect = dialogueContainer.AddComponent<RectTransform>();
        containerRect.anchorMin = Vector2.zero;
        containerRect.anchorMax = Vector2.one;
        containerRect.offsetMin = Vector2.zero;
        containerRect.offsetMax = Vector2.zero;

        // Create Character Portrait (OUTSIDE the panel, fixed position)
        GameObject portraitObj = new GameObject("CharacterPortrait");
        portraitObj.transform.SetParent(dialogueContainer.transform, false);

        Image portraitImage = portraitObj.AddComponent<Image>();
        portraitImage.color = new Color(0.5f, 0.8f, 1f, 1f); // Placeholder color
        portraitImage.preserveAspect = true;

        RectTransform portraitRect = portraitObj.GetComponent<RectTransform>();
        // Position portrait at bottom-left, anchored to screen edges (not panel)
        portraitRect.anchorMin = new Vector2(0f, 0f);
        portraitRect.anchorMax = new Vector2(0f, 0f);
        portraitRect.pivot = new Vector2(0f, 0f);
        portraitRect.anchoredPosition = new Vector2(50f, 50f);
        portraitRect.sizeDelta = new Vector2(200f, 200f);

        // Create Character Name (OUTSIDE the panel, fixed position above portrait)
        GameObject nameObj = new GameObject("CharacterName");
        nameObj.transform.SetParent(dialogueContainer.transform, false);

        TextMeshProUGUI nameText = nameObj.AddComponent<TextMeshProUGUI>();
        nameText.text = "CHARACTER";
        nameText.fontSize = 28;
        nameText.fontStyle = FontStyles.Bold;
        nameText.color = Color.white;
        nameText.alignment = TextAlignmentOptions.Center;

        // Add background to name
        GameObject nameBgObj = new GameObject("NameBackground");
        nameBgObj.transform.SetParent(dialogueContainer.transform, false);
        nameBgObj.transform.SetSiblingIndex(nameObj.transform.GetSiblingIndex());

        Image nameBgImage = nameBgObj.AddComponent<Image>();
        nameBgImage.color = new Color(0.15f, 0.15f, 0.2f, 0.9f);

        RectTransform nameBgRect = nameBgObj.GetComponent<RectTransform>();
        nameBgRect.anchorMin = new Vector2(0f, 0f);
        nameBgRect.anchorMax = new Vector2(0f, 0f);
        nameBgRect.pivot = new Vector2(0f, 0f);
        nameBgRect.anchoredPosition = new Vector2(50f, 260f);
        nameBgRect.sizeDelta = new Vector2(200f, 40f);

        // Move name text in front of background
        nameObj.transform.SetAsLastSibling();
        RectTransform nameRect = nameObj.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0f, 0f);
        nameRect.anchorMax = new Vector2(0f, 0f);
        nameRect.pivot = new Vector2(0f, 0f);
        nameRect.anchoredPosition = new Vector2(50f, 260f);
        nameRect.sizeDelta = new Vector2(200f, 40f);

        // Create Dialogue Panel (separate from portrait/name)
        GameObject dialoguePanel = new GameObject("DialoguePanel");
        dialoguePanel.transform.SetParent(dialogueContainer.transform, false);

        Image panelImage = dialoguePanel.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.15f, 0.95f);

        RectTransform panelRect = dialoguePanel.GetComponent<RectTransform>();
        // Panel starts after the portrait area
        panelRect.anchorMin = new Vector2(0f, 0f);
        panelRect.anchorMax = new Vector2(1f, 0f);
        panelRect.pivot = new Vector2(0.5f, 0f);
        panelRect.anchoredPosition = new Vector2(0f, 50f);
        panelRect.sizeDelta = new Vector2(-360f, 250f); // Leave space on left for portrait
        panelRect.offsetMin = new Vector2(280f, 50f); // 280px from left (portrait width + padding)
        panelRect.offsetMax = new Vector2(-50f, 300f); // 50px from right

        // Create Dialogue Text
        GameObject dialogueTextObj = new GameObject("DialogueText");
        dialogueTextObj.transform.SetParent(dialoguePanel.transform, false);

        TextMeshProUGUI dialogueText = dialogueTextObj.AddComponent<TextMeshProUGUI>();
        dialogueText.text = "Dialogue text will appear here...";
        dialogueText.fontSize = 24;
        dialogueText.color = Color.white;
        dialogueText.alignment = TextAlignmentOptions.TopLeft;

        RectTransform textRect = dialogueTextObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.03f, 0.4f);
        textRect.anchorMax = new Vector2(0.97f, 0.95f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Create Choice Buttons Container
        GameObject choicesContainer = new GameObject("ChoicesContainer");
        choicesContainer.transform.SetParent(dialoguePanel.transform, false);
        RectTransform choicesRect = choicesContainer.AddComponent<RectTransform>();
        choicesRect.anchorMin = new Vector2(0.03f, 0.05f);
        choicesRect.anchorMax = new Vector2(0.97f, 0.35f);
        choicesRect.offsetMin = Vector2.zero;
        choicesRect.offsetMax = Vector2.zero;

        // Create 3 Choice Buttons
        GameObject[] choiceButtons = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            choiceButtons[i] = CreateChoiceButton(choicesContainer.transform, i);
        }

        // Create DialogueManager GameObject
        GameObject dialogueManagerObj = new GameObject("DialogueManager");
        DialogueManager dialogueManager = dialogueManagerObj.AddComponent<DialogueManager>();

        // Auto-assign references using reflection
        var panelField = typeof(DialogueManager).GetField("dialoguePanel",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var textField = typeof(DialogueManager).GetField("dialogueText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var buttonsField = typeof(DialogueManager).GetField("choiceButtons",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var portraitField = typeof(DialogueManager).GetField("characterPortrait",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var nameField = typeof(DialogueManager).GetField("characterNameText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (panelField != null) panelField.SetValue(dialogueManager, dialogueContainer); // Use container as the panel to hide/show
        if (textField != null) textField.SetValue(dialogueManager, dialogueText);
        if (buttonsField != null) buttonsField.SetValue(dialogueManager, choiceButtons);
        if (portraitField != null) portraitField.SetValue(dialogueManager, portraitImage);
        if (nameField != null) nameField.SetValue(dialogueManager, nameText);

        // Mark objects as dirty for saving
        EditorUtility.SetDirty(dialogueManager);

        // Select the DialogueManager in hierarchy
        Selection.activeGameObject = dialogueManagerObj;

        Debug.Log("Dialogue UI created successfully! DialogueManager object is selected.");
        Debug.Log("Portrait and Name are now OUTSIDE the dialogue panel - they won't resize with the panel.");
        Debug.Log("Don't forget to assign an Ink JSON file and character portraits to test the dialogue system.");
    }

    private static GameObject CreateChoiceButton(Transform parent, int index)
    {
        GameObject buttonObj = new GameObject($"ChoiceButton{index + 1}");
        buttonObj.transform.SetParent(parent, false);

        // Add Image component for button background
        Image buttonImage = buttonObj.AddComponent<Image>();
        buttonImage.color = new Color(0.2f, 0.3f, 0.5f, 1f);

        // Add Button component
        Button button = buttonObj.AddComponent<Button>();

        // Set button colors
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.2f, 0.3f, 0.5f, 1f);
        colors.highlightedColor = new Color(0.3f, 0.4f, 0.6f, 1f);
        colors.pressedColor = new Color(0.15f, 0.25f, 0.45f, 1f);
        button.colors = colors;

        // Position button
        RectTransform buttonRect = buttonObj.GetComponent<RectTransform>();
        float spacing = 0.33f;
        float offset = index * spacing;

        buttonRect.anchorMin = new Vector2(0f, 1f - (offset + spacing));
        buttonRect.anchorMax = new Vector2(1f, 1f - offset);
        buttonRect.offsetMin = new Vector2(0, 5);
        buttonRect.offsetMax = new Vector2(0, -5);

        // Create text child
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);

        TextMeshProUGUI buttonText = textObj.AddComponent<TextMeshProUGUI>();
        buttonText.text = $"Choice {index + 1}";
        buttonText.fontSize = 20;
        buttonText.color = Color.white;
        buttonText.alignment = TextAlignmentOptions.Center;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = new Vector2(10, 5);
        textRect.offsetMax = new Vector2(-10, -5);

        return buttonObj;
    }
}
