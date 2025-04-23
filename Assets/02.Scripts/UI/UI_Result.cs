using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Result : UIPopup
{
    [SerializeField]
    private TextMeshProUGUI playerTimeText;
    [SerializeField]
    private TextMeshProUGUI rankText;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject[] stars;


    private void Awake()
    {
        base.Awake();
        SetBtn(restartButton, OnClickRestart);
        SetBtn(backButton, OnClickBack);
    }

    private void Start()
    {
        base.Open();
        ResetData();
    }

    public override void ResetData()
    {
        base.ResetData();
        SetText(playerTimeText, FormatTime(ResultManager.Instance.GetTime()));
        SetText(rankText, ResultManager.Instance.GetRank().ToString());

        int rank = ResultManager.Instance.GetRank();
        for (int i = 0; i < stars.Length; i++)
            SetActive(stars[i], i < (4 - rank));
    }

    private void OnClickRestart()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        SceneManager.LoadScene("Loading");
    }

    private void OnClickBack()
    {
        PlayerPrefs.SetString("NextScene", "Select");
        SceneManager.LoadScene("Loading");
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        return $"{minutes:D2}:{seconds:D2}.{milliseconds:D3}";
    }
}
