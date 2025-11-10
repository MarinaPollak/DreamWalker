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

    [Header("Tab Buttons")]
    public Button audioTabButton;
    public Button displayTabButton;

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

        // Update button visuals
        UpdateTabButtonColors(audioTabButton, displayTabButton);
    }

    void ShowDisplayPanel()
    {
        audioPanel.SetActive(false);
        displayPanel.SetActive(true);

        // Update button visuals
        UpdateTabButtonColors(displayTabButton, audioTabButton);
    }

    void UpdateTabButtonColors(Button activeButton, Button inactiveButton)
    {
        ColorBlock activeColors = activeButton.colors;
        activeColors.normalColor = new Color(0.7f, 0.7f, 0.7f);
        activeButton.colors = activeColors;

        ColorBlock inactiveColors = inactiveButton.colors;
        inactiveColors.normalColor = Color.white;
        inactiveButton.colors = inactiveColors;
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
