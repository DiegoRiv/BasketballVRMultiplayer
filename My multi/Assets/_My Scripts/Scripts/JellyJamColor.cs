using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class JellyJamColor : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Start()
    {
        text=GetComponent<TextMeshProUGUI>();
        StartCoroutine(ColorChange());
    }

    // Update is called once per frame
    IEnumerator ColorChange()
    {
        yield return new WaitForSeconds(1f);
        text.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
        StartCoroutine(ColorChange());
    }
}
