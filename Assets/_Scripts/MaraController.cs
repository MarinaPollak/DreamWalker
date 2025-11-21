using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaraController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    PlayerController playerController;

    private Animator anim;

    public int dirHeld = -1;

    InputAction move;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerController = new PlayerController();
        move = playerController.Player.Move;
    }

    void OnEnable()
    {
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        Vector2 input = move.ReadValue<Vector2>();
        Vector3 direction = (input.x * transform.right);
        //rb.linearVelocity = input * speed;
        if(input.x > 0)
        {
            anim.speed = 0;
            anim.SetBool("WalkRight", true);
            anim.SetBool("WalkLeft", false);
        }
        else if(input.x < 0)
        {
            anim.speed = 0;
            anim.SetBool("WalkRight", false);
            anim.SetBool("WalkLeft", true);
        }
        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }
}
