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
        _myRigidBody2D.velocity = _speedVector;
    }

    private void Update()
    {
        if (_myRigidBody2D.position.x >= 504)
        {
            _myRigidBody2D.velocity = new Vector2(0f, 0f);
        }
    }
}
