using System;
using System.Linq;

//script to hold apperence & apperence parts classes, methods to set textures to parts 

namespace Assets.Scripts.Common.PlayerCommon
{
    //enum storing appearence parts
    public enum AppearancePartType
    {
        Skin = 0,
        Hair = 1,
        Eyes = 2,
        Mouth = 3,
        Clothes = 4,
        Armour = 5,
    };
    //class to map appearence parts to appearence types
    [Serializable]
    public class PlayerAppearance
    {
        public PlayerAppearancePart[] parts = new PlayerAppearancePart[]
        {
            new PlayerAppearancePart() { partType = AppearancePartType.Eyes },
            new PlayerAppearancePart() { partType = AppearancePartType.Clothes },
            new PlayerAppearancePart() { partType = AppearancePartType.Armour },
            new PlayerAppearancePart() { partType = AppearancePartType.Mouth },
            new PlayerAppearancePart() { partType = AppearancePartType.Skin },
            new PlayerAppearancePart() { partType = AppearancePartType.Hair },
        };
        //method to set apperence part
        public void SetPartTexture(AppearancePartType type, string name)
        {
            PlayerAppearancePart part = GetPart(type);
            part.textureName = name;
        }
        //method to return the appearence part by type
        public PlayerAppearancePart GetPart(AppearancePartType type)
        {
            return parts.Single(part => part.partType == type);
        }
    }
    //class to hold texture names to appearence type
    [Serializable]
    public class PlayerAppearancePart
    {
        public string textureName;
        public AppearancePartType partType;
    }
}
