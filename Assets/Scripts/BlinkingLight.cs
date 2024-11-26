using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Light _lightSpot;
    [SerializeField] private Light _lightPoint;
    private bool _islightOn;

    private void Start()
    {
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine()
    {
        while (true)
        {
            print(1);
            float delay = UnityEngine.Random.Range(0f, 1f);
            yield return new WaitForSeconds(delay);

            float minIntensity = UnityEngine.Random.Range(0f, 1f);
            float maxIntensity = UnityEngine.Random.Range(1f, 2f);

            float intensity = Mathf.Lerp(minIntensity, maxIntensity, delay);
            _lightPoint.intensity = intensity;
            _lightSpot.intensity = intensity;

            int random = UnityEngine.Random.Range(0, 5);

            if (random == 0)
            {
                print(2);
                float delay1 = UnityEngine.Random.Range(0f, 5f);
                float intensity1 = Mathf.Lerp(maxIntensity, 0, delay1);
                _lightPoint.intensity = intensity1;
                _lightSpot.intensity = intensity1;
            }
        }

    }
}
