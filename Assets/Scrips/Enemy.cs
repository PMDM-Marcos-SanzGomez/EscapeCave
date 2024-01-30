using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject fireball; // Asigna el prefab de la bola de fuego en el Inspector
    public float throwSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject fireballGO = Instantiate(fireball, transform.position, transform.rotation);

        // Obtiene el componente Rigidbody de la bola de fuego
        Rigidbody2D rb = fireballGO.GetComponent<Rigidbody2D>();

        // Aplica fuerza para lanzar la bola de fuego
        rb.velocity = transform.forward * throwSpeed;
        
        StartCoroutine(ThrowFireball());

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
