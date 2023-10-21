using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UnitHealthUI : MonoBehaviour
{
    [SerializeField] private UnitBase unitBase;
    [SerializeField] private TMP_Text healthText;

    void Start()
    {
        unitBase.OnHealthChanged += UnitBase_OnHealthChanged;
        UpdateUI();
    }

    private void UnitBase_OnHealthChanged()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.text = unitBase.GetCurrentHealth().ToString();
    }
}
