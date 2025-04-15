using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public UIPopupPool popupPool;

    public void ShowPopup()
    {
        UIPopup popup = popupPool.GetPopup();
        popup.Open();
        popup.m_callBack = () => Debug.Log("Popup Closed");
    }

    public void ClosePopup(UIPopup popup)
    {
        popupPool.ReturnPopup(popup);
    }
}
