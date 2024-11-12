using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour, IInteractable
{
    private MyPlayer _myPlayer;
    private Subtitle _subtitle;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;
    private Vector3 _originalLocalScale;
    private Camera _camera;
    private string _mouseHorizontal = "Mouse X";
    private string _mouseVertical = "Mouse Y";
    private bool _isLookingObject;
    
    public void Start()
    {
        _myPlayer = FindAnyObjectByType<MyPlayer>();
        _subtitle = FindAnyObjectByType<Subtitle>();
        _camera = FindAnyObjectByType<Camera>();
    }

    public void Update()
    {
        if (_isLookingObject)
        {
            MoveObject();
            _subtitle.Message("Appuie sur \"Space\" pour quitter");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            _subtitle.HideMessage();

            _isLookingObject = false;
            _myPlayer.CanMove = true;

            gameObject.transform.rotation = _originalRotation;
            gameObject.transform.position = _originalPosition;
            gameObject.transform.localScale = _originalLocalScale;
        }
    }

    public void Interact()
    {
        _originalRotation = gameObject.transform.rotation;
        _originalPosition = gameObject.transform.position;
        _originalLocalScale = gameObject.transform.localScale;
        Debug.Log($"Camera Position: {_camera.transform.position}");

        _myPlayer.CanMove = false;
        _isLookingObject = true;
       
        gameObject.transform.localScale = new Vector3(3, 3, 3);   
    }

    void MoveObject()
    {
        float mouseX = Input.GetAxisRaw(_mouseHorizontal);
        float mouseY = Input.GetAxisRaw(_mouseVertical);

        gameObject.transform.Rotate(Vector3.up, mouseX, Space.World);    
        gameObject.transform.Rotate(Vector3.right, mouseY, Space.Self);  
    }
}
