using System;

//script to hold player stat types and the base stat varaibles

namespace Assets.Scripts.Common.PlayerCommon
{
    //enim to store stat types
    public enum PlayerBaseStatsType
    {
        Strength = 0,
        Dexterity = 1,
        Constitution = 2,
        Wisdom = 3,
        Intelegent = 4,
        Charisma = 5,
    }
    //class to hold base stat varaibles, get and return final stats
    [Serializable]
    public class BaseStats
    {
        public string baseStatName;
        public int defaultStat;
        public int levelUpStat;
        public int additionalStat;
        public PlayerBaseStatsType statType;

        public int finalStat
        {
            get
            {
                return defaultStat + additionalStat + levelUpStat;
            }
        }
    }
}
