using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private Rigidbody2D body;

    //sideway move
    float Speed = 0;
    public float MaxSpeed = 6f;
    public float Acceleration = 7f;
    public float Deceleration = 7f;

    //jump
    public float jumpVelocity = 6f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    //check hitting ground and wall
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }   
    void Update()
    {
        Move();
        Jump();
        Fall();
    }

    //sideway
    private void Move()
    {
        if ((Input.GetKey(KeyCode.D)) && (Speed < MaxSpeed))
        {
            Speed = Speed + Acceleration * Time.deltaTime;
        }
            
        else if ((Input.GetKey(KeyCode.A)) && (Speed > -MaxSpeed))
        {
            Speed = Speed - Acceleration * Time.deltaTime;
        }
           
        else
        {
            if (Speed > Deceleration * Time.deltaTime)
                Speed = Speed - (Deceleration + 1) * Time.deltaTime;
            else if (Speed < -Deceleration * Time.deltaTime)
                Speed = Speed + (Deceleration + 1) * Time.deltaTime;
            else
                Speed = 0;
        }
        transform.position = new Vector2(transform.position.x + Speed * Time.deltaTime, transform.position.y);
    }

    //jump
    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            body.velocity = Vector2.up * jumpVelocity;
        }
        
    }

    //falling down control
    private void Fall()
    {
        if (body.velocity.y < 0)
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            Debug.Log("Normal");
        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            Debug.Log("Low");
        }
    } 

}
