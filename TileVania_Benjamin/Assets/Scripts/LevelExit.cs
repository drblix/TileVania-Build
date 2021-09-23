using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem myParticleSystem;
    [SerializeField]
    private float LevelDelayTime;
    [SerializeField]
    private AudioClip levelCompleteSFX;

    private bool isUsable = true;

    private GameObject SFXListener;

    private void Awake()
    {
        SFXListener = GameObject.Find("SFXListener");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isUsable) { return; }

        isUsable = false;
        Scene currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;
        StartCoroutine("LevelSwitch", currentSceneIndex + 1);
    }

    private IEnumerator LevelSwitch(int newSceneNumber)
    {
        myParticleSystem.Play(true);
        AudioSource.PlayClipAtPoint(levelCompleteSFX, SFXListener.transform.position);

        if (FindObjectOfType<ScenePersist>() != null)
        {
            FindObjectOfType<ScenePersist>().LevelCompleted();
        }

        yield return new WaitForSecondsRealtime(LevelDelayTime);

        myParticleSystem.Stop(false, ParticleSystemStopBehavior.StopEmittingAndClear);
        SceneManager.LoadScene(newSceneNumber);
        isUsable = true;
    }
}
