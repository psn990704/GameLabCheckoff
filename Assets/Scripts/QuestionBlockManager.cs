using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlockManager : MonoBehaviour
{
    GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("QuestionBlock");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Reset()
    {
        foreach (GameObject child in gameObjects)
        {
            child.GetComponent<QuestionBlock>().RestartButtonCallback(0);
        }
    }
}
