using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PikupToDestroy : MonoBehaviour, IInteractable
{
    public GameObject otherObject;
    public void Interact()
    {
        Destroy(otherObject);
        Destroy(gameObject);
    }
}
