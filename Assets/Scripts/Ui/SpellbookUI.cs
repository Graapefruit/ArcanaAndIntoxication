using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellbookUI : MonoBehaviour {
    public Spellbook spellbook;
    private SpellbookPageUI leftPage;
    private SpellbookPageUI rightPage;
    private GameObject leftButton;
    private GameObject rightButton;
    [SerializeField]
    private int page;
    // Start is called before the first frame update
    void Awake() {
        page = 0;
        leftPage = transform.Find("LeftPage").GetComponent<SpellbookPageUI>();
        rightPage = transform.Find("RightPage").GetComponent<SpellbookPageUI>();
        leftButton = transform.Find("LeftButton").gameObject;
        rightButton = transform.Find("RightButton").gameObject;
    }

    void Start() {
        displaySpells();
        setButtonActivity();
    }

    public void flipPageLeft() {
        if (page > 0) {
            page--;
            setButtonActivity();
            displaySpells();
        }
    }

    public void flipPageRight() {
        if ((page + 1) * 2 < spellbook.getSpellCount()) {
            page++;
            setButtonActivity();
            displaySpells();
        }
    }

    public void addNewSpell() {
        if (spellbook.getSpellCount() == 1) {
            leftPage.SpellIndex = 0;
            leftPage.gameObject.SetActive(true);
        }
        if (onLastPagePriorToNewSpell()) {
            if (onSecondPageFilledPriorToNewSpell()) {
                rightButton.SetActive(true);
            } else {
                rightPage.SpellIndex = spellbook.getSpellCount() - 1;
                rightPage.gameObject.SetActive(true);
            }
        }
    }

    private bool onLastPagePriorToNewSpell() {
        Debug.Log(page);
        return page == (spellbook.getSpellCount() / 2) - 1;
    }

    private bool onSecondPageFilledPriorToNewSpell() {
        return spellbook.getSpellCount() % 2 == 1;
    }

    private void setButtonActivity() {
        if (page <= 0) {
            leftButton.SetActive(false);
        } else {
            leftButton.SetActive(true);
        }
        if ((page + 1) * 2 >= spellbook.getSpellCount()) {
            rightButton.SetActive(false);
        } else {
            rightButton.SetActive(true);
        }
    }

    private void displaySpells() {
        if (spellbook.getSpellCount() > page * 2) {
            leftPage.gameObject.SetActive(true);
            leftPage.SpellIndex = page * 2;
        } else {
            leftPage.gameObject.SetActive(false);
        }
        if (spellbook.getSpellCount() > (page * 2) + 1) {
            rightPage.gameObject.SetActive(true);
            rightPage.SpellIndex = (page * 2) + 1;
        } else {
            rightPage.gameObject.SetActive(false);
        }
    }
}
