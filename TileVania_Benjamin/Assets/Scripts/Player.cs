using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float climbSpeed;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask climbingLayer;
    [SerializeField]
    private LayerMask obstacleLayer;
    [SerializeField]
    private PhysicsMaterial2D zeroFrictionMaterial;
    [SerializeField]
    private GameObject shopKeeperObject;


    // State
    private bool isAlive = true;
    private bool immortalCheat = false;

    // Cached component references
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFootCollider2D;
    SpriteRenderer mySpriteRenderer;
    GameSession gameSession;
    ShopScript shopScript;
    float gravityScaleAtStart;
    
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFootCollider2D = GetComponent<BoxCollider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gameSession = FindObjectOfType<GameSession>();
        gravityScaleAtStart = myRigidBody2D.gravityScale;
        myBodyCollider2D.sharedMaterial = zeroFrictionMaterial;

        try
        {
            shopKeeperObject = GameObject.Find("ShopKeeper");
            shopScript = shopKeeperObject.GetComponent<ShopScript>();
        }
        catch (System.NullReferenceException)
        {
            shopScript = null;
            shopKeeperObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipSprite();
        Jump();
        ClimbLadder();
        CheckCollisions();
        MainMenu();
        OpenShop();
    }


    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // -1 to +1
        Vector2 playerVelocity = new Vector2(controlThrow * _runSpeed, myRigidBody2D.velocity.y);

        myRigidBody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myFootCollider2D.IsTouchingLayers(groundLayer))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            myRigidBody2D.velocity += jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (!myBodyCollider2D.IsTouchingLayers(climbingLayer)) 
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody2D.gravityScale = gravityScaleAtStart;
            return; 
        }
        
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        myAnimator.SetBool("Running", false);
        
        Vector2 climbingVelocity = new Vector2(myRigidBody2D.velocity.x, controlThrow * climbSpeed);

        myRigidBody2D.velocity = climbingVelocity;
        myRigidBody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(controlThrow) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision) // Checks player collisions
    {
        if (collision.gameObject.tag == "Enemy" && isAlive && !immortalCheat)
        {
            PlayerDeath();
        }
    }
    
    private void OpenShop()
    {
        if (Input.GetKeyDown(KeyCode.E) && shopScript != null)
        {
            shopScript.OpenShopGUI();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin" && isAlive)
        {
            gameSession.AddToCoinAmount(1);
        }
        else if (collision.gameObject.tag == "Gem" && isAlive)
        {
            gameSession.AddToCoinAmount(5);
        }
        else if (collision.gameObject.tag == "Heart" && isAlive)
        {
            gameSession.IncreaseLives(1, false);
        }
    }

    private void CheckCollisions()
    {
        if (myBodyCollider2D.IsTouchingLayers(obstacleLayer) && isAlive && !immortalCheat)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        isAlive = false;
        myAnimator.SetTrigger("Death");
        myBodyCollider2D.sharedMaterial = null;
        mySpriteRenderer.color = Color.red;
        myRigidBody2D.velocity = new Vector2(Random.Range(-10f, 10f), 15f);
        gameSession.ProcessPlayerDeath();
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody2D.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody2D.velocity.x), 1f);
        }
        
    }

    private void MainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            Destroy(gameSession.gameObject);
            SceneManager.LoadScene(0);
        }
    }

    public void ToggleImmortality()
    {
        immortalCheat = !immortalCheat;
    }

    public void CheatPlayerStats(string cheatType, float newAmount)
    {
        if (cheatType == "runCheat")
        {
            _runSpeed = newAmount;
        } 
        else if (cheatType == "jumpCheat")
        {
            jumpForce = newAmount;
        }
    }
}
