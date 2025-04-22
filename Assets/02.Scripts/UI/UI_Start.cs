using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Start : UIPopup
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button customButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private Button optionButton;

    private void Awake()
    {
        base.Awake();
        SetBtn(startButton, OnClickStart);
        SetBtn(customButton, OnClickCustom);
        SetBtn(exitButton, OnClickExit);
        SetBtn(optionButton, OnClickOption);
    }

    private void Start()
    {
        base.Open();
    }


    #region 버튼기능
    private void OnClickStart()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        SceneManager.LoadScene("Select");
    }

    private void OnClickCustom()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        SceneManager.LoadScene("Custom");
    }

    private void OnClickExit()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        Application.Quit();
    }

    private void OnClickOption()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        Debug.Log("옵션 버튼 클릭됨");
    }
    #endregion
}
