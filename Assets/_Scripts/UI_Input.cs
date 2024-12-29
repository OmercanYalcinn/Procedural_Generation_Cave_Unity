using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Input : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;

    [Header("Map Generator")]
    public MapGenerator mapGenerator;

    [Header("Settings Inputs")]
    public InputField widthInput;    // It is a text format with integer input
    public InputField heightInput;   // It is a text format and integer input
    public InputField randomFillPercentInput;   // It is a text format int input
    public InputField seedInput;    // There is no specific input format here
    public Toggle useRandomSeedToggle;  // Checkbox of random seed
    public Scrollbar randomFillPercent_scrollbar;
    public Button applyButton;


    private void Start(){
        // Set default values in the UI
        widthInput.text = "25";
        heightInput.text = "50";
        randomFillPercentInput.text = "3";
        useRandomSeedToggle.isOn = true;

        ShowMainMenu();
    }


    public void ShowMainMenu(){
        mainMenuPanel.SetActive(true);
        settingsMenuPanel.SetActive(false);
    }

    public void ShowSettingsMenu(){
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void ApplySettings()
    {
        // Update map generator settings based on UI input
        int width = int.Parse(widthInput.text);
        int height = int.Parse(heightInput.text);
        int randomFillPercent = int.Parse(randomFillPercentInput.text);
        string seed = seedInput.text;
        bool useRandomSeed = useRandomSeedToggle.isOn;

        mapGenerator.SetWidth(width);
        mapGenerator.SetHeight(height);
        mapGenerator.SetRandomFillPercent(randomFillPercent);
        mapGenerator.SetSeed(seed);
        mapGenerator.SetUseRandomSeed(useRandomSeed);

        ShowMainMenu();
    }

    public void GenerateMap()
    {
        mapGenerator.GenerateMap();
    }
}
