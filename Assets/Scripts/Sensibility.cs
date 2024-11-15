using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sensibility : MonoBehaviour
{
    [SerializeField] public Slider SliderSensibility;
    [SerializeField] private TMP_Text _sensibilityNb;
    [SerializeField] private TMP_Text _sensibilityText;
    private float _maxValue = 100.0f;

    public static int PlayerSensibility
    {
        get => PlayerPrefs.GetInt("Sensibility", 1);
        set
        {
            PlayerPrefs.SetInt("Sensibility", value);
            PlayerPrefs.Save();
        }
    }
        
    private void Start()
    {
        SliderSensibility.value = PlayerSensibility / _maxValue;
        _sensibilityNb.text = PlayerSensibility.ToString();
    }

    public void SensibilityTextUpdate(float slideValue)
    {
        _sensibilityNb.text = Mathf.RoundToInt(slideValue * _maxValue).ToString();
    }

    public void SaveSensibility(float value)
    {
        PlayerSensibility = Mathf.RoundToInt(value * _maxValue);
    }

    public void HideORShowSensibilitySetting(bool isVisible)
    {
        SliderSensibility.gameObject.SetActive(isVisible);
        _sensibilityNb.gameObject.SetActive(isVisible);
        _sensibilityText.gameObject.SetActive(isVisible);
    }
}
