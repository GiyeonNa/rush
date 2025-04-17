using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Net.NetworkInformation;
using System.Collections.Generic;

public class UI_Custom : UIPopup
{
    [Header("차량 선택")]
    [SerializeField]
    private Button carLeftButton;
    [SerializeField]
    private Button carRightButton;
    [SerializeField]
    private TextMeshProUGUI carNameText;

    [Header("차량 색상")]
    [SerializeField]
    private Button colorLeftButton;
    [SerializeField]
    private Button colorRightButton;
    [SerializeField]
    private TextMeshProUGUI colorNameText;

    [Header("설정 버튼")]
    [SerializeField]
    private Button closeButton;
    [SerializeField]
    private Button applyButton;

    [Header("차량 미리보기")]
    [SerializeField]
    private MeshFilter carMesh;
    [SerializeField]
    private MeshRenderer carMaterial;

    [Header("차량 모델,색상 어드레서블")]
    private List<AssetReference> carModels = new List<AssetReference>();
    private List<AssetReference> carColors = new List<AssetReference>();

    private int currentCarIndex = 0;
    private int currentColorIndex = 0;

    private void Awake()
    {
        base.Awake();
        carLeftButton.onClick.AddListener(OnCarLeftButtonClicked);
        carRightButton.onClick.AddListener(OnCarRightButtonClicked);
        colorLeftButton.onClick.AddListener(OnColorLeftButtonClicked);
        colorRightButton.onClick.AddListener(OnColorRightButtonClicked);

        // Dynamically load car models and colors from Addressable Groups
        LoadCarModelsAndColorsFromGroup();
    }

    private void LoadCarModelsAndColorsFromGroup()
    {
        Addressables.LoadResourceLocationsAsync("CarModel").Completed += OnCarModelsLoaded;
        Addressables.LoadResourceLocationsAsync("CarColor", typeof(MeshRenderer)).Completed += OnCarColorsLoaded;
    }

    private void OnCarModelsLoaded(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var location in handle.Result)
                carModels.Add(new AssetReference(location.PrimaryKey));

            if (carModels.Count > 0)
                LoadCarModel(currentCarIndex);
        }
        else
            Debug.LogError("Failed to load car models from Addressable Group.");
    }

    private void OnCarColorsLoaded(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var location in handle.Result)
                carColors.Add(new AssetReference(location.PrimaryKey));

            if (carColors.Count > 0)
                LoadCarColor(currentColorIndex);
        }
        else
            Debug.LogError("Failed to load car colors from Addressable Group.");
    }

    #region 차량겉모습
    private void OnCarLeftButtonClicked()
    {
        currentCarIndex = (currentCarIndex - 1 + carModels.Count) % carModels.Count;
        LoadCarModel(currentCarIndex);
    }

    private void OnCarRightButtonClicked()
    {
        currentCarIndex = (currentCarIndex + 1) % carModels.Count;
        LoadCarModel(currentCarIndex);
    }

    private void LoadCarModel(int index)
    {
        if (carModels != null && index >= 0 && index < carModels.Count)
            carModels[index].LoadAssetAsync<MeshFilter>().Completed += OnCarModelLoaded;
    }

    private void OnCarModelLoaded(AsyncOperationHandle<MeshFilter> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            carMesh = handle.Result;
            Debug.Log("Car model updated.");
        }
        else
            Debug.LogError("Failed to load car model.");
    }
    #endregion

    #region 차량 색상
    private void OnColorLeftButtonClicked()
    {
        currentColorIndex = (currentColorIndex - 1 + carColors.Count) % carColors.Count;
        LoadCarColor(currentColorIndex);
    }

    private void OnColorRightButtonClicked()
    {
        currentColorIndex = (currentColorIndex + 1) % carColors.Count;
        LoadCarColor(currentColorIndex);
    }

    private void LoadCarColor(int index)
    {
        if (carColors != null && index >= 0 && index < carColors.Count)
            carColors[index].LoadAssetAsync<MeshRenderer>().Completed += OnCarColorLoaded;
    }

    private void OnCarColorLoaded(AsyncOperationHandle<MeshRenderer> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            carMaterial = handle.Result;
            Debug.Log("Car color updated.");
        }
        else
            Debug.LogError("Failed to load car color.");

    }
    #endregion

    private void OnClickApplyButton()
    {

    }

    private void OnClickCloseButton()
    {

    }
}
