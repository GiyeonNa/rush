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

    #region ��ư���
    private void OnClickBack()
    {
        // �ڷΰ��� ����
        SceneManager.LoadScene("Start");
    }

    private void OnClickOption()
    {
        // �ɼ� ����
        Debug.Log("�ɼ� ��ư Ŭ����");
    }
    private void OnClickStart()
    {
        // ���� ���� ����
        SceneManager.LoadScene("DriveTest");
    }
    #endregion

}
