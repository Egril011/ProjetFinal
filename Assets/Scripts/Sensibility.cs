using System.Runtime.CompilerServices;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sensibility
{
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

    public void SaveSensibility(float value)
    {
        PlayerSensibility = Mathf.RoundToInt(value * _maxValue);
    }

    public int UpdateSensibilityText(float slideValue)
    {
        int value = Mathf.RoundToInt(slideValue * _maxValue);
        return value;
    }
}
