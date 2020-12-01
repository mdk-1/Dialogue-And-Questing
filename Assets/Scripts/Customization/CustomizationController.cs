using Assets.Scripts.Common.Constants;
using Assets.Scripts.Common.PlayerCommon;
using Assets.Scripts.Common.Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//script to handle the customization scene as instance
//load textures from resources and update visuals as they are cycled
//load professions and update base stats as they are cycled
//handle reset & randomization of appearence/stats allocation
//handle reset & randomization of stats allocation

public class CustomizationController : MonoBehaviour
{
    //reference to UI elements
    public Renderer characterRenderer;
    public Text availablePointsTextElement;

    //reference to dictionary holding texture type values
    private readonly Dictionary<AppearancePartType, List<Texture2D>> partsTextures = new Dictionary<AppearancePartType, List<Texture2D>>()
    {
        { AppearancePartType.Eyes, new List<Texture2D>() },
        { AppearancePartType.Armour, new List<Texture2D>() },
        { AppearancePartType.Clothes, new List<Texture2D>() },
        { AppearancePartType.Hair, new List<Texture2D>() },
        { AppearancePartType.Skin, new List<Texture2D>() },
        { AppearancePartType.Mouth, new List<Texture2D>() },
    };
    //reference to dictionary holding texture part values
    private readonly Dictionary<AppearancePartType, int> selectedParts = new Dictionary<AppearancePartType, int>()
    {
        { AppearancePartType.Eyes, 0 },
        { AppearancePartType.Armour, 0 },
        { AppearancePartType.Clothes, 0 },
        { AppearancePartType.Hair, 0 },
        { AppearancePartType.Skin, 0 },
        { AppearancePartType.Mouth, 0 },
    };
    //create player stats instance
    private readonly PlayerStats statsData = new PlayerStats();
    //store player name
    private string playerName;
    //store player profession
    private PlayerProfessionType selectedClass;
    //create player customization instance
    public static CustomizationController instance;
    //reference point to updatestats from playerstats
    public delegate void UpdateStats(PlayerStats stats);
    //event to change visuals for stats
    public event UpdateStats OnStatsChange;

    //call instance
    private void Awake()
    {
        instance = this;
    }
    //call load and update methods
    private void Start()
    {
        LoadTextures();
        UpdateStatsVisuals();
        UpdateClass(0);
    }
    //method to cycle through player appearence parts on button event
    public void SelectPreviousAppearancePart(int partIndex)
    {
        AppearancePartType partType = (AppearancePartType)partIndex;

        int max = partsTextures[partType].Count;
        int index = selectedParts[partType] - 1;

        if (index < 0)
        {
            index = max - 1;
        }
        //call set texture by type
        SetTexture(partType, index);
    }
    //method to cycle through player appearence parts on button event
    public void SelectNextAppearancePart(int partIndex)
    {
        AppearancePartType partType = (AppearancePartType)partIndex;
        int max = partsTextures[partType].Count - 1;
        int index = selectedParts[partType] + 1;

        if (index > max)
        {
            index = 0;
        }
        //call set texture by type
        SetTexture(partType, index);
    }
    //method to update the player name
    public void UpdateName(string value)
    {
        playerName = value;
    }

    //method to update class and set default stats accordingly
    public void UpdateClass(int classIndex)
    {
        PlayerProfessionType playerClass = (PlayerProfessionType)classIndex;
        selectedClass = playerClass;

        PlayerProfession classData = PlayerProfession.professions[playerClass];
        foreach (BaseStats stat in statsData.baseStats)
        {
            stat.defaultStat = classData.defaultStats[stat.statType];
        }
        //call update stats visually
        UpdateStatsVisuals();
    }

    //method to randomize appearence of player
    public void RandomizeAppearance()
    {
        //generate random appearence and call set texture
        foreach (AppearancePartType type in partsTextures.Keys)
        {
            List<Texture2D> textures = partsTextures[type];
            int random = Random.Range(0, textures.Count);
            SetTexture(type, random);
        }
    }
    //method for reset appearence button event
    public void ResetAppearance()
    {
        foreach (AppearancePartType type in partsTextures.Keys)
        {
            SetTexture(type, 0);
        }
    }
    //method to set stats point allocation by type
    public void SetStat(PlayerBaseStatsType statType, int value)
    {
        statsData.SetBaseStat(statType, value);
        UpdateStatsVisuals();
    }
    //method to ranomize the stats on randomize button event
    public void RandomizeStats()
    {
        //call reset stats method
        ResetStats();
        //reset available points
        int pointsTotal = statsData.availableStatPoints;
        //randomly generate stats to be allocated within bounds allocation
        for (int i = 0; i < pointsTotal; i++)
        {
            int random = Random.Range(0, statsData.baseStats.Length);
            BaseStats randomStat = statsData.baseStats[random];
            statsData.SetBaseStat(randomStat.statType, 1);
        }
        //call update stats visually function
        UpdateStatsVisuals();
    }
    //method to reset the stats on reset button event
    public void ResetStats()
    {
        foreach (var stat in statsData.baseStats)
        {
            statsData.availableStatPoints += stat.additionalStat;
            stat.additionalStat = 0;
        }
        UpdateStatsVisuals();
    }
    //method to save player name, stats, appearence and profession for save & play event 
    public void Save()
    {
        //call updatestats
        statsData.UpdateStats();

        //storing player appearence of selected parts into array
        PlayerAppearance appearanceData = new PlayerAppearance()
        {
            parts = selectedParts.Select(x => new PlayerAppearancePart

            {
                partType = x.Key,textureName = partsTextures[x.Key][x.Value].name,}).ToArray(),
            };

        //create new playerdata entry with the data to be saved
        PlayerData data = new PlayerData
        {
            appearance = appearanceData,
            stats = statsData,
            name = playerName,
            playerClass = selectedClass,
        };
        //call savesystem to save formed player data
        SaveSystem.SavePlayer(data);
    }
    //method to set the texture visually on each element
    private void SetTexture(AppearancePartType type, int textureIndex)
    {
        selectedParts[type] = textureIndex;

        var texture = GetSelectedTextureByType(type);
        
        Material[] mats = characterRenderer.materials;
        mats[(int)type].mainTexture = texture;
        characterRenderer.materials = mats;
    }
    //method to update stats visually on each element
    private void UpdateStatsVisuals()
    {
        OnStatsChange(statsData);
        availablePointsTextElement.text = $"Points: {statsData.availableStatPoints}";
    }

    //method to load textures from resources folder
    private void LoadTextures()
    {
        foreach (KeyValuePair<AppearancePartType, List<Texture2D>> part in partsTextures)
        {
            int textureCount = 0;
            Texture2D tempTexture;
            do
            {
                string path = $"{ResourcesLocations.CharacterTextures}{part.Key}_{textureCount}";
                tempTexture = (Texture2D)Resources.Load(path);

                if (tempTexture != null)
                {
                    part.Value.Add(tempTexture);
                }

                textureCount++;
            }
            while (tempTexture != null);
        }
    }
    //method to return the selected texture by type
    private Texture2D GetSelectedTextureByType(AppearancePartType type)
    {
        int selectedIndex = selectedParts[type];
        return partsTextures[type][selectedIndex];
    }

    //Method for save and play onClick function
    public void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
