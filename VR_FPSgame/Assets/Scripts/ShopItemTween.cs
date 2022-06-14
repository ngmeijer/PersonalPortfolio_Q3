using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemTween : MonoBehaviour
{
    private void Start()
    {
        LeanTween.moveY(gameObject, transform.position.y + 0.25f, 1.5f).setLoopPingPong().setEaseInOutSine();
    }
}
