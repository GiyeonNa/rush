using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Result : UIPopup
{
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        base.Awake();
        SetBtn(restartButton, OnClickRestart);
        SetBtn(backButton, OnClickBack);
    }

    private void Start()
    {
        base.Open();
    }

    private void OnClickRestart()
    {
        SceneManager.LoadScene("Game");
    }

    private void OnClickBack()
    {
        SceneManager.LoadScene("Select");
    }
}
