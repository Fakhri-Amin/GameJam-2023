using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerSkills : MonoBehaviour
{
    public List<PlayerSkillButton> playerSkillButtonList;
    public Button cancelButton;

    private void Start()
    {
        //cancelButton.gameObject.SetActive(false);
    }

    public PlayerSkillButton AddNewSkill(ActiveSkillScript newSkill)
    {
        foreach (var button in playerSkillButtonList)
        {
            if (!button.isSkillActive)
            {
                button.InstantiateSkill(newSkill);
                return button;
            }
        }
        return null;
    }

    /*public void RemoveSkill(ActiveSkillScript removeSkill)
    {
        foreach (var button in playerSkillButtonList)
        {
            if (button.GetSkillSO() == removeSkill.GetActiveSkillSO())
            {
                button.RemoveSkill();
                break;
            }
        }
    }*/

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