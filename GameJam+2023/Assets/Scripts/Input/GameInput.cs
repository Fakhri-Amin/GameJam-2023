using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    private PlayerControls playerControls;
    private int currentSkillControl = -1;
    bool insideGameplay;

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

    public Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public bool IsOnMouseLeftBeingPressedGameplay()
    {
        return Mouse.current.leftButton.isPressed && insideGameplay;
    }

    public bool IsOnMouseLeftUpGameplay()
    {
        return Mouse.current.leftButton.wasReleasedThisFrame && insideGameplay;
    }

    public bool IsOnMouseLeftUpOutsideGameplay()
    {
        return Mouse.current.leftButton.wasReleasedThisFrame && !insideGameplay;
    }

    public void UpdatePointerGameplay(bool isInsideGameplay)
    {
        insideGameplay = isInsideGameplay;
    }

    public void EquipSkill(int skillID)
    {
        currentSkillControl = skillID;
        if (skillID >= 0)
        {
            UIManager.instance.playerSkills.EnableCancelButton();
        }
    }

    public bool IsBasicAttack()
    {
        return currentSkillControl < 0;
    }

    public int CurrentSelectedSkill()
    {
        return currentSkillControl;
    }
}
