using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    GameObject dialogueBubble;
    [SerializeField]
    float interactionRadius = .5f;

    Transform interactor;

    void LateUpdate()
    {
        if (dialogueBubble.activeSelf && interactor)
        {
            if (Vector2.Distance(transform.position, interactor.position) > interactionRadius)
            {
                SetDialogueBubbleActive(false, null);
            }
        }
    }

    public void Interact()
    {
        Debug.Log("Interacted");
    }

    public void SetDialogueBubbleActive(bool value, Transform interactor)
    {
        this.interactor = interactor;
        dialogueBubble.SetActive(value);
    }
}