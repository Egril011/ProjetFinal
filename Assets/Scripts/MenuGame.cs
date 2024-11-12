using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuGame : MonoBehaviour
{
    [SerializeField] private Image _viseur;

    [SerializeField] private Slider _sliderSensibility;
    [SerializeField] private TMP_Text _sensibilityNb;
    [SerializeField] private TMP_Text _sensibilityText;

    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private TMP_Text _volumeNb;
    [SerializeField] private TMP_Text _volumeText;

    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _exitButton;
    
    private MyPlayer _player;
    private Flashlight _flashlight;
    private float _maxValue = 100.0f;

    public static float PlayerSensibility { get; private set; }
    public static float PlayerVolume { get; private set; }

    public void Start()
    {
        SettingsMenu(false);
        _player = FindAnyObjectByType<MyPlayer>();
        _flashlight = FindAnyObjectByType<Flashlight>();
    }

    public void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            SettingsMenu(true);
            Cursor.lockState = CursorLockMode.None;

            _saveButton.onClick.AddListener(SaveSensibility);
            _exitButton.onClick.AddListener(ExitGame);

            _player.CanMove = false;
            _flashlight.enabled = false;

            _viseur.enabled = false;
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

        _player.CanMove = true;
        _flashlight.enabled = true;

        _viseur.enabled = true;

    }

    private void ExitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SettingsMenu(bool isTrue)
    {
        _sliderSensibility.gameObject.SetActive(isTrue);
        _sensibilityNb.gameObject.SetActive(isTrue);
        _sensibilityText.gameObject.SetActive(isTrue);

        _sliderVolume.gameObject.SetActive(isTrue);
        _volumeNb.gameObject.SetActive(isTrue);
        _volumeText.gameObject.SetActive(isTrue);

        _saveButton.gameObject.SetActive(isTrue);
        _exitButton.gameObject.SetActive(isTrue);
    }
}
