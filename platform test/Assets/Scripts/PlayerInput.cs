using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public float speed;
    public float grav;
    private float fallSpeed = 0f;
    public float jump;
    private float accel;


    public bool onGround = false;
    public bool leftWall;
    public bool rightWall;
    public bool Celling;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void FixedUpdate()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, .29f);
        leftWall = Physics2D.Raycast(transform.position, Vector2.left, .4f);
        rightWall = Physics2D.Raycast(transform.position, Vector2.right, .4f);
        Celling = Physics2D.Raycast(transform.position, Vector2.up, .2f);


        if (onGround == true)
        {
            fallSpeed = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                accel = grav - jump;
                fallSpeed += accel * Time.deltaTime;
                transform.position -= transform.up * fallSpeed * Time.deltaTime;
            }

            if (rightWall == true || leftWall == true)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    accel = grav - jump;
                    fallSpeed += accel * Time.deltaTime;
                    transform.position -= transform.up * fallSpeed * Time.deltaTime;
                }
            }
        }

        else if (onGround == false && !rightWall && !leftWall)
        {
            accel = grav;
            fallSpeed += accel * Time.deltaTime;
            transform.position -= transform.up * fallSpeed * Time.deltaTime;
        }

        else if (onGround == false && rightWall == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position -= -transform.up * speed * Time.deltaTime;
            }
        }
        else if (onGround == false && leftWall == true)
        {
            if (Input.GetKey(KeyCode.S))
            {
                transform.position -= transform.up * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position -= -transform.up * speed * Time.deltaTime;
            }
        }

        if (!rightWall)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += transform.right * speed * Time.deltaTime;
            }
        }

        if (!leftWall)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position -= transform.right * speed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }


    }
}

