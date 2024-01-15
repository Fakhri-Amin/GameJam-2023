using System;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public static CampaignManager instance;
    public List<CampaignScript> campaignPrefab;
    CampaignScript currentCampaign;
    int campaignNum = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        EventManager.onCampaignFinishEvent += OnCampaignFinish;
    }

    private void OnDestroy()
    {
        EventManager.onCampaignFinishEvent -= OnCampaignFinish;
    }

    private void Start()
    {
        InstantiateCampaign(campaignNum);
    }

    public CampaignScript GetCurrentCampaign()
    {
        return currentCampaign;
    }

    void InstantiateCampaign(int campaignNum)
    {
        if (currentCampaign != null)
        {
            Destroy(currentCampaign);
        }
        currentCampaign = Instantiate(campaignPrefab[campaignNum].gameObject, transform).GetComponent<CampaignScript>();
        currentCampaign.InstantiateCampaign();
        foreach (var upgradePart in currentCampaign.campaignData.initialUpgradePart)
        {
            PlayerManager.instance.AddUpgradePart(upgradePart);
        }
        EventManager.instance.OnCampaignStart(campaignNum);
    }

    void OnCampaignFinish()
    {
        campaignNum++;
        if (campaignPrefab.Count > campaignNum)
        {
            InstantiateCampaign(campaignNum);
        }
        else
        {
            EventManager.instance.OnGameFinish();
        }
    }
}