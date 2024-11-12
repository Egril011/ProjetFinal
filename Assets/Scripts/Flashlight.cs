using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    private Light flashlight;
    [SerializeField] private Camera cam;
    private float _intesity = 100; 
    private bool HasPickedUp {get; set;}

    private void Start()
    {
        cam = Camera.main;
        flashlight = cam.GetComponentInChildren<Light>(); 
        flashlight.enabled = false;
    }

    private void Update()
    {
        if (HasPickedUp)
        {
            if (Input.GetKey(KeyCode.F))
            {
                flashlight.enabled = true;
                flashlight.intensity = _intesity;

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
    }

    public void Interact()
    {
        if (flashlight != null)
        {
            HasPickedUp = true;
            var meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;

            var collider = GetComponent<Collider>();
            collider.enabled = false;
        }
    }
}
