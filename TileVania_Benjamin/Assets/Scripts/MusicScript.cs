using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicScript : MonoBehaviour
{
    [SerializeField]
    private AudioSource _tileVaniaTheme;

    private void Awake()
    {
        int numOfMusicManagers = FindObjectsOfType<MusicScript>().Length;
        if (numOfMusicManagers > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
