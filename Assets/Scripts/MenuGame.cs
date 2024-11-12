using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame : MonoBehaviour
{
    [SerializeField] private Slider _sliderSensibility;
    [SerializeField] private TMP_Text _sensibilityNb;
    [SerializeField] private TMP_Text _sensibilityText;

    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private TMP_Text _volumeNb;
    [SerializeField] private TMP_Text _volumeText;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _exitButton;
    
    private float _maxValue = 100.0f;

    public static float PlayerSensibility { get; private set; }
    public static float PlayerVolume { get; private set; }

    public void Start()
    {
        SettingsMenu(false);
    }

    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            SettingsMenu(true);
            Cursor.lockState = CursorLockMode.None;

            _saveButton.onClick.AddListener(SaveSensibility);
            _exitButton.onClick.AddListener(ExitGame);
        }
        
        _sensibilityNb.text = Mathf.RoundToInt((_sliderSensibility.value * _maxValue)).ToString();
        _volumeNb.text = Mathf.RoundToInt((_sliderVolume.value * _maxValue)).ToString();
    }

    private void SaveSensibility()
    {
        PlayerSensibility = (_sliderSensibility.value * _maxValue);
        PlayerPrefs.SetFloat("PlayerSensibility", PlayerSensibility);

        PlayerVolume = (_sliderVolume.value * _maxValue);
        PlayerPrefs.SetFloat("PlayerVolume", PlayerVolume);


        SettingsMenu(false);

        Cursor.lockState = CursorLockMode.Locked;

        _saveButton.onClick.RemoveListener(SaveSensibility);
        _exitButton.onClick.RemoveListener(ExitGame);

    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SettingsMenu(bool isTrue)
    {
        _sliderSensibility.gameObject.SetActive(isTrue);
        _sensibilityNb.enabled = isTrue;
        _sensibilityText.enabled = isTrue;

        _sliderVolume.gameObject.SetActive(isTrue);
        _volumeNb.enabled = isTrue;
        _volumeText.enabled = isTrue;

        _saveButton.gameObject.SetActive(isTrue);
        _exitButton.gameObject.SetActive(isTrue);
    }
}
