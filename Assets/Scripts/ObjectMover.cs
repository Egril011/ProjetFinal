using Unity.VisualScripting;
using UnityEngine;

public class ObjectMover : MonoBehaviour, IInteractable
{
    private MyPlayer _myPlayer;
    private Subtitle _subtitle;
    private Quaternion _originalRotation;
    private Vector3 _originalPosition;
    private Vector3 _originalLocalScale;
    [SerializeField] private Camera _camera;
    private string _mouseHorizontal = "Mouse X";
    private string _mouseVertical = "Mouse Y";
    private bool _isLookingObject;
    
    private void Start()
    {
        _myPlayer = FindAnyObjectByType<MyPlayer>();
        _subtitle = FindAnyObjectByType<Subtitle>();

    }

    public void Update()
    {
        if (_isLookingObject)
        {
            MoveObject();
            _subtitle.Message("Appuie sur \"Space\" pour quitter");

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
    }

    public void Interact()
    {
        _originalRotation = gameObject.transform.rotation;
        _originalPosition = gameObject.transform.position;
        _originalLocalScale = gameObject.transform.localScale;
        print(_camera.transform.forward);
        Vector3 newPos = _camera.transform.position + _camera.transform.forward;
        newPos.y -= 0.5f;
        gameObject.transform.position = newPos;



        _myPlayer.CanMove = false;
        _isLookingObject = true;
       
        gameObject.transform.localScale = new Vector3(5, 5, 5);   
    }

    void MoveObject()
    {
        float mouseX = Input.GetAxisRaw(_mouseHorizontal) * Sensibility.PlayerSensibility;
        float mouseY = Input.GetAxisRaw(_mouseVertical) * Sensibility.PlayerSensibility;

        gameObject.transform.Rotate(Vector3.up, mouseX, Space.World);    
        gameObject.transform.Rotate(Vector3.right, mouseY, Space.Self);  
    }
}
