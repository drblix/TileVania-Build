using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField]
    private int playerLives = 3;
    
    public int coinAmount;

    [SerializeField]
    private Sprite[] liveImages;

    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Image livesIconDisplay;

    private void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesIconDisplay.sprite = liveImages[3];
        coinAmount = 0;
        scoreText.text = "x " + coinAmount.ToString();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 7)
        {
            Destroy(this.gameObject);
        }
    }

    public void AddToCoinAmount(int coinAmountToAdd)
    {
        coinAmount += coinAmountToAdd;

        if (coinAmount <= 1000)
        {
            scoreText.text = "x " + coinAmount.ToString();
        }
        else
        {
            scoreText.text = "x inf";
        }
    }

    public void IncreaseLives(int livesToAdd, bool fromStore)
    {
        if (playerLives < 3)
        {
            playerLives += livesToAdd;
            livesIconDisplay.sprite = liveImages[playerLives];
        }
        else if (!fromStore)
        {
            AddToCoinAmount(10);
        }
        
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesIconDisplay.sprite = liveImages[playerLives]; // element 0 in array is a filler
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
