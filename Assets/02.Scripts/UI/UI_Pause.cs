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

    private string stageDataJson;

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
        stageDataJson = JsonUtility.ToJson(RecordManager.Instance.Record);
    }

    public override void ResetData()
    {

    }

    private void OnClickResume()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayButtonPopupSound();
        Close();
    }

    private void OnClickRestart()
    {
        Time.timeScale = 1f;
        //로딩부터 다시 시작
        SoundManager.Instance.PlayButtonPopupSound();
        PlayerPrefs.SetString("CurrentStageData", stageDataJson);
        PlayerPrefs.SetString("NextScene", RecordManager.Instance.Record.stageName);
        SceneManager.LoadScene("Loading");
        Close();
    }

    private void OnClickMainMenu()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayButtonPopupSound();
        SceneManager.LoadScene("MainMenu");
        Close();
    }

    public override void Close()
    {
        var name = this.gameObject.name.Split('(')[0];
        UIManager.Instance.HideUI(name, this.gameObject);
        base.Close();
    }


}
