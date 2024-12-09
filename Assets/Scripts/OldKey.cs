using UnityEngine;

public class OldKey : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private OldKeyObject _keyobject;

    public void Interact()
    {
        gameObject.SetActive(false);
        _gameObject.SetActive(true);
        _keyobject.HasPickUpKey = true;
    }
}
