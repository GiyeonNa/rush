using System.Collections.Generic;
using UnityEngine;

public class UIPopupPool : MonoBehaviour
{
    [Header("초기 Pool 크기")]
    public int initialPoolSize = 5;

    private Queue<UIPopup> pool = new Queue<UIPopup>();
    private UIPopup popupPrefab;

    private void Awake()
    {
        // 초기 Pool 생성
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddPopupToPool(CreateNewPopup());
        }
    }

    // UIPopup을 Pool에서 가져오기
    public UIPopup GetPopup()
    {
        if (pool.Count > 0)
        {
            UIPopup popup = pool.Dequeue();
            popup.gameObject.SetActive(true);
            return popup;
        }
        else
        {
            // Pool이 비어있으면 새로 생성
            return CreateNewPopup();
        }
    }

    // UIPopup을 Pool로 반환
    public void ReturnPopup(UIPopup popup)
    {
        popup.Close(); // 닫기 처리
        popup.gameObject.SetActive(false);
        AddPopupToPool(popup);
    }

    // Pool에 UIPopup 추가
    private void AddPopupToPool(UIPopup popup)
    {
        pool.Enqueue(popup);
    }

    // 새 UIPopup 생성
    private UIPopup CreateNewPopup()
    {
        if (popupPrefab == null)
        {
            Debug.LogError("UIPopup 프리팹이 로드되지 않았습니다.");
            return null;
        }

        UIPopup newPopup = Instantiate(popupPrefab, transform);
        newPopup.gameObject.SetActive(false);
        return newPopup;
    }

    // 새 UIPopup 생성 (프리팹 경로를 인자로 받는 오버로드)
    private UIPopup CreateNewPopup(string _path)
    {
        // Resources에서 프리팹 로드
        popupPrefab = Resources.Load<UIPopup>(_path);
        if (popupPrefab == null)
        {
            Debug.LogError($"UIPopup 프리팹을 {_path} 경로에서 찾을 수 없습니다.");
        }
        UIPopup newPopup = Instantiate(popupPrefab, transform);
        newPopup.gameObject.SetActive(false);
        return newPopup;
    }
}
