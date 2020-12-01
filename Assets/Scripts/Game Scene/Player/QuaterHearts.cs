using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//method to handle UI health display as hearts
public class QuaterHearts : MonoBehaviour
{
    [SerializeField] private Image[] heartSlots;
    [SerializeField] private Sprite[] hearts;

    private float currentHealth;
    private float maximumHealth;
    private float healthPerSection;

    // Update is called once per frame
    public void UpdateHearts(float curHealth, float maxHealth)
    {
        currentHealth = curHealth;
        maximumHealth = maxHealth;

        healthPerSection = maxHealth / (heartSlots.Length * 4);
    }

    private void Update()
    {
        int heartSlotIndex = 0;

        foreach(Image image in heartSlots)
        {
            if(currentHealth >= ((healthPerSection * 4)) + healthPerSection * 4 * heartSlotIndex)
            {
                heartSlots[heartSlotIndex].sprite = hearts[0];
            }
            else if(currentHealth >= ((healthPerSection * 3)) + healthPerSection * 4 * heartSlotIndex)
            {
                heartSlots[heartSlotIndex].sprite = hearts[1];
            }
            else if (currentHealth >= ((healthPerSection * 2)) + healthPerSection * 4 * heartSlotIndex)
            {
                heartSlots[heartSlotIndex].sprite = hearts[2];
            }
            else if(currentHealth >= ((healthPerSection * 2)) + healthPerSection * 4 * heartSlotIndex)
            {
                heartSlots[heartSlotIndex].sprite = hearts[3];
            }
            else
            {
                heartSlots[heartSlotIndex].sprite = hearts[4];
            }

            heartSlotIndex++;
        }
    }

}
