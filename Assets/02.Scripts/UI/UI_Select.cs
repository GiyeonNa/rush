using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Select : UIPopup
{
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button optionButton;
    [SerializeField]
    private Button startButton;

    private void Awake()
    {
        base.Awake();
        SetBtn(backButton, OnClickBack);
        SetBtn(optionButton, OnClickOption);
        SetBtn(startButton, OnClickStart);
    }

    private void Init()
    {
    }

    private void Start()
    {
        Init();
        base.Open();
        
    }

    #region 버튼기능
    private void OnClickBack()
    {
        // 뒤로가기 로직
        SceneManager.LoadScene("Start");
    }

    private void OnClickOption()
    {
        // 옵션 로직
        Debug.Log("옵션 버튼 클릭됨");
    }
    private void OnClickStart()
    {
        // 게임 시작 로직
        SceneManager.LoadScene("DriveTest");
    }
    #endregion

}
