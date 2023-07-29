using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField]
    float interactionRadius = 2f;
    [SerializeField]
    ContactFilter2D interactionFilter;

    Collider2D[] interactables;

    int interactablesNum = 0;

    void LateUpdate()
    {
        //Get all the interactables within range and show the interaction bubble.
        interactables = new Collider2D[5];
        interactablesNum = Physics2D.OverlapCircle(transform.position, interactionRadius, interactionFilter, interactables);

        if (interactablesNum > 0)
        {
            foreach(Collider2D i in interactables)
            {
                i?.GetComponent<Interactable>()?.SetInteractionBubbleActive(true, transform);
            }
         }
    }

    /// <summary>
    /// If we press the interaction key and have at least one Interactable within range interact with it, prioritizing the closest to the interactor.
    /// </summary>
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