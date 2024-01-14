using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUpgradePanel : MonoBehaviour
{
    CanvasGroup canvasGroup;
    public List<PlayerUpgradeButton> upgradeButtonList;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        foreach (var button in upgradeButtonList)
        {
            button.upgradeChoice = this;
        }
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    public void InstantiateUpgradeChoice(List<UpgradePartScript> upgradePartList)
    {
        for(var i = 0; i < upgradeButtonList.Count; i++)
        {
            upgradeButtonList[i].SetEnableButton(upgradePartList[i]);
        }
        SetEnable(true);
    }

    public void SetEnable(bool isEnable)
    {
        canvasGroup.alpha = (isEnable) ? 1 : 0;
        canvasGroup.interactable = isEnable;
        canvasGroup.blocksRaycasts = isEnable;
        EventManager.instance.DisableInput(isEnable);
    }

}