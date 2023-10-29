using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSkills : MonoBehaviour
{
    public List<PlayerSkillButton> playerSkills;
    public Button cancelButton;

    private void Start()
    {
        //cancelButton.gameObject.SetActive(false);
    }

    public void EnableCancelButton()
    {
        //cancelButton.gameObject.SetActive(true);
    }

    public void CancelButton()
    {
        GameInput.Instance.EquipSkill(-1);
        //cancelButton.gameObject.SetActive(false);
    }
}