using UnityEngine;

//script to handle on screen journal (IMGUI)
public class QuestsUi : MonoBehaviour
{
    private bool journalIsActive;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            journalIsActive = !journalIsActive;
        }
    }
    public void OnGUI()
    {
        //if journal screen is active
        //display IMGUI box titled journal
        //loop through quests in quest instance into an array
        //display quest title and description
        //for each quest call get completuib progress and display
        //display cancel button, call quest cancel if pushed
        if (journalIsActive)
        {
            GUI.Box(new Rect(20, 200, 250, 450), "Journal");

            int i = 0;
            int y = 0;
            foreach (QuestBase quest in Player.instance.quests.ToArray())
            {
                GUI.Label(new Rect(40, 240 + i * 40 + y * 40, 220, 20), quest.title);
                GUI.Label(new Rect(40, 260 + i * 40 + y * 40, 220, 20), quest.description);

                foreach (string req in quest.GetCompletionProgress())
                {
                    GUI.Label(new Rect(40, 280 + i * 40 + y * 40, 220, 20), req);
                    y++;
                }

                if (GUI.Button(new Rect(40, 260 + i * 40 + y * 40, 220, 20), "Cancel"))
                {
                    quest.Cancel();
                }

                i++;
            }

        }
    }
}
