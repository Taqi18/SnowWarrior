using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    
    public bool isPlayerBall;

    private void OnEnable()
    {
        Destroy(gameObject, 5f);
    }
}
