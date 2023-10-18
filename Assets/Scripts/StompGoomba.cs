using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompGoomba : MonoBehaviour
{
    public GameObject goomba;
    private Rigidbody2D goombaBody;
    private BoxCollider2D goombaBoxCollider;
    private BoxCollider2D goombaHeadBoxCollider;
    public Animator goombaAnimator;
    public AudioSource goombaStomp;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        goombaBody = goomba.GetComponent<Rigidbody2D>();
        goombaBoxCollider = goomba.GetComponent<BoxCollider2D>();
        goombaHeadBoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MarioFoot"))
        {
            Debug.Log("Triggered");
            goomba.GetComponent<EnemyMovement>().Stomp();
            goombaBoxCollider.enabled = false;
            goombaHeadBoxCollider.enabled = false;
            goombaBody.bodyType = RigidbodyType2D.Static;
            goombaAnimator.Play("goomba-stomped");
            goombaStomp.PlayOneShot(goombaStomp.clip);
            gameManager.IncreaseScore(1);
        }
    }
    public void GameRestart()
    {
        Debug.Log("StompRestart");
        goomba.SetActive(true);
        goombaAnimator.Play("goomba-normal");
        goombaBoxCollider.enabled = true;
        goombaHeadBoxCollider.enabled = true;
        goombaBody.bodyType = RigidbodyType2D.Kinematic;
    }
    public void Disappear()
    {
        goomba.SetActive(false);
    }
}
