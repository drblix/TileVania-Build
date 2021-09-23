using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConsoleHandler : MonoBehaviour
{
    // funny moment console commands

    [SerializeField]
    private Canvas consoleWindow;
    [SerializeField]
    private InputField consoleInput;
    [SerializeField]
    private Rigidbody2D playerRigidBody;

    private CanvasGroup consoleWindowGroup;

    private bool consoleOpen = false;
    private string _command;

    private void Start()
    {
        consoleWindowGroup = consoleWindow.GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (consoleOpen)
            {
                consoleOpen = false;
                consoleWindowGroup.alpha = 0;
                consoleWindowGroup.interactable = false;
            }
            else if (!consoleOpen)
            {
                consoleOpen = true;
                consoleWindowGroup.alpha = 1;
                consoleWindowGroup.interactable = true;
            }
        }
    }

    public void CommandEntered()
    {
        _command = consoleInput.text.ToLower();
        consoleInput.text = null;

        if (_command.Contains("load_scene"))
        {
            int sceneNumber;
            string[] words = _command.Split(' ');

            sceneNumber = System.Convert.ToInt32(words[1]);

            SceneManager.LoadScene(sceneNumber);
        }

        if (_command.Contains("add_coins"))
        {
            int coinAmount;
            string[] words = _command.Split(' ');

            coinAmount = System.Convert.ToInt32(words[1]);

            FindObjectOfType<GameSession>().AddToCoinAmount(coinAmount);
        }

        if (_command.Contains("crutchpls"))
        {
            FindObjectOfType<Player>().ToggleImmortality();
        }

        if (_command.Contains("run_speed"))
        {
            string commandType = "runCheat";
            float newRunSpeed;
            string[] words = _command.Split(' ');

            newRunSpeed = System.Convert.ToSingle(words[1]);

            FindObjectOfType<Player>().CheatPlayerStats(commandType, newRunSpeed);
        }

        if (_command.Contains("jump_force"))
        {
            string commandType = "jumpCheat";
            float newJumpForce;
            string[] words = _command.Split(' ');

            newJumpForce = System.Convert.ToSingle(words[1]);

            FindObjectOfType<Player>().CheatPlayerStats(commandType, newJumpForce);
        }
    }
}
