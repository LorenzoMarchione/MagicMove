using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{ 
    //lateral movement input
    public Vector2 move;

    private float facing = 1;

    //movement variables
    [Header("Movement settings")]
    [SerializeField]private float WalkSpeed;

    private Rigidbody2D rb;
    private PlayerInput input;
    private Transform transform;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    //move player based on input and flip to face move direction
    private void Movement()
    {
        rb.linearVelocityX = WalkSpeed * move.x;
        if (move.x < -0.1 && facing > 0)
            FLip();
        else if (move.x > 0.1 && facing < 0)
            FLip();
}
    //save only x axis from move controls
    private void OnMove(InputValue move)
    {
        this.move = move.Get<Vector2>(); 
    }
    private void FLip()
    {
        facing = -transform.localScale.x;
        transform.localScale = new Vector3(facing, 1, 1);
    }
}
