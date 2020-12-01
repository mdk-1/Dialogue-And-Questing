using Assets.Scripts.Common.PlayerCommon;
using UnityEngine;
using UnityEngine.UI;

//script to handle stats value update visuals
public class CustomizationStatController : MonoBehaviour
{
    public PlayerBaseStatsType statType;
    public string statName;
    public Text textElement;

    void Start()
    {
        CustomizationController.instance.OnStatsChange += UpdateValue;
    }
    //method to increase stat
    public void IncrementPlayerStat()
    {
        CustomizationController.instance.SetStat(statType, 1);
    }
    //method to decrease stat
    public void DecrementPlayerStat()
    {
        CustomizationController.instance.SetStat(statType, -1);
    }
    //method to update stat visually
    private void UpdateValue(PlayerStats stats)
    {
        var value = stats.GetBaseStat(statType).finalStat;
        textElement.text = $"{statName}: {value}";
    }
}
