using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameCue : MonoBehaviour
{
    public AudioSource whistle;
    public void Relocate()
    {
        whistle.Play();
    }
}
