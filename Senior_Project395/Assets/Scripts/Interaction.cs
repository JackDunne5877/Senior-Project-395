using UnityEngine;

public class Interaction : MonoBehaviour
{
    //float determines distance required for object interaction
    public float radius = 3f;

    //boolean to determine if an object is currently being interacted with
    public bool isInteracting;

    public Animator animator;


    //creates wire sphere surrounding object of size radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    public void Interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            Debug.Log("item is being interacted with");
            animator.SetBool("isInteracting", isInteracting);

        }
    }
}
