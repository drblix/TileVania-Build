using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startingBuildIndex;
    int currentBuildIndex;
    private GameObject persistSession;
    private void Awake()
    {
        int numOfPersistSessions = FindObjectsOfType<ScenePersist>().Length;
        if (numOfPersistSessions > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        startingBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentBuildIndex != startingBuildIndex)
        {
            Destroy(this.gameObject);
        }
    }

    public void LevelCompleted()
    {
        Destroy(this.gameObject);
    }
}
