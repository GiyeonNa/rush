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


    #region ��ư���
    private void OnClickStart()
    {
        // ���� ���� ����
        SceneManager.LoadScene("GameScene");
    }

    private void OnClickCustom()
    {
        // Ŀ���͸���¡ ����
        Debug.Log("Ŀ���͸���¡ ��ư Ŭ����");
    }

    private void OnClickExit()
    {
        // ���� ���� ����
        Application.Quit();
    }

    private void OnClickOption()
    {
        // �ɼ� ����
        Debug.Log("�ɼ� ��ư Ŭ����");
    }
    #endregion
}
