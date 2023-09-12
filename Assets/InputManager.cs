using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static event System.Action<Vector2> OnPlayerMovement;
    public static event System.Action<bool> OnPress;

    [SerializeField] private PlayerInput playerInput;



    private void OnEnable()
    {
        playerInput.onActionTriggered += HandleInput;
    }

    private void OnDisable()
    {
        playerInput.onActionTriggered -= HandleInput;
    }


    private void HandleInput(InputAction.CallbackContext context)
    {
        // Primero tomaremos una referencia al nombre de la acción actual.
        string action = context.action.name;

        // Y ahora procesaremos esa acción dentro de un condicional de tipo switch.
        switch (action)
        {
            case "Movement":

                Vector2 input = context.ReadValue<Vector2>();
                OnPlayerMovement?.Invoke(input);
                break;
            case "Jump":

                if (context.canceled) OnPress?.Invoke(true);
                else if (context.canceled) OnPress?.Invoke(false);
                break;

        }
    }


}
