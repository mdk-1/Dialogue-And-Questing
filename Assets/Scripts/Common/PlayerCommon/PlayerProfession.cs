using System;
using System.Collections.Generic;

//script to hold player profession types and default stats assigned to profession

namespace Assets.Scripts.Common.PlayerCommon
{
    //enum to hold player profession types
    public enum PlayerProfessionType
    {
        Barbarian = 0,
        Bard = 1,
        Druid = 2,
        Monk = 3,
        Paladin = 4,
        Ranger = 5,
        Sorcerer = 6,
        Warlock = 7,
    }

    [Serializable]
    public class PlayerProfession
    {
        public string professionName = "Profession";
        public string abilityName = "Ability";
        public string abilityDescription = "Does an action";
        //add stat type to default stats into dictionary
        public Dictionary<PlayerBaseStatsType, int> defaultStats = new Dictionary<PlayerBaseStatsType, int>()
        {
            { PlayerBaseStatsType.Strength, 0 },
            { PlayerBaseStatsType.Dexterity, 0 },
            { PlayerBaseStatsType.Constitution, 0 },
            { PlayerBaseStatsType.Intelegent, 0 },
            { PlayerBaseStatsType.Wisdom, 0 },
            { PlayerBaseStatsType.Charisma, 0 },
        };
        //add profession with default stat types into dictionary
        public static Dictionary<PlayerProfessionType, PlayerProfession> professions = new Dictionary<PlayerProfessionType, PlayerProfession>()
        {
            { PlayerProfessionType.Barbarian, new PlayerProfession
                {
                    professionName = "Barbarian",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 3 },
                        { PlayerBaseStatsType.Dexterity, 2 },
                        { PlayerBaseStatsType.Constitution, 2 },
                        { PlayerBaseStatsType.Intelegent, 1 },
                        { PlayerBaseStatsType.Wisdom, 1 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Bard, new PlayerProfession
                {
                    professionName = "Bard",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 1 },
                        { PlayerBaseStatsType.Constitution, 1 },
                        { PlayerBaseStatsType.Intelegent, 2 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 3 },
                    }
                }
            },
            { PlayerProfessionType.Druid, new PlayerProfession
                {
                    professionName = "Druid",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 2 },
                        { PlayerBaseStatsType.Constitution, 2 },
                        { PlayerBaseStatsType.Intelegent, 2 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Monk, new PlayerProfession
                {
                    professionName = "Monk",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 3 },
                        { PlayerBaseStatsType.Constitution, 2 },
                        { PlayerBaseStatsType.Intelegent, 1 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Paladin, new PlayerProfession
                {
                    professionName = "Paladin",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 2 },
                        { PlayerBaseStatsType.Dexterity, 1 },
                        { PlayerBaseStatsType.Constitution, 3 },
                        { PlayerBaseStatsType.Intelegent, 1 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Ranger, new PlayerProfession
                {
                    professionName = "Ranger",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 4 },
                        { PlayerBaseStatsType.Constitution, 1 },
                        { PlayerBaseStatsType.Intelegent, 2 },
                        { PlayerBaseStatsType.Wisdom, 1 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
            { PlayerProfessionType.Sorcerer, new PlayerProfession
                {
                    professionName = "Sorcerer",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 1 },
                        { PlayerBaseStatsType.Constitution, 1 },
                        { PlayerBaseStatsType.Intelegent, 3 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 2 },
                    }
                }
            },
            { PlayerProfessionType.Warlock, new PlayerProfession
                {
                    professionName = "Warlock",
                    defaultStats = new Dictionary<PlayerBaseStatsType, int>
                    {
                        { PlayerBaseStatsType.Strength, 1 },
                        { PlayerBaseStatsType.Dexterity, 1 },
                        { PlayerBaseStatsType.Constitution, 1 },
                        { PlayerBaseStatsType.Intelegent, 4 },
                        { PlayerBaseStatsType.Wisdom, 2 },
                        { PlayerBaseStatsType.Charisma, 1 },
                    }
                }
            },
        };
    }
}
