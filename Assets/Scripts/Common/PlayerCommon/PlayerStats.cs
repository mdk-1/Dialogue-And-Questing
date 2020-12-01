using Assets.Scripts.Common.PlayerCommon;
using System;
using System.Linq;
using UnityEngine;

//script to hold stats, assign overall character stats against player chosen stats 

//defining the stat values
[Serializable]
public class Stat
{
    public Stat(float value)
    {
        defaultValue = value;
        this.value = value;
    }

    public float defaultValue;
    public float value;
}
//adding player stats class against types of stats
[Serializable]
public class PlayerStats
{
    public int availableStatPoints = 10;

    public BaseStats[] baseStats = new BaseStats[]
    {
        new BaseStats() { statType = PlayerBaseStatsType.Strength, baseStatName = "STR" },
        new BaseStats() { statType = PlayerBaseStatsType.Dexterity, baseStatName = "DEX" },
        new BaseStats() { statType = PlayerBaseStatsType.Constitution, baseStatName = "CON" },
        new BaseStats() { statType = PlayerBaseStatsType.Wisdom, baseStatName = "WIS" },
        new BaseStats() { statType = PlayerBaseStatsType.Intelegent, baseStatName = "INT" },
        new BaseStats() { statType = PlayerBaseStatsType.Charisma, baseStatName = "CHA" },
    };

    //overall player stats 

    [Header("Player Stats")]
    [SerializeField] public Stat speed = new Stat(6f);
    [SerializeField] public Stat sprintSpeed = new Stat(12f);
    [SerializeField] public Stat crouchSpeed = new Stat(3f);
    [SerializeField] public Stat jumpHeight = new Stat(1f);
    [SerializeField] public Stat maxHealth = new Stat(100f);
    [SerializeField] public Stat regenHealth = new Stat(5f);
    [SerializeField] public Stat maxMana = new Stat(100f);
    [SerializeField] public Stat manaRegen = new Stat(5f);
    [SerializeField] public Stat maxStamina = new Stat(100f);
    [SerializeField] public Stat staminaRegen = new Stat(10f);

    //current overall stats
    [Header("Current Stats")]
    [SerializeField] public int level;
    [SerializeField] public float currentMana = 100;
    [SerializeField] private float currentStamina = 100;

    //current stamina for regeneration
    public float CurrentStamina
    {
        get
        {
            return currentStamina;
        }
        set
        {
            currentStamina = Mathf.Clamp(value, 0, maxStamina.value);
        }
    }

    //reference to UI hearts
    public QuaterHearts healthHearts;

    [SerializeField] private float currentHealth = 100; 
    public float CurrentHealth 
    {
        get
        {
            return currentHealth;
        }
        set
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth.value);

            if(healthHearts != null) 
            { 
                healthHearts.UpdateHearts(value, maxHealth.value);
            }
        }
    }

    //method to update player stats
    public void UpdateStats()
    {
        int dexterity = GetBaseStat(PlayerBaseStatsType.Dexterity).finalStat;
        int constitution = GetBaseStat(PlayerBaseStatsType.Constitution).finalStat;
        int strength = GetBaseStat(PlayerBaseStatsType.Strength).finalStat;
        int intelligence = GetBaseStat(PlayerBaseStatsType.Intelegent).finalStat;
        int wisdom = GetBaseStat(PlayerBaseStatsType.Wisdom).finalStat;
        int charisma = GetBaseStat(PlayerBaseStatsType.Charisma).finalStat;

        //calucation for determining overall stats based on player stats 
        //dexterity for speed
        speed.value = speed.defaultValue + dexterity;
        sprintSpeed.value = sprintSpeed.defaultValue + dexterity * 1.5f;
        crouchSpeed.value = crouchSpeed.defaultValue + dexterity * .5f;
        jumpHeight.value = jumpHeight.defaultValue + dexterity * .2f;
        //constitution for HP
        maxHealth.value = maxHealth.defaultValue + constitution * 10;
        regenHealth.value = regenHealth.defaultValue + constitution * 2;
        //strength for Stanmina
        maxStamina.value = maxHealth.defaultValue + strength * 10;
        staminaRegen.value = staminaRegen.defaultValue + strength * 2;
        //Intelligence, Wisdom and Charima for Mana
        maxMana.value = maxMana.defaultValue + intelligence * 8 + charisma * 5;
        manaRegen.value = manaRegen.defaultValue + wisdom + charisma;
    }

    //method to set base stats
    public bool SetBaseStat(PlayerBaseStatsType type, int amount)
    {
        if (amount > 0 && availableStatPoints - amount < 0)
        {
            return false;
        }

        BaseStats stat = GetBaseStat(type);

        if (amount < 0 && stat.additionalStat + amount < 0)
        {
            return false;
        }

        stat.additionalStat += amount;
        availableStatPoints -= amount;

        //call update stats function
        UpdateStats();
        return true;
    }
    //method to get base stats against type
    public BaseStats GetBaseStat(PlayerBaseStatsType type)
    {
        return baseStats.Single(stat => stat.statType == type);
    }
}
