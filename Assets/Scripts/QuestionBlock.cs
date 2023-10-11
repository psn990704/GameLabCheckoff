using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    public Animator QuestionBlockAnimator;
    private bool isHitState = false;
    private Rigidbody2D springRigid;
    public GameObject springBody;
    private BoxCollider2D boxBoxCollide;
    public GameObject coin;
    public float coinImpulse = 45;
    private Rigidbody2D coinBody;
    public AudioSource coinAudio;
    // Start is called before the first frame update
    void Start()
    {
        QuestionBlockAnimator.SetBool("isHit", isHitState);
        springRigid = GetComponent<Rigidbody2D>();
        boxBoxCollide = springBody.GetComponent<BoxCollider2D>();
        coinBody = coin.GetComponent<Rigidbody2D>();
        boxBoxCollide.enabled = false;
        coin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (coin.transform.localPosition.y < 0)
        {
            coin.SetActive(false);
            springRigid.bodyType = RigidbodyType2D.Static;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mariohead") && !isHitState)
        {
            isHitState = true;
            // update animator state
            QuestionBlockAnimator.SetBool("isHit", isHitState);
            boxBoxCollide.enabled = true;
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
        QuestionBlockAnimator.SetBool("isHit", isHitState);
        boxBoxCollide.enabled = false;
        coin.transform.localPosition = new Vector3(0f, 0.1f, 0f);
        QuestionBlockAnimator.SetTrigger("gameRestart");
        Debug.Log("Coin reset");
    }
    public void RestartButtonCallback(int input)
    {
        ResetGame();
    }
}
