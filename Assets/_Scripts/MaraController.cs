using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MaraController : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    PlayerController playerController;

    InputAction move;

    void Awake()
    {
        playerController = new PlayerController();
        move = playerController.Player.Move;
    }

    private void OnEnable()
    {
        move.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        move.Disable();
    }

    void FixedUpdate()
    {
        Vector2 input = move.ReadValue<Vector2>();
        Vector3 direction = (input.x * transform.right);

        transform.position = transform.position + (direction * speed * Time.deltaTime);
    }
}
