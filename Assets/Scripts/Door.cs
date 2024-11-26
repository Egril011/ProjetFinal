using UnityEngine;

public class Door: MonoBehaviour, IInteractable
{
    [SerializeField]private Animator _animator;
    [SerializeField] private Collider _handle;
    [SerializeField] private Collider _collider;

    public void Interact()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5.0f))
        {
            if (hit.collider == _handle)
            {
                _collider.enabled = false;
                _animator.SetTrigger("HasOpenedDoor");
            }
        }
    }
}
