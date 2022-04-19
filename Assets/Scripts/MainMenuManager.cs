using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Main Menu Panel List")]
    public GameObject MainPanel;
    public GameObject HTPPanel;
    public GameObject TimerPanel;
    public GameObject ChooseBallPanel;

    // Start is called before the first frame update
    void Start()
    {
        MainPanel.SetActive(true);
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        ChooseBallPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SinglePlayerButton()
    {
        GameData.instance.isSinglePlayer = true;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void MultiPlayerButton()
    {
        GameData.instance.isSinglePlayer = false;
        TimerPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void BackButton()
    {
        HTPPanel.SetActive(false);
        TimerPanel.SetActive(false);
        ChooseBallPanel.SetActive(false);
        MainPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();
    }

    public void SetTimerButton(float Timer)
    {
        GameData.instance.gameTimer = Timer;
        TimerPanel.SetActive(false);
        MainPanel.SetActive(false);
        ChooseBallPanel.SetActive(true);
        HTPPanel.SetActive(false);
        SoundManager.instance.UIClickSfx();
    }

    public void ChooseBallButton( string ballName)
    {
        GameData.instance.ball = ballName;
        TimerPanel.SetActive(false);
        MainPanel.SetActive(false);
        ChooseBallPanel.SetActive(false);
        HTPPanel.SetActive(true);
        SoundManager.instance.UIClickSfx();

    }

    public void StartButton()
    {
        SceneManager.LoadScene("Gameplay");
        SoundManager.instance.UIClickSfx();
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
