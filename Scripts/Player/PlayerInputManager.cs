using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    PlayerMovement movement;

    PlayerControls playerControls;
    [SerializeField] private Vector2 moveInput;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        OnMovement();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Player.Move.performed += i => moveInput = i.ReadValue<Vector2>();
            playerControls.Player.Move.canceled += i => moveInput = Vector2.zero;

            playerControls.Player.Aim.performed += OnMousePos;
        }

        playerControls.Enable();
    }

    private void OnMovement()
    {
        movement.move = moveInput;
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        movement.mouseHandler.mousePos = context.ReadValue<Vector2>();
    }
}
