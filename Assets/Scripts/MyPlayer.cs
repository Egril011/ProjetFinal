using KinematicCharacterController.Examples;
using System;
using UnityEngine;


public class MyPlayer : MonoBehaviour
{
    public ExampleCharacterCamera OrbitCamera;
    public Transform CameraFollowPoint;
    public MyCharacterController Character;

    private const string MouseXInput = "Mouse X";
    private const string MouseYInput = "Mouse Y";
    private const string MouseScrollInput = "Mouse ScrollWheel";
    private const string HorizontalInput = "Horizontal";
    private const string VerticalInput = "Vertical";
    public bool CanMove {  get; set; } = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Tell camera to follow transform
        OrbitCamera.SetFollowTransform(CameraFollowPoint);

        // Ignore the character's collider(s) for camera obstruction checks
        OrbitCamera.IgnoredColliders.Clear();
        OrbitCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
    }

    private void Update()
    {
        if (!CanMove)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Character.RaycastTryToInteract();
        }

        HandleCharacterInput();
    }

    private void LateUpdate()
    {
        HandleCameraInput();
    }

    private void HandleCameraInput()
    {
        if (!CanMove)
        {
            Vector3 CenterCamera = new Vector3(0f, 0f, 0f);
            return;
        }

        // Create the look input vector for the camera
        float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput) * MenuGame.PlayerSensibility;
        float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput) * MenuGame.PlayerSensibility;
        Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

        // Prevent moving the camera while the cursor isn't locked
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            lookInputVector = Vector3.zero;
        }

        // Input for zooming the camera (disabled in WebGL because it can cause problems)
        float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

        // Apply inputs to the camera
        OrbitCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

        // Handle toggling zoom level
    }

    private void HandleCharacterInput()
    {
        if (!CanMove)
        {
            return;
        }

        PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

        // Build the CharacterInputs struct
        characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
        characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
        characterInputs.CameraRotation = OrbitCamera.Transform.rotation;

        // Apply inputs to character
        Character.SetInputs(ref characterInputs);
    }
}
