using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    private PlayerControls playerControls;

    private void Awake()
    {
        Instance = this;

        playerControls = new PlayerControls();
        playerControls.Player.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnDestroy()
    {
        playerControls.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerControls.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
}
