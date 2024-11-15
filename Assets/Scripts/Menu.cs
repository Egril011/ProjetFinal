using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour, OptionMenu
{
    [SerializeField] private Canvas _canvasGame;
    [SerializeField] private TMP_Text _menuText;
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _exitButton;

    private Volume _volume;
    private Sensibility _sensibility;
    private MyPlayer _player;
    private LoadScene _loadScene;

    private void Start()
    {
        _sensibility = FindAnyObjectByType<Sensibility>();
        _volume = FindAnyObjectByType<Volume>();
        _player = FindAnyObjectByType<MyPlayer>();
        _loadScene = FindAnyObjectByType<LoadScene>();

        HideORShowSettings(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideORShowSettings(false);

            _sensibility.SliderSensibility.onValueChanged.AddListener(_sensibility.SensibilityTextUpdate);
            _volume.SliderVolume.onValueChanged.AddListener(_volume.VolumeTextUpdate);
        }

    }

    public void ExitSettings()
    {
        _loadScene.SwitchScene("MainMenu");
    }

    public void HideORShowSettings(bool isVisible)
    {
        _canvasGame.gameObject.SetActive(isVisible);
        _player.CanMove = isVisible;

        _menuText.gameObject.SetActive(!isVisible);
        _saveButton.gameObject.SetActive(!isVisible);
        _exitButton.gameObject.SetActive(!isVisible);
        _sensibility.HideORShowSensibilitySetting(!isVisible);
        _volume.HideORShowVolumeSetting(!isVisible);

        if (isVisible)
        {
            _saveButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else 
        {
            _saveButton.onClick.AddListener(SaveSettings);
            _exitButton.onClick.AddListener(ExitSettings);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void SaveSettings()
    {
        _sensibility.SaveSensibility(_sensibility.SliderSensibility.value);
        _volume.SaveVolume(_volume.SliderVolume.value);

        HideORShowSettings(true);
    }
}
        