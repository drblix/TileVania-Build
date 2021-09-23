using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpSFX;
    
    private GameObject SFXListener;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SFXListener = GameObject.Find("SFXListener");
        AudioSource.PlayClipAtPoint(pickUpSFX, SFXListener.transform.position);
        Destroy(this.gameObject);
    }
}
