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
        // 게임 시작 로직
        SceneManager.LoadScene("GameScene");
    }

    private void OnClickCustom()
    {
        // 커스터마이징 로직
        Debug.Log("커스터마이징 버튼 클릭됨");
    }

    private void OnClickExit()
    {
        // 게임 종료 로직
        Application.Quit();
    }

    private void OnClickOption()
    {
        // 옵션 로직
        Debug.Log("옵션 버튼 클릭됨");
    }
    #endregion
}
