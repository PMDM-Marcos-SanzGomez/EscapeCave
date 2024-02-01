using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementSpeed;
    public LayerMask wallLayer;

    private bool movingRight = true;
    // Start is called before the first frame update
    void Start()
    {

    }
    void Update() {
        MoveHorizontally();
    }
    void MoveHorizontally()
    {
        // Determines the direction of movement
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        // Raycast forward to detect walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayer);

        //If there is a wall, change the direction of movement
        if (hit.collider != null)
        {
            movingRight = !movingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

	// Move the enemy in the corresponding direction
      transform.Translate(direction * movementSpeed * Time.deltaTime);
        

    }

}
