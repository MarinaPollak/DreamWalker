using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaraController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    [Header("Movement Boundaries")]
    [SerializeField] bool useBoundaries = true;
    [SerializeField] float minX = -3f;
    [SerializeField] float maxX = 0.9f;

    PlayerController playerController;

    private Animator anim;

    public int dirHeld = -1;

    InputAction move;
    InputAction interact;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        //anim.GetComponent<Animator>();
        playerController = new PlayerController();
        move = playerController.Player.Move;

        interact = playerController.Player.Interact;
        interact.performed += Interact;
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
        /*if(input.x > 0)
        {
            anim.speed = 0;
            anim.Play("WalkLeft");
        }
        else if(input.x < 0)
        {
            anim.speed = 0;
            anim.Play("WalkLeft");
        }*/
        Vector3 newPosition = transform.position + (direction * speed * Time.deltaTime);

        // Clamp position to boundaries if enabled
        if (useBoundaries)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        }

        transform.position = newPosition;
    }

    void Interact(InputAction.CallbackContext context)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Interactable")
        {
            interact.Enable();
        }
        // Debug: print("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Interactable")
        {
            interact.Disable();
        }
        // Debug: print("Exit");
    }
}
