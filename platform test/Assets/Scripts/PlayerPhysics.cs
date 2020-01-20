using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public bool onGround = false;
    public bool leftWall;
    public bool rightWall;
    public bool ceiling;
    public bool rightClip;
    public bool leftClip;
    public bool groundClip;
    public bool jumpable;
    public bool movement;

    public float gravity;
    public float groundSpeed;
    public float airSpeed;
    public float mass;
    public float damp;
    public float bounce;
    public float jump;
    public float maxSpeed;
    public float jumps;
    public float other;
        
    public Vector2 velocity;
    public Vector2 acceleration;
    public Vector2 force;
    public Vector2 friction;

    void Start()
    {
          
    }

    private void Update()
    {
        Debug.Log(other);
        //Character controller
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpable)
            {
                jumpPhysics();
            }  

        }

        groundSpeed = Mathf.Abs(groundSpeed);
        airSpeed = Mathf.Abs(airSpeed);
        if (!leftWall)
        {
            if (Input.GetKey(KeyCode.A))
            {
                movement = true;
                groundSpeed = -groundSpeed;
                airSpeed = -airSpeed;
                leftRight();
            }
        }

        if (!rightWall)
        {
            if (Input.GetKey(KeyCode.D))
            {
                movement = true;
                leftRight();
            }
        }        
       
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            movement = false;
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    private void FixedUpdate()
    {

        //Physics calcs and checks
        onGround = Physics2D.Raycast(transform.position, Vector2.down, .5f);
        groundClip = Physics2D.Raycast(transform.position, Vector2.down, .49f);        
        leftWall = Physics2D.Raycast(transform.position, Vector2.left, .45f);
        rightWall = Physics2D.Raycast(transform.position, Vector2.right, .45f);
        ceiling = Physics2D.Raycast(transform.position, Vector2.up, .25f);
        rightClip = Physics2D.Raycast(transform.position, Vector2.right, .44f);
        leftClip = Physics2D.Raycast(transform.position, Vector2.left, .44f);

        Vector2 force = mass * acceleration;
        
        if(rightWall || leftWall)
        {
            movement = false;
        }
        
        if (onGround)
        {
            jumpable = true;
            other += gravity;
            acceleration.y = gravity - other;
            velocity.y += acceleration.y * Time.deltaTime;            
            transform.position += transform.right * velocity.x * Time.deltaTime;
            transform.position += transform.up * velocity.y * Time.deltaTime;
            if (!movement)
            {
                if(Mathf.Abs(velocity.x) <= 1)
                {
                    velocity.x = 0f;
                }
                else
                {
                    velocity.x -= friction.x * Time.deltaTime * Mathf.Sign(velocity.x);
                    transform.position += transform.right * velocity.x * Time.deltaTime;
                }
            }
            
            
        }
        if(!onGround)
        {
            acceleration.y = gravity - other;
            velocity.y += acceleration.y * Time.deltaTime;
            transform.position += transform.right * velocity.x * Time.deltaTime;
            other = 0f;

            if (!movement)
            {
                if (Mathf.Abs(velocity.x) <= .01f)
                {
                    velocity.x = 0f;
                }
                else
                {
                    velocity.x -= friction.x/1.5f * Time.deltaTime * Mathf.Sign(velocity.x);
                    transform.position += transform.right * velocity.x * Time.deltaTime;
                }
            }
            transform.position -= transform.up * velocity.y * Time.deltaTime;
            
            if (rightWall || leftWall)
            {
                jumpable = true;
            }
            else
            {
                jumpable = false;
            }
        }


        if(rightClip)
        {
            transform.position += transform.right * - 30f * Time.deltaTime;
            transform.position += transform.up * 30f * Time.deltaTime;
        }
        if (leftClip)
        {
            transform.position += transform.right * 20f * Time.deltaTime;
            transform.position += transform.up * 20f * Time.deltaTime;
        }
        if(groundClip)
        {
            transform.position += transform.up * 10f * Time.deltaTime;
        }

        if (ceiling)
        {
            velocity.y = 0f;
            transform.position -= transform.up * 15f * Time.deltaTime;
        }
    }

    void jumpPhysics()
    {
        if(!rightWall || !leftWall || onGround || jumps < 2)
        {
            other -= jump;
            acceleration.y = gravity - other;
            velocity.y += acceleration.y * Time.deltaTime;            
            transform.position += transform.up * velocity.y * Time.deltaTime;
            jumps++;
        }
    }

    void leftRight()
    {
        if(Mathf.Abs(velocity.x) >= maxSpeed)
        {
            velocity.x = maxSpeed;
        }
        else if(!onGround)
        {
            velocity.x = airSpeed * Time.deltaTime;
        }
        else
        {
            velocity.x = groundSpeed * Time.deltaTime;
        }
    }
}
