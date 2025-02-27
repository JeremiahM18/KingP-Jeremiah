using TMPro;
using UnityEngine;

public class CharacterManager : MonoBehaviour {
    public Pins pinsDB;
    public SpriteRenderer sprite;
    public TMP_Text nameLabel;

    public static int selection = 0;

    private void Start()
    {
        updateCharacter();
    }
    public void updateCharacter()
    {
        if (pinsDB == null)
        {
            return;
        }
        Pin current = pinsDB.getPin(selection);

        if (current != null && current.prefab != null)
        {
            SpriteRenderer prefabSprite = current.prefab.GetComponent<SpriteRenderer>();
            if (prefabSprite != null)
            {
                sprite.sprite = prefabSprite.sprite;
            }
        }
        nameLabel.SetText(current.name);

    }
    public void next()
    {
        int numberPins = pinsDB.getCount();

        if (selection < numberPins - 1)
        {
            selection++;
        }
        else
        {
            selection = 0;
        }
        updateCharacter();
    }

    public void previous()
    {
        if (selection > 0)
        {
            selection--;
        } else
        {
            selection = pinsDB.getCount() -1;
        }
        updateCharacter();
    }

}