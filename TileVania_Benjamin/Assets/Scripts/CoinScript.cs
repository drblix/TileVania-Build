using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip pickUpSFX;
    
    private GameObject SFXListener;

    private bool coinUsable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinUsable)
        {
            coinUsable = false;
            SFXListener = GameObject.Find("SFXListener");
            AudioSource.PlayClipAtPoint(pickUpSFX, SFXListener.transform.position);
            Destroy(this.gameObject);
        }
    }
}
