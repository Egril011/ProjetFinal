using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour, OptionMenu
{
    [SerializeField] private string _gameName;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonOption;
    [SerializeField] private Button _buttonSave;
    [SerializeField] private Button _buttonExit;
    [SerializeField] private TMP_Text _text;

    private LoadScene _loadScene;
    private Volume _volume;
    private Sensibility _sensibility;

    private void Start()
    {
        _volume = FindAnyObjectByType<Volume>();
        _sensibility = FindAnyObjectByType <Sensibility>();
        _loadScene = FindAnyObjectByType<LoadScene>();

        HideORShowSettings(true);
    }

    private void OnclickStartGame()
    {
        _loadScene.SwitchScene("Game");
    }

    private void OnclickOption()
    {
        HideORShowSettings(false);

        _sensibility.SliderSensibility.onValueChanged.AddListener(_sensibility.SensibilityTextUpdate);

        _volume.SliderVolume.onValueChanged.AddListener(_volume.VolumeTextUpdate); 
    }
    public void ExitSettings()
    {
        HideORShowSettings(true);
    }

    public void HideORShowSettings(bool isVisible)
    {
       _text.text = $"{_gameName}";
       _buttonStart.gameObject.SetActive(isVisible);
       _buttonOption.gameObject.SetActive(isVisible);
       
       _buttonExit.gameObject.SetActive(!isVisible);
       _buttonSave.gameObject.SetActive(!isVisible);

       _sensibility.HideORShowSensibilitySetting(!isVisible);
       _volume.HideORShowVolumeSetting(!isVisible);

        if (isVisible)
        {
            _buttonStart.onClick.AddListener(OnclickStartGame);
            _buttonOption.onClick.AddListener(OnclickOption);
        }
        else
        {
            _text.text = "Option";
            _buttonStart.onClick.RemoveAllListeners();
            _buttonOption.onClick.RemoveAllListeners();

            _buttonSave.onClick.AddListener(SaveSettings);
            _buttonExit.onClick.AddListener(ExitSettings);
        }
        
    }

    public void SaveSettings()
    {
        _sensibility.SaveSensibility(_sensibility.SliderSensibility.value);
        _volume.SaveVolume(_volume.SliderVolume.value);
    }
}
