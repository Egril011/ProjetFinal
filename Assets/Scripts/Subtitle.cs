using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Subtitle : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text subtitle;

    private void Start()
    {
        subtitle.enabled = false;
        background.enabled = false;
    }

    public void Subtitles(string text, int delay)
    {
        background.enabled = true;
        subtitle.enabled = true;

        subtitle.text = text;
        StartCoroutine(HideSubtitle(delay));
    }

    private IEnumerator HideSubtitle(int delay)
    {
        yield return new WaitForSeconds(delay);

        background.enabled = false;
        subtitle.enabled = false;
    }

    public void Message(string text)
    {
        background.enabled = true;
        subtitle.enabled = true;

        subtitle.text = text;
    }

    public void HideMessage()
    {
        background.enabled = false;
        subtitle.enabled = false;
    }
}
