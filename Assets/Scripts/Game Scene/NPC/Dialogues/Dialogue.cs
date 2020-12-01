using System;
using UnityEngine;

//script to define dialogue

//Enum to map dialolgue type to index
//dialogue type for response based on preference of player
public enum DialogueType
{
    Neutral = 0,
    Like = 1,
    Dislike = 2,
}
//dialogue class to hold player greetings and responses
//variations in the dialogue conversation
//sentances to hold coversation
[Serializable]
public class Dialogue
{
    [TextArea(3, 240)]
    public string playerSpeech;

    public Dialogue[] dialogueVariants;

    [TextArea(3, 240)]
    public string[] sentences;

    //reference to dialogue type
    public DialogueType type;
    //reference to quest if dialogue will part of quest
    public QuestBase quest;
}
