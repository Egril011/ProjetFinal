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
        _bottomLight.enabled = false;
        _topLight.enabled = false;
    }

    private void Update()
    {
        if (_hasOpenTopDoorFridge || _hasOpenBottomDoorFridge)
        {
            CheckDoor();
        }
    }

    public void Interact()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider == _topDoor)
            {
                HandleTopDoor();
            }
            else if (hit.collider == _bottomDoor)
            {
                HandleBottomDoor();
            }
        }
    }

    private void HandleTopDoor()
    {
        if (_hasOpenBottomDoorFridge)
        {
            CloseBottomDoor();
        }
        if (!_hasOpenTopDoorFridge)
        {
            OpenTopDoor();
        }
        else
        {
            CloseTopDoor();
        }

    }

    private void HandleBottomDoor()
    {
        if (_hasOpenTopDoorFridge)
        {
            CloseTopDoor();
        }

        if (!_hasOpenBottomDoorFridge)
        {
            OpenBottomDoor();
        }
        else
        {
            CloseBottomDoor();
        }
        
    }

    private void CloseTopDoor()
    {
        _animatorFridge.SetTrigger("CloseTopDoor");
        _hasOpenTopDoorFridge = false;
        _topLight.enabled = false;
        _topDoor.enabled = true;
    }

    private void CloseBottomDoor()
    {
        _animatorFridge.SetTrigger("CloseBottomDoor");
        _hasOpenBottomDoorFridge = false;
        _bottomLight.enabled = false;
        _bottomDoor.enabled = true;
    }

    private void OpenTopDoor()
    {
        _animatorFridge.SetTrigger("OpenTopDoor");
        _hasOpenTopDoorFridge = true;
        _topLight.enabled = true;
        _topDoor.enabled = false;
    }

    private void OpenBottomDoor()
    {
        _animatorFridge.SetTrigger("OpenBottomDoor");
        _hasOpenBottomDoorFridge = true;
        _bottomLight.enabled = true;
        _bottomDoor.enabled = false;
    } 

    private void CheckDoor()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        if (Physics.Raycast(ray,out RaycastHit hit, 5))
        {
            Debug.Log(hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point,Color.red, 10000);
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Fridge"))
            {
               return;
            }
        }
        else
        {
            CloseBottomDoor();
            CloseTopDoor();
        }
    }
}
