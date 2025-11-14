using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaraController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    PlayerController playerController;

    InputAction move;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = new PlayerController();
        move = playerController.Player.Move;
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
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

        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }
}
