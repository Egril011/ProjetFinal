using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menu;
    [SerializeField] private Slider _sliderSensibility;
    [SerializeField] private Slider _sliderVolume;
    [SerializeField] private TMP_Text _textSensibility;
    [SerializeField] private TMP_Text _textVolume;
    private Sensibility _sensibility;
    private Volume _volume;

    public void Start()
    {
        _sensibility = new Sensibility();
        _volume = new Volume();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ;
            if (_menu.activeInHierarchy)
            {
                _menu.SetActive(false);
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;

                _sliderSensibility.onValueChanged.RemoveListener(TextSensibility);
                _sliderVolume.onValueChanged.RemoveListener(TextVolume);

                _sensibility.SaveSensibility(_sliderSensibility.value);
                _volume.SaveVolume(_sliderVolume.value);
            }
            else
            {
                Debug.Log(2);
                _menu.SetActive(true);
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.Confined;
                _sliderSensibility.onValueChanged.AddListener(TextSensibility);
                _sliderVolume.onValueChanged.AddListener(TextVolume);

            }
        }

    }

    private void TextSensibility(float value)
    {
        _textSensibility.text = _sensibility.UpdateSensibilityText(value).ToString();
    }

    private void TextVolume(float value)
    {
        _textSensibility.text = _volume.UpdateVolumeText(value).ToString();
    }
}
