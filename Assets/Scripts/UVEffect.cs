using UnityEngine;

public class UVEffect : MonoBehaviour
{
    public MeshRenderer codeMesh;
    private Flashlight _flashlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _flashlight = FindAnyObjectByType<Flashlight>();
    }

    // Update is called once per frame
    void Update()
    {
       if (_flashlight.HasPickedUp)
        {
            codeMesh.material.SetVector("_LightPos", transform.position);
            codeMesh.material.SetVector("_LightDir", transform.forward);
            codeMesh.material.SetFloat("_LightAngle", Mathf.Deg2Rad * 50f);
        }
    }
}
