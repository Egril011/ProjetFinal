using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private int _distanceFromWalls = 1;
    private Light _flashlight;
    private float _intesity = 100;
    private bool _flaslighOn = false;
    public bool HasPickedUp { get; private set; }
    
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
                _flaslighOn = !_flaslighOn;
                _flashlight.enabled = _flaslighOn;

                if (_flaslighOn)
                {
                    _flashlight.intensity = _intesity;
                }
            }

            if (_flaslighOn)
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
        
        if (Physics.Raycast(ray, out hit, _distanceFromWalls, _wallLayer))
        {
            _flashlight.enabled = false;
        }
    }

}

               

