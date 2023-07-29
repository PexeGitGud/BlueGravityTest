using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public List<Animator> animators;
    
    Vector2 inputDir;
    Vector2 facingDir;
    public Vector2 GetFacingDir() => facingDir;

    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField] 
    ContactFilter2D collisionFilter;
    [SerializeField]
    float collisionOffset = .05f;

    List<RaycastHit2D> collisionsList = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (inputDir != Vector2.zero)
        {
            bool moving = true;
            if (!Move(inputDir)) //if we cannot move, we collided with something, so try to move sliding along the collider
            {
                if (!Move(new Vector2(inputDir.x, 0))) //for this we need to check both axis separately
                {
                    if (!Move(new Vector2(0, inputDir.y)))
                    {
                        moving = false;
                    }
                }
            }

            foreach (Animator a in animators)
            {
                if (!a)
                {
                    animators.Remove(a);
                    break;
                }

                a.SetBool("Moving", moving);

                if (moving)
                {
                    a.SetFloat("XDir", inputDir.x);
                    a.SetFloat("YDir", inputDir.y);
                }
            }
        }
        else
        {
            foreach (Animator a in animators)
            {
                if (!a)
                {
                    animators.Remove(a);
                    break;
                }

                a.SetBool("Moving", false);
            }
        }
    }

    /// <summary>
    /// check if there is a collision when trying to move to "direction"
    /// </summary>
    /// <param name="direction"></param>
    /// <returns>returns true if we can move and false otherwise</returns>
    bool Move(Vector2 direction)
    {
        if (direction == Vector2.zero)
            return false;

        int collisions = rb.Cast(direction, collisionFilter, collisionsList, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        
        if (collisions == 0)
        {
            facingDir = direction;
            rb.MovePosition(rb.position + facingDir * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        return false;
    }

    void OnMove(InputValue movementInput)
    {
        inputDir = movementInput.Get<Vector2>();
    }
}