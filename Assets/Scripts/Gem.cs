using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject gemExplosion;
    // Start is called before the first frame update
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Character") {
                Instantiate(gemExplosion, transform.position, Quaternion.identity);
                Destroy (gameObject);
                
        }
    }

}
