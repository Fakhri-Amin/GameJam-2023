using System;
using System.Collections.Generic;
using UnityEngine;

public class CampaignManager : MonoBehaviour
{
    public static CampaignManager instance;
    public List<CampaignScript> campaignPrefab;
    CampaignScript currentCampaign;

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
    }

    private void Start()
    {
        InstantiateCampaign(0);
    }

    public CampaignScript GetCurrentCampaign()
    {
        return currentCampaign;
    }

    void InstantiateCampaign(int campaignNum)
    {
        currentCampaign = Instantiate(campaignPrefab[campaignNum].gameObject, transform).GetComponent<CampaignScript>();
        currentCampaign.InstantiateCampaign();
        EventManager.instance.OnCampaignStart(campaignNum);
    }

}