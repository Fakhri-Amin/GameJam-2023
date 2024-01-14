using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PlayerUpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI upgradeNameText;
    public Image upgradeSpriteImage;
    public TextMeshProUGUI upgradeDescText;
    [HideInInspector]
    public UIUpgradePanel upgradeChoice;

    Button button;
    UpgradePartScript currentUpgradePart;

    private void Awake()
    {
        button = GetComponent<Button>();

    }

    public void SetEnableButton(UpgradePartScript upgradePart)
    {
        currentUpgradePart = upgradePart;
        upgradeNameText.text = currentUpgradePart.UpgradeName;
        upgradeSpriteImage.sprite = currentUpgradePart.upgradeSprite;
        upgradeDescText.text = currentUpgradePart.UpgradeDescription;
    }

    public void OnButtonClick()
    {
        PlayerManager.instance.AddUpgradePart(currentUpgradePart);
        upgradeChoice.SetEnable(false);
    }


}