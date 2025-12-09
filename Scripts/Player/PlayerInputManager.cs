using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    private PlayerManager manager;

    PlayerControls playerControls;
    [SerializeField] private Vector2 moveInput;

    private void Awake()
    {
        manager = GetComponent<PlayerManager>();
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

            playerControls.Player.Attack.performed += OnAttack;
        }

        playerControls.Enable();
    }

    private void OnMovement()
    {
        manager.movement.move = moveInput;
        manager.animatorManager.AnimateMovement(moveInput);
    }

    private void OnMousePos(InputAction.CallbackContext context)
    {
        manager.movement.mouseHandler.mousePos = context.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        manager.attackHandler.Attack();
        manager.animatorManager.PlayTargetAnim("Attack");
    }
}
