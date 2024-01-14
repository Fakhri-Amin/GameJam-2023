using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerSkillButton : MonoBehaviour
{
    public string skillID;
    public Image skillImage;
    public Button button;

    ActiveSkillScript skillScript;
    public void InstantiateButton(ActiveSkillScript newSkill)
    {
        skillScript = newSkill;
    }

    public void OnButtonClick()
    {
        skillScript.OnSkillSelect();
    }

    public void UpdateCooldown(float progress)
    {
        button.interactable = (progress >= 1);
        skillImage.fillAmount = progress;
    }
}