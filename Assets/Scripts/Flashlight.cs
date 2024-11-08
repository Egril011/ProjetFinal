using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    Light flashlight;
    [SerializeField] Camera cam;
    float _intesity = 100; 
    bool hasPickedUp;
    float _timer = 0; 

    private void Start()
    {
        cam = Camera.main;
        flashlight = cam.GetComponentInChildren<Light>(); 
    }

    private void Update()
    {
        if (hasPickedUp && Input.GetKey(KeyCode.F))
        {
            flashlight.enabled = true;
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
            {
                if (hit.collider.CompareTag("Walls"))
                {
                    Debug.Log(hit.distance);

                    if (hit.distance < 1)
                    {
                        flashlight.intensity = 0;
                    }
                    else
                    {
                        flashlight.intensity = _intesity;
                    }
                }
            }
        }
        else
        {
            flashlight.enabled = false;
        }
    }

    public void Interact()
    {
        if (flashlight != null)
        {
            hasPickedUp = true;
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;

            var collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
