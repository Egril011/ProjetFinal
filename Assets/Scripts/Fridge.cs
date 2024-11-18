using Unity.Burst.CompilerServices;
using UnityEngine;

public class Fridge : MonoBehaviour, IInteractable
{
    [SerializeField] private BoxCollider _topDoor;
    [SerializeField] private BoxCollider _bottomDoor;
    [SerializeField] private Animator _animatorFridge;
    [SerializeField] private Light _bottomLight;
    [SerializeField] private Light _topLight;
    private bool _hasOpenTopDoorFridge;
    private bool _hasOpenBottomDoorFridge;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _animatorFridge = GetComponent<Animator>();
        _bottomLight.enabled = false;
        _topLight.enabled = false;
    }

    private void Update()
    {
        if (_hasOpenTopDoorFridge)
        {
            CheckTopDoor();
        }
        else if (_hasOpenBottomDoorFridge)
        {
            CheckBottomDoor();
        }
    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == _topDoor)
            {
                OpenTopDoor();
                _hasOpenTopDoorFridge = true;
                _topLight.enabled = true;
            }
            else if (hit.collider == _bottomDoor)
            {
                OpenBottomDoor();
                _hasOpenBottomDoorFridge = true;
                _bottomLight.enabled = true;
            }
        }
    }

    private void OpenTopDoor()
    {
        _animatorFridge.SetTrigger("OpenTopDoor");
    }

    private void OpenBottomDoor()
    {
        _animatorFridge.SetTrigger("OpenBottomDoor");
    }

    private void CheckTopDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Fridge"))
            {
                _animatorFridge.SetTrigger("CloseTopDoor");
                _hasOpenTopDoorFridge = false;
                _topLight.enabled = false;
            }
            else if (Physics.Raycast(ray, out RaycastHit hit1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit1.collider == _bottomDoor)
                    {
                        _animatorFridge.SetTrigger("CloseTopDoor");
                        _hasOpenTopDoorFridge = false;
                        _bottomLight.enabled = false;

                        _animatorFridge.SetTrigger("OpenBottomDoor");
                        _hasOpenBottomDoorFridge = true;
                        _bottomLight.enabled = true;
                    }

                }
            }
        }
                
    }

    private void CheckBottomDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.layer != LayerMask.NameToLayer("Fridge"))
            {
                _animatorFridge.SetTrigger("CloseBottomDoor");
                _hasOpenBottomDoorFridge = false;
                _bottomLight.enabled = false;
            }
            else if (Physics.Raycast(ray, out RaycastHit hit1))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit1.collider == _topDoor)
                    {
                        _animatorFridge.SetTrigger("CloseBottomDoor");
                        _hasOpenBottomDoorFridge = false;
                        _bottomLight.enabled = false ;

                        _animatorFridge.SetTrigger("OpenTopDoor");
                        _hasOpenTopDoorFridge = true;
                        _topLight.enabled = true;
                    }
                }
            }
        }
    }
}
