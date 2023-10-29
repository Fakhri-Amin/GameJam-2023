using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    private PlayerControls playerControls;
    private int currentSkillControl = -1;
    Vector2 lastClickPos = Vector2.zero;
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

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                Debug.Log("hit");
            }
            lastClickPos = Mouse.current.position.ReadValue();
        }
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
        return Mouse.current.leftButton.isPressed && GetComponent<Collider>().bounds.Contains(lastClickPos);
    }

    public bool IsOnMouseLeftUpGameplay()
    {
        return Mouse.current.leftButton.wasReleasedThisFrame && GetComponent<Collider>().bounds.Contains(lastClickPos);
    }

    public bool IsOnMouseLeftUpOutsideGameplay()
    {
        return Mouse.current.leftButton.wasReleasedThisFrame && !GetComponent<Collider>().bounds.Contains(lastClickPos);
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
