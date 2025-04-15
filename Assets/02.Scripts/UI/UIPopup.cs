using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopup : UIBase
{
    [Header("===�ݱ� ��ư===")]
    public Button[] btnClose;
    public System.Action m_callBack;

    [Header("�׻� �ֻ������ �������� ���� üũ")]
    public bool m_isSortLast = true;

    protected bool m_isClose = false;

    //���� ��� ����
    protected void DelectAllChild(Transform tr)
    {
        var Allchild = tr.GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < Allchild.Length; i++)
        {
            Destroy(Allchild[i].gameObject);
        }
    }

    //�ݱ� ��ư �̺�Ʈ �Ҵ�
    protected virtual void Awake()
    {
        for (int i = 0; i < btnClose.Length; ++i)
        {
            SetBtn(btnClose[i], OnClick_Close);
        }
    }

    //���� �� ��ġ����
    public override void Open()
    {
        base.Open();

        transform.localScale = Vector3.one;

        m_isClose = false;
        if (m_isSortLast)
            SetSortLast();
        SetStretch();
    }

    //m_isSortLast ������ �������� ����
    public void SetSortLast()
    {
        transform.SetAsLastSibling();
    }

    //ũ������
    public void SetStretch()
    {
        RectTransform _rect = gameObject.GetComponent<RectTransform>();
        _rect.sizeDelta = Vector2.zero;
    }

    //��ư Ŭ�� �� UI �ݱ�
    public virtual void OnClick_Close()
    {
        Close();
    }

    //�ݱ�
    public override void Close()
    {
        base.Close();
        if (m_isClose == false)
        {
            m_callBack?.Invoke();
            m_callBack = null;
        }
        m_isClose = true;
    }
}
