using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//script to control flow, display and activation of dialogue

public class DialogueController : MonoBehaviour
{
    //reference to dialogue class as top level
    public Dialogue rootDialogue;
    // reference to dialogue class as current
    private Dialogue currentDialogue;
    //integer to keep track of dialogue
    private int currentSentenceIndex;
    //bool to toggle if end of diagloue reached
    private bool endOfDialogueReached;
    //reference for a list of active quests
    private List<QuestBase> activeQuests = new List<QuestBase>();
    public bool active;
    private bool inRange;

    //call returntoroot
    //return to top level of dialogue
    private void Start()
    {
        ReturnToRoot();
    }
    //if in range of NPC and button E is pressed, toggle control bool
    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            active = true;
        }
    }
    //method to cycle to next piece of dialogue
    public void SelectNextDialogue(Dialogue dialogue)
    {
        currentSentenceIndex = 0;
        currentDialogue = dialogue;
        endOfDialogueReached = currentSentenceIndex >= currentDialogue.sentences.Length - 1;
    }
    //method to increase to the next piece of dialogue
    public void NextSentence()
    {
        currentSentenceIndex++;
        endOfDialogueReached = currentSentenceIndex >= currentDialogue.sentences.Length - 1;
    }
    //method to return to the top level of the dialogue
    public void ReturnToRoot()
    {
        SelectNextDialogue(rootDialogue);
    }
    //reference to a collider to trigger when in range of an NPC
    //toggle control bool
    //reset dialogue
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Player.instance.tag))
        {
            inRange = true;
            ReturnToRoot();
        }
    }
    //reference to a collider to trigger when out of range of an NPC
    //toggle control bool
    //reset dialogue
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Player.instance.tag))
        {
            inRange = false;
            active = false;
            ReturnToRoot();
        }
    }
    //if dialogue controller is active
    //create IMGUI labels and buttons for dialogue and quest notifications
    public void OnGUI()
    {
        if (!active)
        {
            return;
        }
        //display quest if requirements are met
        GUI.Box(new Rect(610, 510, 500, 300), name);
        QuestBase completedQuest = activeQuests.FirstOrDefault(x => x.status == QuestStatus.RequirementsReached);

        //if completed quest is not empty
        //create label of completed quest name, completed
        //create button to claim rewards, call complete method
        //return to top level of dialogue
        if (completedQuest != null)
        {
            GUI.Label(new Rect(620, 540, 450, 20), completedQuest.name + " completed!");
            if (GUI.Button(new Rect(620, 580, 450, 20), "Claim rewards"))
            {
                completedQuest.Complete();
                ReturnToRoot();
            }
            return;
        }
        //label to display current quest rewards for gold and experience
        if (currentDialogue.quest != null)
        {
            GUI.Label(new Rect(620, 540, 450, 20), currentDialogue.quest.description);
            string rewardText = $"Reward: {currentDialogue.quest.reward.gold} gold, {currentDialogue.quest.reward.exp} exp";

            GUI.Label(new Rect(620, 580, 450, 20), rewardText);
            //button to handle accepting a quest, instantiate current dialogue for quest
            //add new quest to active quests
            //call accept against new quest
            if (GUI.Button(new Rect(620, 620, 450, 20), "Deal <Accept>"))
            {
                QuestBase newQuest = Instantiate(currentDialogue.quest);
                activeQuests.Add(newQuest);
                newQuest.Accept();
            }
            //button to handle declining a quest, return to top level of dialogue
            if (GUI.Button(new Rect(620, 660, 450, 20), "I got better things to do <Decline>"))
            {
                ReturnToRoot();
            }
            return;
        }
        //create label to display current dialogue in index array
        GUI.Label(new Rect(620, 540, 450, 20), currentDialogue.sentences[currentSentenceIndex]);
        //if end of dialogue in index array
        if (endOfDialogueReached)
        {
            int i = 1;
            //loop through the variants of dialogue flow
            if (currentDialogue.dialogueVariants.Length > 0)
            {
                foreach (Dialogue dialogue in currentDialogue.dialogueVariants)
                {   
                    //continue if dialogue is quest related
                    if (dialogue.quest != null && (dialogue.quest.status == QuestStatus.Completed || dialogue.quest.status == QuestStatus.RequirementsReached || dialogue.quest.status == QuestStatus.Accepted))
                    {
                        continue;
                    }
                    //cycle through dialogue with player response
                    if (GUI.Button(new Rect(620, 540 + 40 * i, 450, 20), dialogue.playerSpeech))
                    {
                        SelectNextDialogue(dialogue);
                    }

                    i++;
                }
            }
            //else display dialogue flow control buttons
            else
            {
                //return button to go back to top level of dialogue
                if (GUI.Button(new Rect(620, 580, 450, 20), "Return"))
                {
                    ReturnToRoot();
                }
            }
            //bye button to end current dialogue session
            if (rootDialogue == currentDialogue)
            {
                if (GUI.Button(new Rect(620, 540 + 40 * i, 450, 20), "Bye"))
                {
                    active = false;
                }
            }
        }
        else
        {
            //continue button to call next sentance and advance through dialogue session
            if (GUI.Button(new Rect(620, 580, 450, 20), "Continue"))
            {
                NextSentence();
            }
        }
    }
}
