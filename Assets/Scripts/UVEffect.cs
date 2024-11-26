using UnityEngine;

public class UVEffect : MonoBehaviour
{
    public MeshRenderer[] codeMesh;
    private Flashlight _flashlight;

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
            foreach (MeshRenderer render in codeMesh)
            {
                Material material = render.material;
                material.SetVector("_LightPos", transform.position);
                material.SetVector("_LightDir", transform.forward);
                material.SetFloat("_LightAngle", Mathf.Deg2Rad * 50f);
            }
        }
        else
        {
            foreach (MeshRenderer render in codeMesh)
            {
                Material material = render.material;
                material.SetVector("_LightPos", Vector3.zero);
                material.SetVector("_LightDir", Vector3.zero);
                material.SetFloat("_LightAngle", 0f);
            }
        }
    }
}
