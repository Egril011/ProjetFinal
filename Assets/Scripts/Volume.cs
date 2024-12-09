using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Volume
{
    private float _maxValue = 100.0f;
    internal object profile;

    public static int PlayerVolume
    {
        get => PlayerPrefs.GetInt("Volume", 1);
        set
        {
            PlayerPrefs.SetInt("Volume", value);
            PlayerPrefs.Save();
        }
    }

    public int UpdateVolumeText(float slideVolume)
    {
        int value = Mathf.RoundToInt(slideVolume * _maxValue);
        return value;

    }

    public void SaveVolume(float value)
    {
        PlayerVolume = Mathf.RoundToInt(value * _maxValue);
    }
}
