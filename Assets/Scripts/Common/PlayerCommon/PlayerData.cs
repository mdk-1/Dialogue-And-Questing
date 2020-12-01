using System;

// script to hold playerdata to be serialized and deserialized.

namespace Assets.Scripts.Common.PlayerCommon
{
    [Serializable]
    public class PlayerData
    {
        public PlayerAppearance appearance;
        public PlayerStats stats;
        public string name;
        public PlayerProfessionType playerClass;

        //player data method to store data from customization controller
        public PlayerData()
        {
        }
        //method to store player data from player
        public PlayerData(Player player)
        {
            appearance = player.appearance;
            stats = player.playerStats;
            name = player.name;
        }
    }
}