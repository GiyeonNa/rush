using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : UIPopup
{
    [SerializeField]
    private Button continueButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button giveUpButton;

    private void Awake()
    {
        base.Awake();
        SetBtn(continueButton, OnClickResume);
        SetBtn(restartButton, OnClickRestart);
        SetBtn(giveUpButton, OnClickMainMenu);
    }

    private void Start()
    {
        base.Open();
    }

    private void OnClickResume()
    {
        // Resume the game  
        Time.timeScale = 1f;
        Close();
    }

    private void OnClickRestart()
    {
        // Restart the game  
        Time.timeScale = 1f;
        //����� ���� ���� ������ؾ��Ѵ�.
        SceneManager.LoadScene("GameScene");
        Close();
    }

    private void OnClickMainMenu()
    {
        // Go back to the main menu  
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Close();
    }
}
