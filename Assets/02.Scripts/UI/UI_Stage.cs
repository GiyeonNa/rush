using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

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
            rectTransform.sizeDelta = new Vector2(630, 500); // Adjust size
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
            SetText(stageText, stageData.stageName);
            SetText(playerTimeText, $"Best Time: {stageData.bestTime:F2}s");
            SetIcon(stageImage, stageData.stageImage);
        }
    }
}

