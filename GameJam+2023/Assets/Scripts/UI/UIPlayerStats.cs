using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIPlayerStats : MonoBehaviour
{
    public Image healthBarSlider;
    public Image energyBarSlider;
    public TextMeshProUGUI bulletCountText;

    public void UpdateHealth(float newValue, float maxValue)
    {
        healthBarSlider.fillAmount = newValue / maxValue;
    }

    public void UpdateEnergy(float newValue, float maxValue)
    {
        healthBarSlider.fillAmount = newValue / maxValue;
    }

    public void UpdateBulletCount(int newValue)
    {
        bulletCountText.text = newValue.ToString();
    }
}