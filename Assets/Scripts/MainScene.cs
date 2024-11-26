using System;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField] private string _gameName;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonOption;
    [SerializeField] private Button _buttonSave;
    [SerializeField] private Button _buttonExit;

    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _menuOption;

    [SerializeField] private Slider _sliderSensibility;
    [SerializeField] private Slider _sliderVolume;

    [SerializeField] private TMP_Text _textSensibilityNb;
    [SerializeField] private TMP_Text _textVolumeNb;
    [SerializeField] private TMP_Text _text;

    private LoadScene _loadScene;
    private Volume _volume;
    private Sensibility _sensibility;

    private void Start()
    {
        _volume = new Volume();
        _loadScene = new LoadScene();
        _sensibility = new Sensibility();

        OptionStart();
    }

    private void ShowORHideUI(bool value)
    {
        _mainMenu.SetActive(value);
        _menuOption.SetActive(!value);

        if (value)
        {
            _text.text = $"{_gameName}";
            _buttonStart.onClick.AddListener(LoadGame);
            _buttonOption.onClick.AddListener(OpenOptionMenu);

            _buttonSave.onClick.RemoveListener(SaveMenuOption);
            _buttonExit.onClick.RemoveListener(ExitOption);

            _sliderSensibility.onValueChanged.RemoveListener(TextSensibility);
            _sliderVolume.onValueChanged.RemoveListener(TextVolume);
        }
        else
        {
            _text.text = "Options";
            _buttonSave.onClick.AddListener(SaveMenuOption);
            _buttonExit.onClick.AddListener(ExitOption);

            _buttonStart.onClick.RemoveListener(LoadGame);
            _buttonOption.onClick.RemoveListener(OpenOptionMenu);

            _sliderSensibility.onValueChanged.AddListener(TextSensibility);
            _sliderVolume.onValueChanged.AddListener(TextVolume);
        }
    }

    private void TextVolume(float value)
    {
        _textVolumeNb.text = _volume.UpdateVolumeText(value).ToString();
    }

    private void TextSensibility(float value)
    {
        _textSensibilityNb.text = _sensibility.UpdateSensibilityText(value).ToString();
    }

    private void ExitOption()
    {
        ShowORHideUI(true);
    }

    private void SaveMenuOption()
    {
        _sensibility.SaveSensibility(_sliderSensibility.value);
        _volume.SaveVolume(_sliderVolume.value);
    }

    private void LoadGame()
    {
        _loadScene.SwitchScene("Game");
    }

    private void OpenOptionMenu()
    {
        ShowORHideUI(false);
    }

    private void OptionStart()
    {
        _sliderSensibility.value = Sensibility.PlayerSensibility / 100.0f;
        _sliderVolume.value = Volume.PlayerVolume / 100.0f;

        _textSensibilityNb.text = _sensibility.UpdateSensibilityText(_sliderSensibility.value).ToString();
        _textVolumeNb.text = _volume.UpdateVolumeText(_sliderVolume.value).ToString();

        ShowORHideUI(true);
    }
}
