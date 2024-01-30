using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject fireball; // Asigna el prefab de la bola de fuego en el Inspector
    public float throwSpeed = 10f;
    public float movementSpeed = 5f;
    public LayerMask wallLayer;

    private bool movingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        GameObject fireballGO = Instantiate(fireball, transform.position, transform.rotation);

        // Obtiene el componente Rigidbody de la bola de fuego
        Rigidbody2D rb = fireballGO.GetComponent<Rigidbody2D>();

        // Aplica fuerza para lanzar la bola de fuego
        rb.velocity = transform.forward * throwSpeed;

        StartCoroutine(ThrowFireball());
        MoveHorizontally();

    }

    // Mover horizontal no funciona, arreglar 

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

    IEnumerator ThrowFireball()
    {
        while (true)
        {
            // Crea una instancia de la bola de fuego en el punto de lanzamiento
            GameObject bolaDeFuego = Instantiate(fireball, transform.position, transform.rotation);

            // Obtiene el componente Rigidbody de la bola de fuego
            Rigidbody rb = bolaDeFuego.GetComponent<Rigidbody>();

            // Aplica fuerza para lanzar la bola de fuego
            rb.velocity = transform.forward * throwSpeed;

            // Espera 2 segundos antes de lanzar la siguiente bola de fuego
            yield return new WaitForSeconds(2.0f);
        }
    }

}
