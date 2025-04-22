using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Stage : UIPopup
{
    [SerializeField]
    private TextMeshProUGUI stageText;
    [SerializeField]
    private Image stageImage;
    [SerializeField]
    private TextMeshProUGUI playerTimeText;

    private void Awake()
    {
        base.Awake();
    }
    private void Init()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
            rectTransform.sizeDelta = new Vector2(630, 500); // 원하는 크기로 설정
    }
    private void Start()
    {
        base.Open();
        Init();
    }

    public void SetStageData(StageSO stageData)
    {
        if (stageData != null)
        {
            stageText.text = stageData.stageName;
            playerTimeText.text = $"Best Time: {stageData.bestTime:F2}s";
            stageImage.sprite = stageData.stageImage;
        }
    }
}

