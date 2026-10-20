using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private InputActionReference _movementInput;
    [SerializeField] private InputActionReference _interactInput;

    private void OnEnable()
    {
        _interactInput.action.performed += Interact;
    }
    private void OnDisable()
    {
        _interactInput.action.performed -= Interact;
    }

    private void FixedUpdate()
    {
        _player.Move(_movementInput.action.ReadValue<Vector2>());
    }

    public void Interact(InputAction.CallbackContext context)
    {
        _player.Interact();
        Debug.Log("Interact pressed!");
    }
}
