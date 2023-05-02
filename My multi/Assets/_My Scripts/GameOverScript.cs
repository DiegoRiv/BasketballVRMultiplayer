using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    AudioSource gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver=GetComponent<AudioSource>();
    }
    void GameOver()
    {
        gameOver.Play();
    }
}
