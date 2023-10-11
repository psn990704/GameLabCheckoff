using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public Animator BrickAnimator;
    private bool isHitState = false;
    private Rigidbody2D brickRigid;
    public GameObject springBody;
    private BoxCollider2D brickBoxCollide;
    public GameObject coin;
    public float coinImpulse = 45;
    private Rigidbody2D coinBody;
    public AudioSource coinAudio;
    // Start is called before the first frame update
    void Start()
    {
        BrickAnimator.SetBool("isHit", isHitState);
        brickRigid = GetComponent<Rigidbody2D>();
        brickBoxCollide = springBody.GetComponent<BoxCollider2D>();
        coinBody = coin.GetComponent<Rigidbody2D>();
        brickBoxCollide.enabled = false;
        coin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (coin.transform.localPosition.y < 0)
        {
            coin.SetActive(false);
            brickRigid.bodyType = RigidbodyType2D.Static;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mariohead") && !isHitState)
        {
            isHitState = true;
            // update animator state
            BrickAnimator.SetBool("isHit", isHitState);
            brickBoxCollide.enabled = true;
            coin.SetActive(true);
            coinSpawn();
        }
    }

    void coinSpawn()
    {
        coinBody.AddForce(Vector2.up * coinImpulse, ForceMode2D.Impulse);
        coinAudio.PlayOneShot(coinAudio.clip);
    }

    private void ResetGame()
    {
        isHitState = false;
        BrickAnimator.SetBool("isHit", isHitState);
        brickBoxCollide.enabled = false;
        coin.transform.localPosition = new Vector3(0f, 0.1f, 0f);
        BrickAnimator.SetTrigger("gameRestart");
        Debug.Log("Coin reset");
    }
    public void RestartButtonCallback(int input)
    {
        ResetGame();
    }
}
