using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject pauseMenu = null;
    [SerializeField]
    GameObject confirmationPanel = null;
    [SerializeField]
    GameObject mainMenuText = null;
    [SerializeField]
    GameObject desktopText = null;
    [SerializeField]
    GameObject mainMenuConfirmButton = null;
    [SerializeField]
    GameObject desktopConfirmButton = null;
    [SerializeField]
    GameObject savePanel = null;
    [SerializeField]
    GameObject loadPanel = null;
    [SerializeField]
    GameObject settingsPanel = null;

    public static bool gamePause;

    // Start is called before the first frame update
    void Start()
    {
        ResetPauseMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            gamePause = !gamePause;
            pauseMenu.SetActive(gamePause);
        }
    }

    void ResetPauseMenu()
    {
        pauseMenu.SetActive(false);
        confirmationPanel.SetActive(false);
        mainMenuText.SetActive(false);
        desktopText.SetActive(false);
        mainMenuConfirmButton.SetActive(false);
        desktopConfirmButton.SetActive(false);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
        settingsPanel.SetActive(false);

        gamePause = false;
    }

    public void Button(int _buttonNum)
    {
        switch (_buttonNum)
        {
            default:
                break;

            // Case for the Resume Button
            case 0:
                gamePause = false;
                pauseMenu.SetActive(gamePause);
                break;

            // Case for the Save Game Button
            case 1:
                //savePanel.SetActive(true);
                break;

            // Case for the Load Game Button
            case 2:
                //loadPanel.SetActive(true);
                break;

            // Case for the Settings Button
            case 3:
                settingsPanel.SetActive(true);
                break;

            // Case for the Quit to Main Menu button
            case 4:
                confirmationPanel.SetActive(true);
                mainMenuText.SetActive(true);
                mainMenuConfirmButton.SetActive(true);
                break;

            // Case for the Quit to Desktop Button
            case 5:
                confirmationPanel.SetActive(true);
                desktopText.SetActive(true);
                desktopConfirmButton.SetActive(true);
                break;

            // Case for the Negative button for quiting confirmation
            case 6:
                confirmationPanel.SetActive(false);
                mainMenuText.SetActive(false);
                desktopText.SetActive(false);
                mainMenuConfirmButton.SetActive(false);
                desktopConfirmButton.SetActive(false);
                break;

            // Case for Confirmation button for Quit to Main menu
            case 7:
                // Load Main menu Scene here
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
                break;

            // Case for Confirmation button for quit to desktop
            case 8:
                Application.Quit();
                break;
        }
    }
}
