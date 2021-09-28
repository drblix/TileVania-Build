using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level05LavaScript : MonoBehaviour
{
    [SerializeField] [Tooltip("Rising/lowering speed of the lava")]
    private float risingSpeed;
    [SerializeField] [Tooltip("Maximum Y position the lava can raise")]
    private float maxRaisePos;
    [SerializeField] [Tooltip("Minimum Y position the lava can lower")]
    private float minRaisePos;

    private bool lavaRaising = true;

    private Vector2 raiseVelocity;
    private Vector2 lowerVelocity;

    private void Start()
    {
        raiseVelocity = new Vector2(0f, risingSpeed * Time.fixedDeltaTime);
        lowerVelocity = new Vector2(0f, -risingSpeed * Time.fixedDeltaTime);
    }

    
    void FixedUpdate()
    {
        if (maxRaisePos <= transform.position.y)
        {
            lavaRaising = false;
        }

        if (minRaisePos >= transform.position.y)
        {
            lavaRaising = true;
        }

        RaiseLava();
    }
    
    private void RaiseLava()
    {
        if (!lavaRaising)
        {
            transform.Translate(lowerVelocity);
        }
        else
        {
            transform.Translate(raiseVelocity);
        }
    }
}
