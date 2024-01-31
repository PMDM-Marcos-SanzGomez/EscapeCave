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
        // Determina la dirección del movimiento
        Vector2 direction = movingRight ? Vector2.right : Vector2.left;

        // Realiza un Raycast hacia adelante para detectar paredes
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.5f, wallLayer);

        // Si hay una pared, cambia la dirección del movimiento
        if (hit.collider != null)
        {
            movingRight = !movingRight;
        }

        // Mueve al enemigo en la dirección correspondiente
        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

}
