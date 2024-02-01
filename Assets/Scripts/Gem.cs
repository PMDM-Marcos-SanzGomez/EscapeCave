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
        // If the collider belongs to the character object
	if (collider.gameObject.tag == "Character") {
	   // Instantiate a gem explosion effect at the current object's position
    		Instantiate(gemExplosion, transform.position, Quaternion.identity);
    	// Destroy the current gem object
    		Destroy (gameObject);
}

    }

}
