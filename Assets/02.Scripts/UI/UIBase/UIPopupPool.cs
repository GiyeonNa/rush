using System.Collections.Generic;
using UnityEngine;

public class UIPopupPool : MonoBehaviour
{
    [Header("�ʱ� Pool ũ��")]
    public int initialPoolSize = 5;

    private Queue<UIPopup> pool = new Queue<UIPopup>();
    private UIPopup popupPrefab;

    private void Awake()
    {
        // �ʱ� Pool ����
        for (int i = 0; i < initialPoolSize; i++)
        {
            AddPopupToPool(CreateNewPopup());
        }
    }

    // UIPopup�� Pool���� ��������
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
            // Pool�� ��������� ���� ����
            return CreateNewPopup();
        }
    }

    // UIPopup�� Pool�� ��ȯ
    public void ReturnPopup(UIPopup popup)
    {
        popup.Close(); // �ݱ� ó��
        popup.gameObject.SetActive(false);
        AddPopupToPool(popup);
    }

    // Pool�� UIPopup �߰�
    private void AddPopupToPool(UIPopup popup)
    {
        pool.Enqueue(popup);
    }

    // �� UIPopup ����
    private UIPopup CreateNewPopup()
    {
        if (popupPrefab == null)
        {
            Debug.LogError("UIPopup �������� �ε���� �ʾҽ��ϴ�.");
            return null;
        }

        UIPopup newPopup = Instantiate(popupPrefab, transform);
        newPopup.gameObject.SetActive(false);
        return newPopup;
    }

    // �� UIPopup ���� (������ ��θ� ���ڷ� �޴� �����ε�)
    private UIPopup CreateNewPopup(string _path)
    {
        // Resources���� ������ �ε�
        popupPrefab = Resources.Load<UIPopup>(_path);
        if (popupPrefab == null)
        {
            Debug.LogError($"UIPopup �������� {_path} ��ο��� ã�� �� �����ϴ�.");
        }
        UIPopup newPopup = Instantiate(popupPrefab, transform);
        newPopup.gameObject.SetActive(false);
        return newPopup;
    }
}
