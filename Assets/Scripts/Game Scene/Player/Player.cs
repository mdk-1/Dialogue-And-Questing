using Assets.Scripts.Common.Constants;
using Assets.Scripts.Common.PlayerCommon;
using Assets.Scripts.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//script to control playable character, load in saved data and handle overall stat updates.

public class Player : MonoBehaviour
{
    [Header ("Player Calibration")]
    [SerializeField]
    private string playername;
    public Text playernameUI;
    [SerializeField] 
    private float healthRegenCooldown = 5f;
    private bool disableHealthRegen;
    private float disableHealthRegenTime;
    [SerializeField] 
    private float staminaRegenCooldown = 3f;
    public bool disableStaminaUsage;
    private float disableStaminaUsageTime;
    
    [Header("Inventory Calibration")]
    public int gold = 0;
    public Storage inventory = new Storage();
    public Item[] startingItems;
    [SerializeField] 
    private float consumablesUsageCooldown = 3f;
    public bool disableConsumablesUsage;
    private float disableConsumablesUsageTime;

    public List<QuestBase> quests;

    public PlayerStats playerStats;
    public PlayerAppearance appearance;
    public PlayerProfession profession;
    public PlayerProfession Profession
    {
        get
        {
            return profession;
        }
        set
        {
            ChangeProfession(value);
        }
    }

    public Renderer characterRenderer;
    public static Player instance;

    //create player instance
    //load player data in
    //update player name UI
    void Awake()
    {
        instance = this;
        LoadPlayerData();
        playernameUI.text = playername;
        //create starting items
        inventory.items.AddRange(startingItems.Select(item => Instantiate(item)));
    }
    private void Update()
    {
        ProcessHealthRegen();
        ProcessStaminaRegen();
        ProcessConsumablesCooldown();
    }

    //method to level up player
    public void LevelUp()
    {
        playerStats.availableStatPoints += 3;

        foreach (var stat in playerStats.baseStats)
        {
            stat.levelUpStat += 1;
        }

        playerStats.UpdateStats();
    }
    //method to change profession
    public void ChangeProfession(PlayerProfession cProfession)
    {
        profession = cProfession;
        SetUpProfression();
    }
    //method to initialize profession
    public void SetUpProfression()
    {
        foreach (BaseStats stat in playerStats.baseStats)
        {
            stat.defaultStat = profession.defaultStats[stat.statType];
        }

        playerStats.UpdateStats();
    }
    //method to damage player
    public void DealDamage(float damage)
    {
        playerStats.CurrentHealth -= damage;
        disableHealthRegen = true;
        disableHealthRegenTime = Time.time;
    }
    //method to heal player
    public void Heal(float health)
    {
        playerStats.CurrentHealth += health;
    }
    //method for health regeneration
    private void ProcessHealthRegen()
    {
        if (!disableHealthRegen)
        {
            if (playerStats.CurrentHealth < playerStats.maxHealth.value)
            {
                playerStats.CurrentHealth += playerStats.regenHealth.value * Time.deltaTime;
            }
        }
        else
        {
            if (Time.time > disableHealthRegenTime + healthRegenCooldown)
            {
                disableHealthRegen = false;
            }
        }
    }
    //method for stamina regeneration
    private void ProcessStaminaRegen()
    {
        if (playerStats.CurrentStamina < 1)
        {
            disableStaminaUsage = true;
            disableStaminaUsageTime = Time.time;
            playerStats.CurrentStamina = 1;
        }

        if (playerStats.CurrentStamina < playerStats.maxStamina.value)
        {
            playerStats.CurrentStamina += playerStats.staminaRegen.value * Time.deltaTime;
        }

        if (disableStaminaUsage)
        {
            if (Time.time > disableStaminaUsageTime + staminaRegenCooldown)
            {
                disableStaminaUsage = false;
            }
        }
    }
    //method to load player data 
    private void LoadPlayerData()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        appearance = data.appearance;
        playerStats = data.stats;
        playername = data.name;

        playerStats.healthHearts = FindObjectOfType<QuaterHearts>();

        Profession = PlayerProfession.professions[data.playerClass];
        //load player appearence for each part
        foreach (PlayerAppearancePart part in appearance.parts)
        {
            string path = $"{ResourcesLocations.CharacterTextures}{part.textureName}";
            Texture2D texture = (Texture2D)Resources.Load(path);
            Material[] mats = characterRenderer.materials;
            mats[(int)part.partType].mainTexture = texture;
            characterRenderer.materials = mats;
        }
    }
    //method to change gold amount
    public void ChangeGold(int amount)
    {
        gold += amount;
    }
    //method to update inventory for consumable timer
    private void ProcessConsumablesCooldown()
    {
        if (disableConsumablesUsage && Time.time > disableConsumablesUsageTime + consumablesUsageCooldown)
        {
            disableConsumablesUsage = false;
            inventory.InvokeOnStorageUpdate();
        }
    }
    //method to set consumable cooldown timer
    public void SetConsumablesOnCooldown()
    {
        disableConsumablesUsage = true;
        disableConsumablesUsageTime = Time.time;
    }

    //temp level up button, deal damage button and quit button
    public void OnGUI() 
    {
        if (GUI.Button(new Rect(130, 10, 100, 20), "Level Up"))
        {
            LevelUp();
        }

        if (GUI.Button(new Rect(130, 40, 120, 20), "Do Damage" + playerStats.CurrentHealth)) // temp damage button
        {
            DealDamage(25f);
        }

        GUI.TextArea(new Rect(10, 40, 120, 20), $"{playerStats.CurrentStamina}/{playerStats.maxStamina.value}");

        if (GUI.Button(new Rect(930, 10, 100, 20), "Quit Game"))
        {
            Application.Quit();
        }
    }
}
