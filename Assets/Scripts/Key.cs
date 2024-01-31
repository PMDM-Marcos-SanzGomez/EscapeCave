using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject keyExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Character") {
                Instantiate(keyExplosion, transform.position, Quaternion.identity);
                Destroy (gameObject);
                
        }
    }
}
