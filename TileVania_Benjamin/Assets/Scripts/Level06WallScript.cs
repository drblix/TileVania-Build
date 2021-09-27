using UnityEngine;

public class Level06WallScript : MonoBehaviour
{
    [SerializeField] private float _wallSpeed;
    private Vector2 _speedVector;

    Rigidbody2D _myRigidBody2D;


    private void Start()
    {
        if (_wallSpeed <= 30f)
        {
            _wallSpeed = 40f;
        }

        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _speedVector = new Vector2(_wallSpeed * Time.fixedDeltaTime, 0f);
    }

    private void FixedUpdate()
    {
        _myRigidBody2D.velocity = _speedVector;
    }
}
