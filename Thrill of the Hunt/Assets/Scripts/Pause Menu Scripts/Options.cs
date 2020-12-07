using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField]
    GameObject settingsPanel = null;
    [SerializeField]
    GameObject soundPanel = null;
    [SerializeField]
    GameObject videoPanel = null;
    [SerializeField]
    GameObject graphicsPanel = null;
    [SerializeField]
    GameObject keybindingsPanel = null;

    public Dropdown resolutionDropdown;

    public AudioMixer audiomixer;

    Resolution[] resolutions;

    //resolution
    void Start()
    {
        ResetOptions();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
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
    }

    //fullscreen toggle
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //quality
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //volume
    public void SetMaster(float mastervol)
    {
        audiomixer.SetFloat("Master", mastervol);
    }

    public void SetMusic(float musicvol)
    {
        audiomixer.SetFloat("Music", musicvol);
    }

    public void SetSFX(float sfxvol)
    {
        audiomixer.SetFloat("Sound Effects", sfxvol);
    }

    public void SetDialouge(float sfxvol)
    {
        audiomixer.SetFloat("Dialouge", sfxvol);
    }

    private void ResetOptions()
    {
        settingsPanel.SetActive(false);
        soundPanel.SetActive(true);
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        keybindingsPanel.SetActive(false);
    }

    private void AllOffOptions()
    {
        settingsPanel.SetActive(true);
        soundPanel.SetActive(false);
        videoPanel.SetActive(false);
        graphicsPanel.SetActive(false);
        keybindingsPanel.SetActive(false);
    }

    public void Button(int _buttonNum)
    {
        switch (_buttonNum)
        {
            default:
                break;

            // Sound Button
            case 0:
                AllOffOptions();
                soundPanel.SetActive(true);
                break;

            // Video Button
            case 1:
                AllOffOptions();
                videoPanel.SetActive(true);
                break;

            // Graphics Button
            case 2:
                AllOffOptions();
                graphicsPanel.SetActive(true);
                break;

            // Keybindings Button
            case 3:
                AllOffOptions();
                keybindingsPanel.SetActive(true);
                break;

            // Back Button
            case 4:
                ResetOptions();
                break;
        }
    }
}
