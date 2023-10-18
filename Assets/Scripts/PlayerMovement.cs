using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerMovement : Singleton<PlayerMovement>
{
    // Start is called before the first frame update

    // global variables
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    public float speed = 10;
    public float maxSpeed = 20;

    public float upSpeed = 10;
    private bool onGroundState = true;
    private Rigidbody2D marioBody;

    public GameObject enemies;
    public float deathImpulse = 45;
    // for animation
    public Animator marioAnimator;

    // for audio
    public AudioSource marioAudio;

    public AudioSource marioDeath;

    private bool moving = false;

    private bool jumpedState = false;

    public GameManager gameManager;


    // state
    [System.NonSerialized]
    public bool alive = true;
    public Transform gameCamera;
    int collisionLayerMask = (1 << 3) | (1 << 6) | (1 << 7);

    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();
        // Set to be 30 FPS
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();

        // update animator state
        marioAnimator.SetBool("onGround", onGroundState);

        // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SetStartingPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
    }

    void FlipMarioSprite(int value)
    {
        if (value == -1 && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
            if (marioBody.velocity.x > 0.05f)
                marioAnimator.SetTrigger("onSkid");

        }

        else if (value == 1 && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
            if (marioBody.velocity.x < -0.05f)
                marioAnimator.SetTrigger("onSkid");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (((collisionLayerMask & (1 << col.transform.gameObject.layer)) > 0) & !onGroundState)
        {
            onGroundState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // FixedUpdate is called 50 times a second
    void FixedUpdate()
    {
        if (alive && moving)
        {
            Move(faceRightState == true ? 1 : -1);
        }
    }

    void Move(int value)
    {
        Vector2 movement = new Vector2(value, 0);
        // check if it doesn't go beyond maxSpeed
        if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed);
    }

    public void MoveCheck(int value)
    {
        if (value == 0)
        {
            moving = false;
        }
        else
        {
            FlipMarioSprite(value);
            moving = true;
            Move(value);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies") && alive)
        {
            Debug.Log("Collided with goomba!");
            // play death animation
            marioAnimator.Play("Mario-Die");
            marioDeath.PlayOneShot(marioDeath.clip);
            PlayDeathImpulse();
            alive = false;
        }
    }

    public void RestartButtonCallback()
    {
        Debug.Log("Restart!");
        // reset everything
        ResetGame();
        // resume time
    }

    private void ResetGame()
    {
        // reset position
        marioBody.transform.position = new Vector3(-10f, -2f, 0f);

        // reset sprite direction
        faceRightState = true;
        marioSprite.flipX = false;

        // reset animation
        marioAnimator.SetTrigger("gameRestart");
        alive = true;

        // reset camera position
        gameCamera.position = new Vector3(0, 0, -10);
    }

    void PlayJumpSound()
    {
        // play jump sound
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    void PlayDeathImpulse()
    {
        marioBody.AddForce(Vector2.up * deathImpulse, ForceMode2D.Impulse);
    }



    public void Jump()
    {
        if (alive && onGroundState)
        {
            // jump
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            jumpedState = true;
            // update animator state
            marioAnimator.SetBool("onGround", onGroundState);

        }
    }

    public void JumpHold()
    {
        if (alive && jumpedState)
        {
            // jump higher
            marioBody.AddForce(Vector2.up * upSpeed * 30, ForceMode2D.Force);
            jumpedState = false;

        }
    }
    public void GameOverScene()
    {
        gameManager.GameOver();
    }
    public void SetStartingPosition(Scene current, Scene next)
    {
        if (next.name == "World1-2")
        {
            // change the position accordingly in your World-1-2 case
            this.transform.position = new Vector3(-9.5f, -4.5f, 0.0f);
        }
    }
}
