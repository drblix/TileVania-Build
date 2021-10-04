using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameSessionObject;

    private GameSession gameSessionScript;
    [SerializeField]
    private GameObject SFXListener;

    [SerializeField]
    private GameObject openShopPrompt;
    [SerializeField]
    private Canvas shopGUI;
    [SerializeField]
    private AudioClip purchaseEffectClip;
    [SerializeField]
    private AudioClip insufficientFundsClip;
    [SerializeField]
    private int lifePrice;


    private bool shopGUIOpen = false;
    private bool shopOpenable = false;
    private int playerCoinAmount;
    private int numOfShopKeepers;


    private void Awake()
    {
        numOfShopKeepers = FindObjectsOfType<ShopScript>().Length;
        if (numOfShopKeepers > 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        try
        {
            gameSessionObject = GameObject.FindGameObjectWithTag("GameSession");
            gameSessionScript = gameSessionObject.GetComponent<GameSession>();
        }
        catch (System.NullReferenceException)
        {
            Debug.LogError("GameSession not located");
            gameSessionObject = null;
            gameSessionScript = null;
        }
        shopGUI.gameObject.SetActive(false);
        openShopPrompt.SetActive(false);
    }

    private void Update()
    {
        playerCoinAmount = gameSessionScript.coinAmount;
    }

    public void OpenShopGUI()
    {
        if (shopOpenable)
        {
            shopGUI.gameObject.SetActive(true);
            shopGUIOpen = true;
        }
        else { return; }
    }

    public void BuyLife()
    {
        if (playerCoinAmount >= lifePrice)
        {
            gameSessionScript.IncreaseLives(1, true);
            gameSessionScript.AddToCoinAmount(-lifePrice);
            AudioSource.PlayClipAtPoint(purchaseEffectClip, SFXListener.transform.position);
        }
        else
        {
            AudioSource.PlayClipAtPoint(insufficientFundsClip, SFXListener.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openShopPrompt.SetActive(true);
            shopOpenable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            openShopPrompt.SetActive(false);
            shopOpenable = false;
            if (shopGUIOpen)
            {
                shopGUI.gameObject.SetActive(false);
            }
        }
    }
}
