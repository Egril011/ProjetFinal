using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessWakeUp : MonoBehaviour
{
    [SerializeField] private UnityEngine.Rendering.Volume _volume;
    [SerializeField] private UnityEngine.Rendering.VolumeProfile _profil;
    [SerializeField] private Subtitle _subtitle;
    [SerializeField] private MyPlayer _myPlayer;
    [SerializeField] Animator _animator;  

    private Vignette _vignette;
    private bool _isSubtitleActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator.SetTrigger("Camera");
        _volume.profile = _profil;

        if (_volume.profile.TryGet<Vignette>(out _vignette))
        {
            StartCoroutine(VignetteIntensity(1, 0, 15));
            StartCoroutine(VignetteSmoothless(1, 0, 15));
        }
    }

    public void Update()
    {
        if (!_isSubtitleActive)
        {
            _isSubtitleActive = true;
            StartCoroutine(_subtitle.SubtitlesWordPerWord("Où suis-je qu'est-ce qu'il se passer, j'ai si mal a la tête"));
        }
    }

    private IEnumerator VignetteIntensity(float start, float end, float duration)
    {
        _myPlayer.CanMove = false;
        Cursor.lockState = CursorLockMode.None;

        float elasped = 0f;

        while ( elasped < duration)
        {
            _vignette.intensity.value = Mathf.Lerp(start, end, elasped / duration);
            elasped += Time.deltaTime;
            yield return null;
        }

        _vignette.intensity.value = end;
        _myPlayer.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private IEnumerator VignetteSmoothless(float start, float end, float duration)
    {
        _myPlayer.CanMove = false;
        Cursor.lockState = CursorLockMode.None;

        float elasped = 0f;

        while (elasped < duration)
        {
            _vignette.smoothness.value = Mathf.Lerp(start, end, elasped / duration);
            elasped += Time.deltaTime;
            yield return null;
        }

        _vignette.smoothness.value = end;

        _myPlayer.CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
