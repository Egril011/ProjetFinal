using Unity.VisualScripting;
using UnityEngine;

public class OldKeyObject : MonoBehaviour, IInteractable
{
    private Rigidbody _rb;
    public bool HasPickUpKey;
    [SerializeField] private Transform _parentTransforml;
    [SerializeField] private Vector3 _position; 
    [SerializeField] private Quaternion _rotation;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    public void Update()
    {
        if (HasPickUpKey && Input.GetKeyDown(KeyCode.G))
        {
            DropKey();
        }
    }

    private void DropKey()
    {
        if (HasPickUpKey)
        {
            HasPickUpKey = false;
            _rb.isKinematic = false;
            gameObject.transform.parent = null;
        }

    }

    public void Interact()
    {
        gameObject.transform.SetParent(_parentTransforml);
        transform.localPosition = _position;
        transform.localRotation = _rotation;
        HasPickUpKey = true;
        _rb.isKinematic = true;
    }
}
