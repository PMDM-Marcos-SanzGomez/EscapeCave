using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Character : MonoBehaviour
{
    private const int matchSecondsDuration = 180;
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
    public TextMeshProUGUI textAttempts;
    
    public TextMeshProUGUI timerText;
    public AudioClip keySound;
    public AudioClip gemSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip deniedSound;
    float movementButton = 0.0f;
    


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
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }

        if (grounded){
            animator.SetTrigger ("Grounded");
        } else{
            animator.SetTrigger ("Jump");
        }

        Speed = lateralMovement * Input.GetAxis ("Horizontal");
        //Speed = lateralMovement * movementButton;

        transform.Translate (Vector2.right * Speed * Time.deltaTime);
        animator.SetFloat("Speed", Mathf.Abs(Speed));
        if (Speed < 0)
            transform.localScale = new Vector3 (1, 1, 1);
        else
            transform.localScale = new Vector3 (-1, 1, 1);

        if (GameManager.currentLives <= 0)
            {
                GameManager.timer = matchSecondsDuration;
                GameManager.currentLives = 3;
                GameManager.currentGems = 0;
                GameManager.currentKeys = 0;
                GameManager.attemps++;

                textAttempts.text = GameManager.attemps.ToString();
                textLives.text = GameManager.currentLives.ToString();
                textGems.text = GameManager.currentGems.ToString();
                textKey.text = GameManager.currentKeys.ToString();

                SceneManager.LoadScene("Level1");        
            }

        if (GameManager.timer > 0){

            GameManager.timer -= Time.deltaTime;
            UpdateTimerDisplay();

        } else
        {
            GameManager.timer = matchSecondsDuration;
<<<<<<< HEAD
            GameManager.attemps++;
=======
            SceneManager.LoadScene("Level1");
>>>>>>> ec0fbb5 (Level 2 avanzado, falta solucionar error en enemigo)
        }
    }

    public void Jump()
    {
        if (grounded)
            rigidbody2d.AddForce(Vector2.up * jumpMovement);
    }
    public void Move(float amount)
    {
        movementButton = amount;
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Zoom"){
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
        if (collider.gameObject.tag == "Key") {
                GameManager.currentKeys++;
                textKey.text = GameManager.currentKeys.ToString();
                AudioSource.PlayClipAtPoint(keySound, transform.position);
                Destroy (collider.gameObject);
        }
        if (collider.gameObject.tag == "Gem") {
                GameManager.currentGems++;
                textGems.text = GameManager.currentGems.ToString();
                AudioSource.PlayClipAtPoint(gemSound, transform.position);
                Destroy (collider.gameObject);
        }
        if (collider.gameObject.tag == "DoorLevelOne") {
                SceneManager.LoadScene("Level1");
        }
        if (collider.gameObject.tag == "DoorLevelTwo") {
            if(GameManager.currentKeys==1){
                SceneManager.LoadScene("Level2");
            } else {
                AudioSource.PlayClipAtPoint(deniedSound, transform.position);
            }
                
        }
        if (collider.gameObject.tag == "Enemy") {
                GameManager.currentLives--;
                if( GameManager.currentLives < 1){
                    if (SceneManager.GetActiveScene().name == "Level2") {
                        AudioSource.PlayClipAtPoint(deathSound, transform.position);
                        animator.SetTrigger ("Death");
                        SceneManager.LoadScene("Level1");
                        animator.SetTrigger ("Recover");
                    }
                    AudioSource.PlayClipAtPoint(deathSound, transform.position);
                    animator.SetTrigger ("Death");
                    animator.SetTrigger ("Recover");
                    SceneManager.LoadScene("Level1");
                } else {
                    AudioSource.PlayClipAtPoint(hurtSound, transform.position);
                    animator.SetTrigger ("Hurt");
                }

                textLives.text = GameManager.currentLives.ToString();
        }
   }

   void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(GameManager.timer / 60);
        int seconds = Mathf.FloorToInt(GameManager.timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnTriggerExit2D(Collider2D other)
    {
    if (other.CompareTag("Zoom")){
        GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    }
}