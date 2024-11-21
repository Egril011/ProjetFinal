using UnityEngine;

public class UVEffect : MonoBehaviour
{
    public MeshRenderer codeMesh;
    private Flashlight _flashlight;
    private float _lightAngle = 100;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _flashlight = FindAnyObjectByType<Flashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_flashlight.HasPickedUp && _flashlight.FlaslightOn)
        {
            codeMesh.material.SetVector("_LightPos",transform.position);
            codeMesh.material.SetVector("_LightDir", transform.forward);
            codeMesh.material.SetFloat("_LightAngle", Mathf.Deg2Rad * 50f);
        }
        else
        {
            codeMesh.material.SetVector("_LightPos", Vector3.zero);
            codeMesh.material.SetVector("_LightDir", Vector3.zero);
            codeMesh.material.SetFloat("_LightAngle", 0f);
        }
    }
}
