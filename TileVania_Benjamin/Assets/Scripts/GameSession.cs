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

    private Text scoreText;
    private Image livesIconDisplay;

    private int _endingSceneIndex;

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
        scoreText = transform.Find("Canvas").Find("ScoreText").GetComponent<Text>();
        livesIconDisplay = transform.Find("Canvas").Find("LivesIcon").GetComponent<Image>();
        livesIconDisplay.sprite = liveImages[3];
        scoreText.text = "x 0";
        coinAmount = 0;
        _endingSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == _endingSceneIndex)
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
            if (coinAmount < 0)
            {
                coinAmount = 0;
                scoreText.text = "x " + coinAmount.ToString();
            }
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
            AddToCoinAmount(-10);
            StartCoroutine(DeathCooldown());
        }
        else
        {
            ResetGameSession();
        }
    }

    private IEnumerator DeathCooldown()
    {
        yield return new WaitForSecondsRealtime(2f);

        TakeLife();
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
