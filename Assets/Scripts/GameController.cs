using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this);
    }

    [SerializeField]
    PlayerController playerController;
    public PlayerController GetPlayerController() => playerController;

    [SerializeField]
    PlayerInventory playerInventory;
    public PlayerInventory GetPlayerInventory() => playerInventory;

    void Start()
    {
        Cursor.visible = true;
    }
}