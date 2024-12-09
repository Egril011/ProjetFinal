using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SafeBox : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _passcodeGameObject;
    [SerializeField] private Camera _secondCamera;
    [SerializeField] private Light _light;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider _collider;

    private int[] _code = { 52, 12, 79 };   //47 87 20.
    private string _mouseHorizontal = "Mouse X";
    private int totalNumbers = 100;
    private bool _useSafeBox;
    private bool _successCode;
    private int _index;
    private int _currentNumber;
    private MyPlayer _player;
    private Flashlight _flashlight;

    private void Start()
    {
        _player = FindAnyObjectByType<MyPlayer>();
        _flashlight = FindAnyObjectByType<Flashlight>();
    }

    private void Update()
    {
        if (_useSafeBox && !_successCode)
        {
            _flashlight.Intesity = 0;

            if (Input.GetMouseButton(0))
            {
                float x = Input.GetAxisRaw(_mouseHorizontal) * 2;
                _passcodeGameObject.transform.Rotate(0f, 0f, x);

                float currentAngle = Mathf.Repeat(_passcodeGameObject.transform.rotation.eulerAngles.z, 360f);

                float segment = 360f / totalNumbers;

                _currentNumber = Mathf.FloorToInt(currentAngle / segment);

                _currentNumber = Mathf.Clamp(_currentNumber, 0, totalNumbers - 1);


            }
            if (Input.GetMouseButtonUp(0))
            {
                if (_currentNumber == _code[_index])
                {
                    _index++;

                    if (_index >= _code.Length)
                    {
                        _animator.SetTrigger("HasOpened");
                        _index = _code.Length - 1;

                        _secondCamera.gameObject.SetActive(false);
                        _light.gameObject.SetActive(false);
                        _player.CanMove = true;
                        _useSafeBox = false;
                        _successCode = true;
                        _flashlight.Intesity = 100;
                        _collider.enabled = false;
                    }
                }
            }

            if (Input.GetKey(KeyCode.Space))
            {
                _secondCamera.gameObject.SetActive(false);
                _light.gameObject.SetActive(false);
                _player.CanMove = true;
                _useSafeBox = false;
                _flashlight.Intesity = 100;
            }
        }
    }



    public void Interact()
    {
        if (_successCode)
        {
            return;
        }
        else
        {
            _secondCamera.gameObject.SetActive(true);
            _useSafeBox = true;
            _light.gameObject.SetActive(true);
            _player.CanMove = false;
        }
    }
}
