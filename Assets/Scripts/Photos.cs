using UnityEngine;

public class Photos : MonoBehaviour, IInteractable
{
    MyPlayer _myPlayer;
    MyCharacterController _characterController;
    Light _light;
    string _mouseHorizontal = "Mouse X"; 
    string _mouseVertical = "Mouse Y";  
    bool _isLookingObject;
    Quaternion _originalPosition;

    public void Start()
    {
        _myPlayer = FindAnyObjectByType<MyPlayer>();
        _characterController = FindAnyObjectByType<MyCharacterController>();
        Transform findParent = transform.parent;
        _light = findParent.GetComponentInChildren<Light>();
    }

    public void Update()
    {
        if (_isLookingObject)
        {
            MoveObject();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _isLookingObject = false;
            _myPlayer.CanMove = true;
            _light.enabled = false;
            gameObject.transform.rotation = _originalPosition;
        }
    }

    public void Interact()
    {
        _originalPosition = gameObject.transform.rotation;

        _myPlayer.CanMove = false;
        _isLookingObject = true;
       
        gameObject.transform.localScale = new Vector3(3, 3, 3);

        _light.enabled = true;

        Vector3 playerPostion = _characterController.transform.position;
        Vector3 lightPostition = _light.transform.position;

        Vector3 distanceLight = (lightPostition - playerPostion).normalized;
        _light.transform.position = playerPostion + distanceLight * 2;
    }

    void MoveObject()
    {
        float mouseX = Input.GetAxisRaw(_mouseHorizontal);
        float mouseY = Input.GetAxisRaw(_mouseVertical);

        gameObject.transform.Rotate(Vector3.up, mouseX, Space.World);    
        gameObject.transform.Rotate(Vector3.right, mouseY, Space.Self);  
    }
}
