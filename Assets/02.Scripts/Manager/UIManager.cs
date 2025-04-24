using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, GameObject> cachedUIs = new Dictionary<string, GameObject>();

    // �̸� �ε��� UI Ű ���
    [SerializeField]
    private List<string> preloadUIKeys;

    private void Awake()
    {
        base.Awake();
        PreloadUIs();
    }

    private void PreloadUIs()
    {
        foreach (string uiKey in preloadUIKeys)
        {
            // Check if the UI is already cached
            if (cachedUIs.ContainsKey(uiKey))
            {
                Debug.Log($"UI already cached: {uiKey}, skipping preload.");
                continue;
            }

            // Addressables�� UI �ε�
            Addressables.LoadAssetAsync<GameObject>(uiKey).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    // �ε�� UI�� ĳ��
                    GameObject uiInstance = Instantiate(handle.Result, transform);
                    uiInstance.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
                    cachedUIs[uiKey] = uiInstance;
                }
                else
                {
                    Debug.LogError($"Failed to preload UI: {uiKey}");
                }
            };
        }
    }

    public void ShowUI(string uiKey)
    {
        Canvas canvas = Object.FindFirstObjectByType<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("No Canvas found in the scene.");
            return;
        }

        if (cachedUIs.ContainsKey(uiKey))
        {
            // �̹� �ε�� UI�� �ִٸ� Ȱ��ȭ
            GameObject uiInstance = cachedUIs[uiKey];
            uiInstance.transform.SetParent(canvas.transform, false); // Set as child of canvas
            uiInstance.GetComponent<UIPopup>().Open();
        }
        else
        {
            // Addressables�� UI �ε�
            Addressables.LoadAssetAsync<GameObject>(uiKey).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject uiInstance = Instantiate(handle.Result);
                    uiInstance.transform.SetParent(canvas.transform, false); // Set as child of canvas
                    cachedUIs[uiKey] = uiInstance;
                    uiInstance.SetActive(true);
                }
                else
                {
                    Debug.LogError($"Failed to load UI: {uiKey}");
                }
            };
        }
    }

    public void HideUI(string uiKey, GameObject uiInstance)
    {
        if (cachedUIs.ContainsKey(uiKey))
        {
            cachedUIs[uiKey] = uiInstance;
            uiInstance.transform.SetParent(transform, false);
        }
    }
}
