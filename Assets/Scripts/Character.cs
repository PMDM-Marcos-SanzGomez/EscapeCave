using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Character : MonoBehaviour
{
    // Game duration
    private const int matchSecondsDuration = 240;
    // Character speed
    public float Speed = 0.0f;
    // Lateral movement speed
    public float lateralMovement = 2.0f;
    // Jump force
    public float jumpMovement = 400.0f;
    // Transform to check if the character is on the ground
    public Transform groundCheck;
    // Reference to the character's Animator
    private Animator animator;
    // Reference to the character's Rigidbody2D
    private Rigidbody2D rigidbody2d;
    // Variable to check if the character is on the ground
    public bool grounded = true;

    // Texts of the UI
    public TextMeshProUGUI textLives;
    public TextMeshProUGUI textGems;
    public TextMeshProUGUI textKey;
    public TextMeshProUGUI textCounter;
    public TextMeshProUGUI textAttempts;
    public TextMeshProUGUI timerText;

    // Game sounds
    public AudioClip keySound;
    public AudioClip gemSound;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip jumpSound;
    public AudioClip deniedSound;

    //Movement button
    float movementButton = 0.0f;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTexts();
        // Check if the character is on the ground using a raycast
        grounded = Physics2D.Linecast (transform.position,
                                        groundCheck.position,
                                        LayerMask.GetMask ("Ground"));

        // If the character is on the ground and the jump button is pressed, an upward force is applied
        if (grounded && Input.GetButtonDown ("Jump") && Mathf.Abs(rigidbody2d.velocity.y) < 0.01f){
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);
        }

        // Updates character animation depending on whether they are on the ground or in the air
        if (grounded){
            animator.SetTrigger ("Grounded");
        } else{
            animator.SetTrigger ("Jump");
        }

        // Get player input for lateral movement
        //Speed = lateralMovement * Input.GetAxis ("Horizontal");
        Speed = lateralMovement * movementButton;

        // The character moves according to the calculated speed
        transform.Translate (Vector2.right * Speed * Time.deltaTime);

        // The character's movement animation and the scale of its sprite are updated
        animator.SetFloat("Speed", Mathf.Abs(Speed));
        if (Speed < 0)
            transform.localScale = new Vector3 (1, 1, 1);
        else
            transform.localScale = new Vector3 (-1, 1, 1);

        //Handles game logic, such as resetting the timer and counting attempts
        if (GameManager.currentLives <= 0)
            {
                GameManager.timer = matchSecondsDuration;
                GameManager.currentLives = 5;
                GameManager.currentGems = 0;
                GameManager.currentKeys = 0;
                GameManager.attemps++;
            }

        // The game timer is updated
        if (GameManager.timer > 0){
            GameManager.timer -= Time.deltaTime;
            UpdateTimerDisplay();

        } else
        {
            // If the timer reaches zero, it restarts and the scene loads
            GameManager.timer = matchSecondsDuration;
            GameManager.attemps++;
            SceneManager.LoadScene("Level1");
        }

        // If all gems are collected, the final scene loads
        if (GameManager.currentGems == 3)
        {
            SceneManager.LoadScene("FinalScene");
        } else{
            UpdateCounterDisplay();
            GameManager.counter += Time.deltaTime;
        }
    }

    void UpdateTexts(){
        // Texts in the user interface are updated
        textAttempts.text = GameManager.attemps.ToString();
        textLives.text = GameManager.currentLives.ToString();
        textGems.text = GameManager.currentGems.ToString();
        textKey.text = GameManager.currentKeys.ToString();
    }

    // Method to jump
    public void Jump()
    {
        if (grounded)
            rigidbody2d.AddForce(Vector2.up * jumpMovement);
    }

    // Method to move
    public void Move(float amount)
    {
        movementButton = amount;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        // Different events are handled depending on the object that activates the trigger
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
                if( GameManager.currentLives == 0){
                    AudioSource.PlayClipAtPoint(deathSound, transform.position);
                    animator.SetTrigger ("Death");
                    animator.SetTrigger ("Recover");
                    SceneManager.LoadScene("Level1");
                    UpdateTexts();
                } else {
                    AudioSource.PlayClipAtPoint(hurtSound, transform.position);
                    animator.SetTrigger ("Hurt");
                }

                textLives.text = GameManager.currentLives.ToString();
        }
   }

    // Method to update the timer display
   void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(GameManager.timer / 60);
        int seconds = Mathf.FloorToInt(GameManager.timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    void UpdateCounterDisplay()
    {
        int minutes = Mathf.FloorToInt(GameManager.counter / 60);
        int seconds = Mathf.FloorToInt(GameManager.counter % 60);
        textCounter.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // The virtual camera is activated again if you leave the zoom area
    void OnTriggerExit2D(Collider2D other)
    {
    if (other.CompareTag("Zoom")){
        GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = true;
    }
    }
}
