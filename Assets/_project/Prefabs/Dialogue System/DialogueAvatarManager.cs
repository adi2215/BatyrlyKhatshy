using UnityEngine;
using UnityEngine.UI;
using TMPro; // Use this if you're working with TextMeshPro.

public class DialogueAvatarManager : MonoBehaviour
{
    public Image targetImage;          // The Image component to update.
    public TMP_Text textField;         // The text field (use Text if not using TextMeshPro).
    public Sprite defaultSprite;      // A fallback sprite if no match is found.

    // Create a dictionary to map text to sprites.
    public Sprite[] spriteOptions;
    public string[] textOptions; // Must match `spriteOptions` in size.
    public void Update()
    {
        string currentText = "";
        if (textField.text != null) {
            currentText = textField.text;
            //Debug.Log(currentText);
            Sprite newSprite = GetSpriteForText(currentText);
            targetImage.sprite = newSprite ?? defaultSprite;
        }
    }

    private Sprite GetSpriteForText(string text)
    {
        for (int i = 0; i < textOptions.Length; i++)
        {
            if (textOptions[i].Equals(text, System.StringComparison.OrdinalIgnoreCase))
                return spriteOptions[i];
        }
        return null; // Return null if no match is found.
    }
}
