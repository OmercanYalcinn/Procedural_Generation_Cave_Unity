using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMPro.TMP_InputField widthInput;
    [SerializeField] private TMPro.TMP_InputField heightInput;
    [SerializeField] private TMPro.TMP_InputField seedInput;
    [SerializeField] private UnityEngine.UI.Toggle randomSeedToggle;
    [SerializeField] private UnityEngine.UI.Slider fillPercentSlider;
    [SerializeField] private TMPro.TextMeshProUGUI fillPercentText;
    [SerializeField] private UnityEngine.UI.Button generateButton;

    public event Action<MapSettings> OnSettingsChanged;
    private MapSettings currentSettings;

    public void InitializeUI(MapSettings initialSettings){
        currentSettings = initialSettings;
        
        // Set initial UI values
        widthInput.text = initialSettings.width.ToString();
        heightInput.text = initialSettings.height.ToString();
        seedInput.text = initialSettings.seed.ToString();
        randomSeedToggle.isOn = initialSettings.useRandomSeed;
        fillPercentSlider.value = initialSettings.fillPercent;
        
        // Add listeners
        widthInput.onValueChanged.AddListener(OnWidthChanged);
        heightInput.onValueChanged.AddListener(OnHeightChanged);
        seedInput.onValueChanged.AddListener(OnSeedChanged);
        randomSeedToggle.onValueChanged.AddListener(OnRandomSeedToggled);
        fillPercentSlider.onValueChanged.AddListener(OnFillPercentChanged);
        generateButton.onClick.AddListener(OnGenerateClicked);
        
        UpdateFillPercentText();
    }

    private void OnWidthChanged(string value){
        if (int.TryParse(value, out int newWidth))
        {
            currentSettings.width = Mathf.Clamp(newWidth, 1, 100);
            NotifySettingsChanged();
        }
    }

    private void OnHeightChanged(string value){
        if (int.TryParse(value, out int newHeight))
        {
            currentSettings.height = Mathf.Clamp(newHeight, 1, 100);
            NotifySettingsChanged();
        }
    }

    private void OnSeedChanged(string value){
        if (int.TryParse(value, out int newSeed))
        {
            currentSettings.seed = newSeed;
            NotifySettingsChanged();
        }
    }

    private void OnRandomSeedToggled(bool isRandom){
        currentSettings.useRandomSeed = isRandom;
        seedInput.interactable = !isRandom;
        if (isRandom)
        {
            currentSettings.seed = UnityEngine.Random.Range(0, int.MaxValue);
            seedInput.text = currentSettings.seed.ToString();
        }
        NotifySettingsChanged();
    }

    private void OnFillPercentChanged(float value){
        currentSettings.fillPercent = value;
        UpdateFillPercentText();
        NotifySettingsChanged();
    }

    private void UpdateFillPercentText(){
        fillPercentText.text = $"Fill Percent: {currentSettings.fillPercent:F1}%";
    }

    private void OnGenerateClicked(){
        NotifySettingsChanged();
    }

    private void NotifySettingsChanged(){
        OnSettingsChanged?.Invoke(currentSettings);
    }
}
