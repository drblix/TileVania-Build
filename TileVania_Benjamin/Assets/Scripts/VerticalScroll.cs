using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [Tooltip ("Game units per second")]
    [SerializeField]
    private float scrollRate;

    private Vector2 scrollVector;

    void Start()
    {
        scrollVector = new Vector2(0f, scrollRate * Time.fixedDeltaTime);
    }


    void FixedUpdate()
    {
        transform.Translate(scrollVector);
    }
}
