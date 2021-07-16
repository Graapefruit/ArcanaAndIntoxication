using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellbookPageUI : MonoBehaviour {
    public Spellbook spellbook;
    private readonly Color RED_OUTLINE = new Color(1.0f, 0.25f, 0.25f, 1.0f);
    private readonly Color BLUE_OUTLINE = new Color(0.28f, 0.69f, 1.0f, 1.0f);
    private readonly Color YELLOW_OUTLINE = new Color(1.0f, 0.85f, 0.25f, 1.0f);
    private readonly Color GREEN_OUTLINE = new Color(0.35f, 1.0f, 0.31f, 1.0f);
    private Image image;
    new private Text name;
    private Text cost;
    private Text cooldown;
    private Text description;
    private Image hotbarAssignmentWrapper;
    private int spellIndex;
    void Awake() {
        image = transform.Find("SpellImage").GetComponent<Image>();
        name = transform.Find("SpellName").GetComponent<Text>();
        cost = transform.Find("SpellCost").GetComponent<Text>();
        cooldown = transform.Find("SpellCooldown").GetComponent<Text>();
        description = transform.Find("SpellDescription").GetComponent<Text>();
        hotbarAssignmentWrapper = transform.Find("HotbarAssignmentWrapper").GetComponent<Image>();
    }

    public int SpellIndex {
        get { return spellIndex; }
        set { 
            spellIndex = value;
            SpellInfo spellInfo = spellbook.getSpell(spellIndex);
            image.sprite = spellInfo.sprite;
            name.text = spellInfo.name;
            cost.text = "Cost: "+ spellInfo.cost;
            cooldown.text = "Cooldown: " + spellInfo.cooldown;
            description.text = spellInfo.description;
            updateHotbarImage();
        }
    }

    public void updateHotbarImage() {
        int hotbarAssignment = spellbook.getSpellsHotbarMapping(spellIndex);
        switch (hotbarAssignment) {
            case 1:
                hotbarAssignmentWrapper.color = RED_OUTLINE;
                hotbarAssignmentWrapper.gameObject.SetActive(true);
                break;
            case 2:
                hotbarAssignmentWrapper.color = BLUE_OUTLINE;
                hotbarAssignmentWrapper.gameObject.SetActive(true);
                break;
            case 3:
                hotbarAssignmentWrapper.color = YELLOW_OUTLINE;
                hotbarAssignmentWrapper.gameObject.SetActive(true);
                break;
            case 4:
                hotbarAssignmentWrapper.color = GREEN_OUTLINE;
                hotbarAssignmentWrapper.gameObject.SetActive(true);
                break;
            default:
                hotbarAssignmentWrapper.gameObject.SetActive(false);
                break;
        }
    }
}
