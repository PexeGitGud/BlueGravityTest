using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBubble;
    [SerializeField]
    float interactionRadius = .6f;
    [SerializeField]
    UnityEvent interactionEvent;
    [SerializeField]
    UnityEvent movingAwayEvent;

    Transform interactor;

    void LateUpdate()
    {
        if (dialogueBubble.activeSelf && interactor)
        {
            if (Vector2.Distance(transform.position, interactor.position) > interactionRadius)
            {
                MovingAway();
            }
        }
    }

    public void Interact()
    {
        interactionEvent?.Invoke();
    }

    public void SetInteractionBubbleActive(bool value, Transform interactor)
    {
        this.interactor = interactor;
        dialogueBubble.SetActive(value);
    }

    void MovingAway()
    {
        SetInteractionBubbleActive(false, null);
        movingAwayEvent?.Invoke();
    }
}