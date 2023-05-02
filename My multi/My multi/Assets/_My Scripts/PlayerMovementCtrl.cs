using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementCtrl : MonoBehaviour
{
    public static PlayerMovementCtrl control;
    public bool state=false;
    FPC_Motion fpcMotion;
    void Start()
    {
        fpcMotion= GetComponent<FPC_Motion>();
    }
     void Update()
    {
        if(state ==true)
        {
            fpcMotion.enabled=false;
        }
        else
        {
            fpcMotion.enabled=true;
        }
    }
}
