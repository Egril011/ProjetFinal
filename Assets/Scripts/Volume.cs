using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] public Slider SliderVolume;
    [SerializeField] private TMP_Text _volumeNb;
    [SerializeField] private TMP_Text _volumeText;
    private float _maxValue = 100.0f;

    public static int PlayerVolume
    {
        get => PlayerPrefs.GetInt("Volume",1);
        set
        {
            PlayerPrefs.SetInt("Volume",value);
            PlayerPrefs.Save();
        }
    }

    private void Start()
    {
        SliderVolume.value = PlayerVolume / _maxValue;
        _volumeNb.text = PlayerVolume.ToString();
    }

    public void VolumeTextUpdate(float slideVolume)
    {
        _volumeNb.text = Mathf.RoundToInt(slideVolume * _maxValue).ToString();
       
    }

    public void SaveVolume(float value)
    {
        PlayerVolume = Mathf.RoundToInt(value * _maxValue);
    }

    public void HideORShowVolumeSetting(bool isVisible)
    {
        SliderVolume.gameObject.SetActive(isVisible);
        _volumeNb.gameObject.SetActive(isVisible);
        _volumeText.gameObject.SetActive(isVisible);
    }
}
