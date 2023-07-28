using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    float interactionRadius = 2f;
    [SerializeField]
    ContactFilter2D interactionFilter;
    [SerializeField]
    Collider2D[] interactables;

    int interactablesNum = 0;

    void LateUpdate()
    {
        interactables = new Collider2D[5];
        interactablesNum = Physics2D.OverlapCircle(transform.position, interactionRadius, interactionFilter, interactables);

        if (interactablesNum > 0)
        {
            foreach(Collider2D i in interactables)
            {
                i?.GetComponent<Interactable>()?.SetDialogueBubbleActive(true, transform);
            }
         }
    }

    void OnInteract()
    {
        if (interactablesNum > 0)
        {
            float closestDist = float.MaxValue;
            Interactable closestInteractable = null;
            foreach (Collider2D i in interactables)
            {
                Interactable interactable = i?.GetComponent<Interactable>();
                if (interactable)
                {
                    float dist = Vector2.Distance(transform.position, i.transform.position);
                    if (dist < closestDist)
                    {
                        closestDist = dist;
                        closestInteractable = interactable;
                    }
                }
            }

            if (closestInteractable)
            {
                closestInteractable.Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}