using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Cinemachine; 

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed = 0.0f;
    public float lateralMovement = 2.0f;
    public float jumpMovement = 400.0f;
    public Transform groundCheck;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public bool grounded = true;
    public TextMeshProUGUI textLives;
    public TextMeshProUGUI textGems;
    public TextMeshProUGUI textKey;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast (transform.position,
                                        groundCheck.position,
                                        LayerMask.GetMask ("Ground"));

        if (grounded && Input.GetButtonDown ("Jump") && Mathf.Abs(rigidbody2d.velocity.y) < 0.01f){
            rigidbody2d.AddForce (Vector2.up * jumpMovement);   
        }
            
        if (grounded){
            animator.SetTrigger ("Grounded");
        } else{
            animator.SetTrigger ("Jump");
        }
            
        

        Speed = lateralMovement * Input.GetAxis ("Horizontal");
        transform.Translate (Vector2.right * Speed * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(Speed));
        if (Speed < 0)
            transform.localScale = new Vector3 (1, 1, 1);
        else
            transform.localScale = new Vector3 (-1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Key") {
                collider.GetComponent<AudioSource>().Play();
                Key.numKeys--;
                GameManager.currentKeys++;
                textKey.text = GameManager.currentKeys.ToString();
                //Destroy (collider.gameObject);
        }
        if (collider.gameObject.tag == "Gem") {
                collider.GetComponent<AudioSource>().Play();
                Gem.numGems--;
                GameManager.currentGems++;
                textGems.text = GameManager.currentGems.ToString();
                //Destroy (collider.gameObject);
        }
        if (collider.gameObject.tag == "Enemy") {
                gameObject.GetComponent<AudioSource>().Play();
                GameManager.currentLives--;
                print(GameManager.currentLives);
                animator.SetTrigger ("Hurt");
                textLives.text = GameManager.currentLives.ToString();
                //Destroy (collider.gameObject);
        }
   }

 
}