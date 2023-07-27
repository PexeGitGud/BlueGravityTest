using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    
    Vector2 inputDir;
    Vector2 facingDir;
    [SerializeField]
    float moveSpeed = 1f;
    [SerializeField] 
    ContactFilter2D contactFilter;
    [SerializeField]
    float collisionOffset = .05f;

    List<RaycastHit2D> collisionsList = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            animator.SetBool("Moving", moving);

            if (moving)
            {
                animator.SetFloat("XDir", inputDir.x);
                animator.SetFloat("YDir", inputDir.y);
            }
        }
        else
        {
            animator.SetBool("Moving", false);
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

        int collisions = rb.Cast(direction, contactFilter, collisionsList, moveSpeed * Time.fixedDeltaTime + collisionOffset);
        
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