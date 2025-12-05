using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class AddDialoguePanelToScenes : MonoBehaviour
{
    [MenuItem("DreamWalker/Add Dialogue Panel to BedroomScene")]
    public static void AddDialoguePanelToBedroom()
    {
        AddDialoguePanelToScene("Assets/Scenes/BedroomScene.unity");
    }

    [MenuItem("DreamWalker/Add Dialogue Panel to AlleyWay")]
    public static void AddDialoguePanelToAlley()
    {
        AddDialoguePanelToScene("Assets/Scenes/AlleyWay.unity");
    }

    [MenuItem("DreamWalker/Add Dialogue Panel to WorkScene")]
    public static void AddDialoguePanelToWork()
    {
        AddDialoguePanelToScene("Assets/Scenes/WorkScene.unity");
    }

    [MenuItem("DreamWalker/Add Dialogue Panel to All Scenes")]
    public static void AddDialoguePanelToAllScenes()
    {
        AddDialoguePanelToScene("Assets/Scenes/BedroomScene.unity");
        AddDialoguePanelToScene("Assets/Scenes/AlleyWay.unity");
        AddDialoguePanelToScene("Assets/Scenes/WorkScene.unity");
        Debug.Log("Dialogue panels added to BedroomScene, AlleyWay, and WorkScene!");
    }

    private static void AddDialoguePanelToScene(string scenePath)
    {
        // Save current scene if needed
        if (EditorSceneManager.GetActiveScene().isDirty)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        }

        // Open the target scene
        Scene scene = EditorSceneManager.OpenScene(scenePath);
        Debug.Log($"Opened scene: {scene.name}");

        // Check if DialogueManager already exists
        DialogueManager existingManager = Object.FindAnyObjectByType<DialogueManager>();
        if (existingManager != null)
        {
            Debug.LogWarning($"DialogueManager already exists in {scene.name}. Skipping...");
            return;
        }

        // Find or create Canvas
        Canvas canvas = Object.FindAnyObjectByType<Canvas>();
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

            Debug.Log($"Created new Canvas in {scene.name}");
        }

        // Create Dialogue Panel
        GameObject dialoguePanel = new GameObject("DialoguePanel");
        dialoguePanel.transform.SetParent(canvas.transform, false);

        Image panelImage = dialoguePanel.AddComponent<Image>();
        panelImage.color = new Color(0.1f, 0.1f, 0.15f, 0.95f);

        RectTransform panelRect = dialoguePanel.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.1f, 0.1f);
        panelRect.anchorMax = new Vector2(0.9f, 0.4f);
        panelRect.offsetMin = Vector2.zero;
        panelRect.offsetMax = Vector2.zero;

        // Create Character Portrait Container
        GameObject portraitContainer = new GameObject("CharacterPortrait");
        portraitContainer.transform.SetParent(dialoguePanel.transform, false);

        Image portraitImage = portraitContainer.AddComponent<Image>();
        portraitImage.color = new Color(0.5f, 0.8f, 1f, 1f); // Placeholder light blue

        RectTransform portraitRect = portraitContainer.GetComponent<RectTransform>();
        portraitRect.anchorMin = new Vector2(0.02f, 0.4f);
        portraitRect.anchorMax = new Vector2(0.22f, 0.95f);
        portraitRect.offsetMin = Vector2.zero;
        portraitRect.offsetMax = Vector2.zero;

        // Create Character Name Text
        GameObject nameTextObj = new GameObject("CharacterName");
        nameTextObj.transform.SetParent(dialoguePanel.transform, false);

        TextMeshProUGUI nameText = nameTextObj.AddComponent<TextMeshProUGUI>();
        nameText.text = "MARA";
        nameText.fontSize = 20;
        nameText.fontStyle = FontStyles.Bold;
        nameText.color = Color.white;
        nameText.alignment = TextAlignmentOptions.Center;

        RectTransform nameRect = nameTextObj.GetComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0.02f, 0.35f);
        nameRect.anchorMax = new Vector2(0.22f, 0.4f);
        nameRect.offsetMin = Vector2.zero;
        nameRect.offsetMax = Vector2.zero;

        // Create Dialogue Text
        GameObject dialogueTextObj = new GameObject("DialogueText");
        dialogueTextObj.transform.SetParent(dialoguePanel.transform, false);

        TextMeshProUGUI dialogueText = dialogueTextObj.AddComponent<TextMeshProUGUI>();
        dialogueText.text = "Dialogue text will appear here...";
        dialogueText.fontSize = 24;
        dialogueText.color = Color.white;
        dialogueText.alignment = TextAlignmentOptions.TopLeft;

        RectTransform textRect = dialogueTextObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.25f, 0.4f); // Moved right to make room for portrait
        textRect.anchorMax = new Vector2(0.95f, 0.95f);
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        // Create Choice Buttons Container
        GameObject choicesContainer = new GameObject("ChoicesContainer");
        choicesContainer.transform.SetParent(dialoguePanel.transform, false);
        RectTransform choicesRect = choicesContainer.AddComponent<RectTransform>();
        choicesRect.anchorMin = new Vector2(0.05f, 0.05f);
        choicesRect.anchorMax = new Vector2(0.95f, 0.35f);
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
        var nameTextField = typeof(DialogueManager).GetField("characterNameText",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (panelField != null) panelField.SetValue(dialogueManager, dialoguePanel);
        if (textField != null) textField.SetValue(dialogueManager, dialogueText);
        if (buttonsField != null) buttonsField.SetValue(dialogueManager, choiceButtons);
        if (portraitField != null) portraitField.SetValue(dialogueManager, portraitImage);
        if (nameTextField != null) nameTextField.SetValue(dialogueManager, nameText);

        // Mark objects as dirty and save scene
        EditorUtility.SetDirty(dialogueManager);
        EditorSceneManager.MarkSceneDirty(scene);
        EditorSceneManager.SaveScene(scene);

        Debug.Log($"Dialogue UI created successfully in {scene.name}!");
        Debug.Log("Don't forget to assign Ink JSON files to your DialogueTriggers.");
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
