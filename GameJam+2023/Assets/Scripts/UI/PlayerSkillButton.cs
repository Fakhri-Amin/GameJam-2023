using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSkillButton : MonoBehaviour
{
    public TextMeshProUGUI skillName;
    public Image skillImage;
    public Button button;
    [HideInInspector]
    public bool isSkillActive { get; private set; }

    ActiveSkillScript skillScript;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void InstantiateSkill(ActiveSkillScript newSkill)
    {
        skillScript = newSkill;
        isSkillActive = true;
        skillImage.sprite = skillScript.GetActiveSkillSO().skillSprite;
        skillName.text = skillScript.GetActiveSkillSO().SkillName;
        gameObject.SetActive(true);
    }

    public void RemoveSkill()
    {
        gameObject.SetActive(false);
        skillScript = null;
        isSkillActive = false;
        skillImage.sprite = null;
    }

    public void OnButtonClick()
    {
        skillScript.OnSkillUse(PlayerManager.instance);
    }

    public void UpdateCooldown(float progress)
    {
        button.interactable = (progress >= 1);
        skillImage.fillAmount = progress;
    }

    public ActiveSkillSO GetSkillSO()
    {
        return skillScript.GetActiveSkillSO();
    }
}