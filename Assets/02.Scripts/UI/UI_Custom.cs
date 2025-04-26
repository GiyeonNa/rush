using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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
    private Button backButton;
    [SerializeField]
    private Button applyButton;

    [Header("차량 미리보기")]
    [SerializeField]
    private GameObject carPreview;
    [SerializeField]
    private MeshFilter carMesh;
    [SerializeField]
    private MeshRenderer carMaterial;

    [Header("차량 모델,색상 어드레서블")]
    private List<AssetReference> carModels = new List<AssetReference>();
    private List<AssetReference> carColors = new List<AssetReference>();

    [Header("차량 회전 설정")]
    [SerializeField]
    private float rotationSpeed = 10f;

    private int currentCarIndex = 0;
    private int currentColorIndex = 0;

    private const string SelectedCarModelKey = "SelectedCarModel";
    private const string SelectedCarColorKey = "SelectedCarColor";

    private bool carModelsLoaded = false;
    private bool carColorsLoaded = false;

    private void Awake()
    {
        base.Awake();
       
        SetBtn(carLeftButton, OnCarLeftButtonClicked);
        SetBtn(carRightButton, OnCarRightButtonClicked);
        SetBtn(colorLeftButton, OnColorLeftButtonClicked);
        SetBtn(colorRightButton, OnColorRightButtonClicked);
        SetBtn(applyButton, OnClickApplyButton);
        SetBtn(backButton, OnClickCloseButton);
        SetBtn(closeButton, OnClickCloseButton);

        LoadCarModelsAndColorsFromGroup();
    }


    private void Update()
    {
        RotateCarPreview();
    }

    private void LoadCarModelsAndColorsFromGroup()
    {
        Addressables.LoadResourceLocationsAsync("CarModel", typeof(Mesh)).Completed += handle =>
        {
            OnCarModelsLoaded(handle);
            carModelsLoaded = true;
            CheckAllResourcesLoaded();
        };

        Addressables.LoadResourceLocationsAsync("CarColor", typeof(Material)).Completed += handle =>
        {
            OnCarColorsLoaded(handle);
            carColorsLoaded = true;
            CheckAllResourcesLoaded();
        };
    }

    private void CheckAllResourcesLoaded()
    {
        if (carModelsLoaded && carColorsLoaded)
        {
            OnAllResourcesLoaded();
        }
    }

    private void OnAllResourcesLoaded()
    {
        string savedModelName = PlayerPrefs.GetString(SelectedCarModelKey, null);
        string savedColorName = PlayerPrefs.GetString(SelectedCarColorKey, null);

        if (!string.IsNullOrEmpty(savedModelName))
        {
            for (int i = 0; i < carModels.Count; i++)
            {
                if (carModels[i].AssetGUID.Contains(savedModelName))
                {
                    currentCarIndex = i;
                    break;
                }
            }
        }
        else
        {
            currentCarIndex = 0;
        }
        LoadCarModel(currentCarIndex);

        if (!string.IsNullOrEmpty(savedColorName))
        {
            for (int i = 0; i < carColors.Count; i++)
            {
                if (carColors[i].AssetGUID.Contains(savedColorName))
                {
                    currentColorIndex = i;
                    break;
                }
            }
        }
        else
        {
            currentColorIndex = 0;
        }
        LoadCarColor(currentColorIndex);
    }

    private void OnCarModelsLoaded(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var location in handle.Result)
                carModels.Add(new AssetReference(location.PrimaryKey));
        }
        else
        {
            Debug.LogError("Failed to load car models from Addressable Group.");
        }
    }

    private void OnCarColorsLoaded(AsyncOperationHandle<IList<IResourceLocation>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            foreach (var location in handle.Result)
                carColors.Add(new AssetReference(location.PrimaryKey));
        }
        else
        {
            Debug.LogError("Failed to load car colors from Addressable Group.");
        }
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
        {
            var assetReference = carModels[index];

            SetText(carNameText, assetReference.AssetGUID);

            if (assetReference.OperationHandle.IsValid() && assetReference.OperationHandle.Status == AsyncOperationStatus.Succeeded)
                carMesh.mesh = assetReference.OperationHandle.Result as Mesh;
            else
                assetReference.LoadAssetAsync<Mesh>().Completed += OnCarModelLoaded;
        }
    }

    private void OnCarModelLoaded(AsyncOperationHandle<Mesh> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
            carMesh.mesh = handle.Result;
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
        {
            var assetReference = carColors[index];

            SetText(colorNameText, assetReference.AssetGUID.Split("_")[1]);

            if (assetReference.OperationHandle.IsValid() && assetReference.OperationHandle.Status == AsyncOperationStatus.Succeeded)
                carMaterial.material = assetReference.OperationHandle.Result as Material;
            else
                carColors[index].LoadAssetAsync<Material>().Completed += OnCarColorLoaded;
        }
 
    }

    private void OnCarColorLoaded(AsyncOperationHandle<Material> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            carMaterial.material = handle.Result;
            Debug.Log("Car color updated.");
        }
        else
            Debug.LogError("Failed to load car color.");

    }
    #endregion

    private void SaveSelectedCarData()
    {
        if (carMesh.mesh != null)
        {
            // Remove " Instance" from the car model name before saving
            string modelName = carMesh.mesh.name.Replace(" Instance", "");
            PlayerPrefs.SetString(SelectedCarModelKey, modelName);
        }

        if (carMaterial.material != null)
        {
            // Remove " (Instance)" from the car color name before saving
            string colorName = carMaterial.material.name.Replace(" (Instance)", "");
            PlayerPrefs.SetString(SelectedCarColorKey, colorName);
        }

        PlayerPrefs.Save();
    }

    //연출
    private void RotateCarPreview()
    {
        if (carPreview != null)
        {
            carPreview.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Apply 버튼 클릭 시 데이터 저장
    /// </summary>
    private void OnClickApplyButton()
    {
        if (carMesh.mesh != null && carMaterial.material != null)
        {
            SaveSelectedCarData();
            PlayerPrefs.SetString("LoadingType", "MainMenu");
            PlayerPrefs.SetString("NextScene", "Start");
            PlayerPrefs.Save();
            SceneManager.LoadScene("Loading");
        }
    }

    /// <summary>
    /// Close 버튼 클릭 시 정보 저장하지 않고 돌아가기
    /// </summary>
    private void OnClickCloseButton()
    {
        PlayerPrefs.SetString("LoadingType", "MainMenu");
        PlayerPrefs.SetString("NextScene", "Start");
        PlayerPrefs.Save();
        SceneManager.LoadScene("Loading");
    }
}
