using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BlinkingLight : MonoBehaviour
{
    [SerializeField] private Light _lightPoint;
    private bool _islightOn;


    private void Start()
    {
        StartCoroutine(BlinkingCoroutine());
    }

    private IEnumerator BlinkingCoroutine()
    {
        Debug.Log(2);
        float delay = UnityEngine.Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(delay);

        float minIntensity = Mathf.Lerp(0.2f, 0.5f, UnityEngine.Random.value);
        float maxIntensity = Mathf.Lerp(0.8f, 1.5f, UnityEngine.Random.value);

        float intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * 2f, 1f));
        _lightPoint.intensity = intensity;

        if (UnityEngine.Random.Range(0, 10) < 2)
        {
            float dimmingDelay = UnityEngine.Random.Range(0.2f, 0.6f);
            float dimmingIntensity = Mathf.Lerp(intensity, 0f, 0.5f);
            float t = 0;

            while (t < 1)
            {
                t += Time.deltaTime / dimmingDelay;
                _lightPoint.intensity = Mathf.Lerp(intensity, dimmingIntensity, t);
                yield return null;
            }
        }

        if (UnityEngine.Random.Range(0, 50) < 1)
        {
            float blackoutDuration = UnityEngine.Random.Range(0.5f, 2f);
            float t = 0;
            float initialIntensity = intensity;

            while (t < 1)
            {
                t += Time.deltaTime / blackoutDuration;
                _lightPoint.intensity = Mathf.Lerp(initialIntensity, 0f, t);
                yield return null;
            }

            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime / blackoutDuration;
                _lightPoint.intensity = Mathf.Lerp(0f, initialIntensity, t);
                yield return null;
            }
        }
    }

}

