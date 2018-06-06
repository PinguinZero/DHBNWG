using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;         // vars editable in Unity
    public Vector2 UpJumpForce;
    public Vector2 DownJumpForce;
    public Vector2 LeftJumpForce;
    public Vector2 RightJumpForce;
    public float TopSpeed;
    public Transform groundCheck;
    public float CheckRadius;
    public LayerMask WhatIsGround;
    public bool IsGroundTouching;
    public int GravDirectionRand; // what dir is gravity

    private float MoveInput;
    private float VelocityAdded;


    public Timer PlayerTScript; // access pub variables in Timer script

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        GravDirectionRand = 0;
    }
	
    void Update()
    {
        if (PlayerTScript.trigger == true)   // when timer script hits 0 change gravity
        {
            if (PlayerTScript.singleTick == true)
            {
                GravDirectionRand = Random.Range(0, 4);
                if (GravDirectionRand == 0)
                {
                    Physics2D.gravity = new Vector2(0f, -9.81f);
                    Rotation();
                }
                if (GravDirectionRand == 1)
                {
                    Physics2D.gravity = new Vector2(-9.81f, 0f);
                    Rotation();
                }
                if (GravDirectionRand == 2)
                {
                    Physics2D.gravity = new Vector2(0f, 9.81f);
                    Rotation();
                }
                if (GravDirectionRand == 3)
                {
                    Physics2D.gravity = new Vector2(9.81f, 0f);
                    Rotation();
                }
                PlayerTScript.singleTick = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))  //makes player jump if player is grounded
        {
            if (IsGroundTouching == true)
            {
                if (GravDirectionRand == 0)
                {
                    GetComponent<Rigidbody2D>().AddForce(UpJumpForce, ForceMode2D.Impulse);
                }
                if (GravDirectionRand == 2)
                {
                    GetComponent<Rigidbody2D>().AddForce(DownJumpForce, ForceMode2D.Impulse);
                }
                if (GravDirectionRand == 1)
                {
                    GetComponent<Rigidbody2D>().AddForce(LeftJumpForce, ForceMode2D.Impulse);
                }
                if (GravDirectionRand == 3)
                {
                    GetComponent<Rigidbody2D>().AddForce(RightJumpForce, ForceMode2D.Impulse);
                }

            }
        }
    }

	void FixedUpdate () {
        IsGroundTouching = Physics2D.OverlapCircle(groundCheck.position, CheckRadius, WhatIsGround); //is player contacting object in GROUND layer, if so IsGroundTouching = true
        if (GravDirectionRand == 0 || GravDirectionRand == 2) // IF GRAVITY IS DOWN OR UP
        {
            MoveInput = Input.GetAxis("Horizontal");
            VelocityAdded = MoveInput * speed;

            if (VelocityAdded >= TopSpeed)        // if velo is greater than TopSpeed set velo to the TopSpeed (Cannot exceed TopSpeed)
            {
                VelocityAdded = TopSpeed;
            }
       
            rb.velocity = new Vector2(VelocityAdded, rb.velocity.y);  //applies correct force to player (movement and jumping)
        }
        else   // IF GRAVITY IS LEFT OR RIGHT
        {
            MoveInput = Input.GetAxis("Vertical");
            VelocityAdded = MoveInput * speed;

            if (VelocityAdded >= TopSpeed)        // if velo is greater than TopSpeed set velo to the TopSpeed (Cannot exceed TopSpeed)
            {
                VelocityAdded = TopSpeed;
            }

            rb.velocity = new Vector2(rb.velocity.x, VelocityAdded);  //applies correct force to player (movement and jumping)
        }

	}

    void Rotation()  //run everytime grav is switched so "feet" are facing grav side
    {
        if (GravDirectionRand == 2)
        {
            transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = new Vector3(0, 0, 180f);   //to detect if ground is touching character i used an invisible "ground check" on the feet, so to make sure its touching the ceiling when upside down i rotate the character 180 degrees, and vice versa. This also helps annimation
        }
        if (GravDirectionRand == 0)
        {
            transform.eulerAngles = Vector3.zero;
        }
        if (GravDirectionRand == 1)
        {
            transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = new Vector3(0, 0, 270f);
        }
        if (GravDirectionRand == 3)
        {
            transform.eulerAngles = Vector3.zero;
            transform.eulerAngles = new Vector3(0, 0, 90f);
        }
    }
}