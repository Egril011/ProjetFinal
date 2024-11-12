using UnityEngine;

public class Fridge : MonoBehaviour, IInteractable
{
    [SerializeField] BoxCollider TopDoor;
    [SerializeField] BoxCollider BottomDoor;

    [SerializeField] Animator animatorFridge;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorFridge = GetComponent<Animator>();
    }

    public void Interact()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
       
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider == TopDoor)
            {
                animatorFridge.SetTrigger("OpenTopDoor");
            }
            else if (hit.collider == BottomDoor)
            {
                animatorFridge.SetTrigger("OpenBottomDoor");
            }

        }
    }
}
