using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class SettingsManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject audioPanel;
    public GameObject displayPanel;
    public GameObject dialoguePanel;

    [Header("Tab Buttons")]
    public Button audioTabButton;
    public Button displayTabButton;
    public Button dialogueTabButton;

    [Header("Audio Settings")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public TextMeshProUGUI masterVolumeText;
    public TextMeshProUGUI musicVolumeText;
    public TextMeshProUGUI sfxVolumeText;

    [Header("Display Settings")]
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // Initialize tabs
        ShowAudioPanel();

        // Setup tab buttons
        audioTabButton.onClick.AddListener(ShowAudioPanel);
        displayTabButton.onClick.AddListener(ShowDisplayPanel);
        if (dialogueTabButton != null)
        {
            dialogueTabButton.onClick.AddListener(ShowDialoguePanel);
        }

        // Setup audio sliders BEFORE loading settings
        masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);

        // Setup display settings BEFORE loading settings
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        SetupResolutionDropdown();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);

        // Load saved settings (this will trigger the listeners and update the text)
        LoadSettings();
    }

    void ShowAudioPanel()
    {
        audioPanel.SetActive(true);
        displayPanel.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Update button visuals
        UpdateTabButtonColors(audioTabButton, displayTabButton, dialogueTabButton);
    }

    void ShowDisplayPanel()
    {
        audioPanel.SetActive(false);
        displayPanel.SetActive(true);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Update button visuals
        UpdateTabButtonColors(displayTabButton, audioTabButton, dialogueTabButton);
    }

    void ShowDialoguePanel()
    {
        audioPanel.SetActive(false);
        displayPanel.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(true);

        // Update button visuals
        UpdateTabButtonColors(dialogueTabButton, audioTabButton, displayTabButton);
    }

    void UpdateTabButtonColors(Button activeButton, params Button[] inactiveButtons)
    {
        // Active tab - Navy blue background
        if (activeButton != null)
        {
            ColorBlock activeColors = activeButton.colors;
            activeColors.normalColor = new Color(0.1f, 0.2f, 0.5f, 0.9f); // Navy blue with high opacity
            activeButton.colors = activeColors;

            // Set active tab text to white
            TextMeshProUGUI activeText = activeButton.GetComponentInChildren<TextMeshProUGUI>();
            if (activeText != null)
            {
                activeText.color = Color.white;
            }
        }

        // Inactive tabs - Glass-like appearance
        foreach (Button inactiveButton in inactiveButtons)
        {
            if (inactiveButton != null)
            {
                ColorBlock inactiveColors = inactiveButton.colors;
                inactiveColors.normalColor = new Color(0.95f, 0.95f, 1f, 0.35f); // Semi-transparent glass
                inactiveButton.colors = inactiveColors;

                // Set inactive tab text to darker color for visibility
                TextMeshProUGUI inactiveText = inactiveButton.GetComponentInChildren<TextMeshProUGUI>();
                if (inactiveText != null)
                {
                    inactiveText.color = new Color(0.2f, 0.2f, 0.2f, 1f); // Dark gray
                }
            }
        }
    }

    // Audio Settings
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
        masterVolumeText.text = Mathf.RoundToInt(volume * 100) + "%";
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolumeText.text = Mathf.RoundToInt(volume * 100) + "%";
        PlayerPrefs.SetFloat("MusicVolume", volume);
        // You can implement this with your audio mixer or FMOD
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolumeText.text = Mathf.RoundToInt(volume * 100) + "%";
        PlayerPrefs.SetFloat("SFXVolume", volume);
        // You can implement this with your audio mixer or FMOD
    }

    // Display Settings
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRateRatio.value.ToString("F0") + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    void LoadSettings()
    {
        // Load audio settings
        float masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        // Setting slider values will automatically trigger the onValueChanged listeners
        // which will update the text and audio settings
        masterVolumeSlider.value = masterVolume;
        musicVolumeSlider.value = musicVolume;
        sfxVolumeSlider.value = sfxVolume;

        // Load display settings
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;

        int savedResolution = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        if (savedResolution < resolutions.Length)
        {
            resolutionDropdown.value = savedResolution;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
