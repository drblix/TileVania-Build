using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("hit");
    }
}