using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPortrait : MonoBehaviour
{
    [Header("UI Portrait")]
    [SerializeField] private Image skinImage;
    [SerializeField] private Image hairImage;
    [SerializeField] private Image clothesImage;
    [SerializeField] private Image clothesAccessoryImage;
    [SerializeField] private Image accessoryImage;

    public Portrait Portrait { get; private set; }

    public void Generate(Portrait portrait)
    {
        skinImage.color = portrait.skinColor;
        hairImage.sprite = portrait.hairSprite;
        hairImage.color = portrait.hairColor;
        clothesImage.sprite = portrait.clothesSprite;

        clothesAccessoryImage.enabled = portrait.hasClothesAccessory;
        clothesAccessoryImage.sprite = portrait.clothesAccessory;

        accessoryImage.enabled = portrait.hasAccessory;
        accessoryImage.sprite = portrait.accessory;
    }
}
