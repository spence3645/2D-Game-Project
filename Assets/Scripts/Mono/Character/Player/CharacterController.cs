using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{

    private int walkSpeed = 1000;
    private int swimSpeed = 500;
    private int jumpHeight = 200; //Needs to be bigger since jumping is a force not position
    private int thrustHeight = 10;
    private float fuelCapacity = 2;
    private float fuel = 2;
    private float horizontal;
    private float vertical;

    public bool isGrounded;
    public bool isSwimming;
    public bool isFacingRight; //Public so EquippedWeapon can access to decide how to aim the gun
    public bool isAirborn;

    GameObject player;

    public ParticleSystem walkTrail;
    ParticleSystem jetpack;

    Rigidbody2D player_rigid;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        walkTrail = GameObject.Find("Walk Trail").GetComponent<ParticleSystem>();
        jetpack = GameObject.Find("Jetpack").GetComponent<ParticleSystem>();

        player_rigid = player.GetComponent<Rigidbody2D>();

        if (player.transform.localScale.x == 1)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        if (!isSwimming)
        {
            MoveHorizontal();
            Jump();
            Jetpack();
        }
        else if (isSwimming)
        {
            Swimming();
        }
    }

    void MoveHorizontal()
    {
        player_rigid.gravityScale = 1f;
        player_rigid.velocity = new Vector2(Mathf.Clamp(player_rigid.velocity.x, -6f, 6f), Mathf.Clamp(player_rigid.velocity.y, -6f, 6f));

        //moves character on horizontal axis
        Vector3 tempVector = new Vector3(horizontal, 0, 0);
        tempVector = tempVector.normalized * walkSpeed * Time.deltaTime;
        player_rigid.AddForce(tempVector);

        if (horizontal > 0 && !isFacingRight)
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x = 1;
            player.transform.localScale = localScale;
            isFacingRight = true;
        }

        else if (horizontal < 0 && isFacingRight)
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x = -1;
            player.transform.localScale = localScale;
            isFacingRight = false;
        }

        if(horizontal == 0)
        {
            player_rigid.velocity = new Vector2(0.0f, player_rigid.velocity.y);
        }
    }

    void Swimming()
    {
        player_rigid.gravityScale = 0.25f;
        player_rigid.velocity = new Vector2(Mathf.Clamp(player_rigid.velocity.x, -4f, 4f), Mathf.Clamp(player_rigid.velocity.y, -4f, 4f));

        //moves character on horizontal axis
        Vector2 tempVector = new Vector2(horizontal, vertical);
        tempVector = tempVector.normalized * swimSpeed * Time.deltaTime;
        player_rigid.AddForce(tempVector);

        if (horizontal > 0 && !isFacingRight)
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x = 1;
            player.transform.localScale = localScale;
            isFacingRight = true;
        }

        else if (horizontal < 0 && isFacingRight)
        {
            Vector3 localScale = player.transform.localScale;
            localScale.x = -1;
            player.transform.localScale = localScale;
            isFacingRight = false;
        }

        if (horizontal == 0)
        {
            player_rigid.velocity = new Vector2(0.0f, player_rigid.velocity.y);
        }

        Jump();
    }

    void Jump()
    {
        if (isGrounded && !isSwimming && vertical > 0)
        {
            Vector2 tempVector = new Vector2(0, vertical*jumpHeight);
            player_rigid.AddForce(tempVector);
            isAirborn = true;
        }
        else if(isGrounded && isSwimming && vertical != 0)
        {
            Vector2 tempVector = new Vector2(0, vertical*jumpHeight);
            player_rigid.AddForce(tempVector);
        }
    }

    void Jetpack()
    {
        if (isAirborn && fuel > 0 && vertical > 0)
        {
            fuel -= Time.deltaTime;
            Vector2 tempVector = new Vector2(0, vertical*thrustHeight);
            player_rigid.AddForce(tempVector);
            jetpack.Emit(5);
        }
        else if (isGrounded)
        {
            isAirborn = false;

            while(fuel < fuelCapacity)
            {
                fuel += Time.deltaTime;
            }
        }
    }

    public void EmitTrail(Color color)
    {
        ParticleSystem.MainModule module = walkTrail.main;
        module.startColor = color;
        if (horizontal > 0 || horizontal < 0)
        {
            walkTrail.Emit(2);
        }
    }
}
