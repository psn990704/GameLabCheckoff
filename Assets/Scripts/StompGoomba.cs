using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StompGoomba : MonoBehaviour
{
    public Animator goombaAnimator;
    public GameObject goomba;
    private BoxCollider2D goombaBoxCollide;
    private EdgeCollider2D goombaFootEdgeCollide;
    private Rigidbody2D goombaRigid;
    private bool isStomping;

    // Start is called before the first frame update
    void Start()
    {
        goombaBoxCollide = goomba.GetComponent<BoxCollider2D>();
        goombaFootEdgeCollide = GetComponent<EdgeCollider2D>();
        goombaRigid = goomba.GetComponent<Rigidbody2D>();
        isStomping = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("MarioFoot") && !isStomping)
        {
            isStomping = true;
            Debug.Log("triggered2");
            goombaBoxCollide.enabled = false;
            goombaFootEdgeCollide.enabled = false;
            goombaRigid.bodyType = RigidbodyType2D.Static;
            goombaAnimator.Play("goomba-stomped");
            isStomping = false;
        }
    }

    public void GoombaDisappear()
    {
        goomba.SetActive(false);
    }

    public void GameRestart()
    {
        goomba.SetActive(true);
        goombaBoxCollide.enabled = true;
        goombaFootEdgeCollide.enabled = true;
        goombaRigid.bodyType = RigidbodyType2D.Kinematic;
    }
}
