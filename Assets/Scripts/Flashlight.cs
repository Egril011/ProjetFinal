using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _objectLayer;
    [SerializeField] private int _distanceFromWalls = 1;
    private Light _flashlight;
    public float Intesity = 50;
    public bool FlaslightOn { get; private set; }
    public bool HasPickedUp { get; private set; }
    private bool _color;

    private void Start()
    {
        _cam = Camera.main;
        _flashlight = _cam.GetComponentInChildren<Light>();
        _flashlight.enabled = false;
    }

    private void Update()
    {
        if (HasPickedUp)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FlaslightOn = !FlaslightOn;
                _flashlight.enabled = FlaslightOn;

                if (FlaslightOn)
                {
                    _flashlight.intensity = Intesity;
                }
            }

            if (Input.GetMouseButtonDown(2) && FlaslightOn)
            {
                if (_color)
                {
                    Debug.Log(1);
                    Color color = new Color(191f / 255f, 90f / 255f, 255f / 255f, 1f);
                    _flashlight.color = color;
                    _color = false;
                }
                else if (!_color) 
                {
                    _flashlight.color = Color.white;
                    _color = true;
                }
            }

            if (FlaslightOn)
            {
                WallsDetection();
            }
        }
    }

    public void Interact()
    {
        HasPickedUp = true;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }

    private void WallsDetection()
    {
        RaycastHit hit;
        Ray ray = new Ray(_cam.transform.position, _cam.transform.forward);

        if (Physics.Raycast(ray, out hit, _distanceFromWalls, _objectLayer))
        {
            _flashlight.enabled = false;
        }
    }

}



