using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UI_Start : UIPopup
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button customButton;
    [SerializeField]
    private Button exitButton;
    [SerializeField]
    private GameObject titleImageObject;

    private void Awake()
    {
        base.Awake();
        SetBtn(startButton, OnClickStart);
        SetBtn(customButton, OnClickCustom);
        SetBtn(exitButton, OnClickExit);
    }

    private void Start()
    {
        base.Open();
        SoundManager.Instance.PlayBackgroundMusic();

        if (titleImageObject != null)
        {
            RectTransform rectTransform = titleImageObject.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                float originalY = rectTransform.anchoredPosition.y;
                rectTransform.DOAnchorPosY(originalY + 10f, 1f)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            }
        }
    }


    #region 버튼기능
    private void OnClickStart()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        PlayerPrefs.SetString("LoadingType", "MainMenu");
        PlayerPrefs.SetString("NextScene", "Select");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Loading");
    }

    private void OnClickCustom()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        PlayerPrefs.SetString("LoadingType", "MainMenu");
        PlayerPrefs.SetString("NextScene", "Custom");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Loading");
    }

    private void OnClickExit()
    {
        SoundManager.Instance.PlayButtonPopupSound();
        Application.Quit();
    }
    #endregion
}
