using UnityEngine;

public class MapSettingsManager : MonoBehaviour
{
    [SerializeField] private MapSettings settings;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private TileMapGenerator mapGenerator;
    
    private void Start()
    {
        if (uiManager != null)
        {
            // Initialize UI with current settings
            uiManager.InitializeUI(settings);
            // Subscribe to UI events
            uiManager.OnSettingsChanged += ApplySettings;
        }
    }

    public void ApplySettings(MapSettings newSettings)
    {
        settings = newSettings;
        if (mapGenerator != null)
        {
            mapGenerator.UpdateSettings(settings);
            mapGenerator.RegenerateMap();
        }
    }

    public MapSettings GetCurrentSettings()
    {
        return settings;
    }
}
