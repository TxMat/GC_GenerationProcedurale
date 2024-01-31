using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Portrait

public struct Portrait
{
    public Portrait(Color _skinColor, Sprite _hairSprite, Color _hairColor, Sprite _clothesSprite)
    {
        skinColor = _skinColor;
        hairSprite = _hairSprite;
        hairColor = _hairColor;
        clothesSprite = _clothesSprite;
    }

    public Color skinColor;

    public Sprite hairSprite;
    public Color hairColor;

    public Sprite clothesSprite;
}

#endregion

public class PortraitGenerator : MonoBehaviour
{
    #region Singleton

    private static PortraitGenerator Instance { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    #endregion

    #region Global Members

    [Header("Portrait Generator")]

    [SerializeField] private List<Color> skinColors;

    [Space(10f)]

    [SerializeField] private List<Sprite> manHairs;
    [SerializeField] private List<Sprite> womanHairs;

    [Space(5f)]

    [SerializeField] private List<Color> hairColors;

    #endregion

    #region Generation

    public Portrait Generate(bool man, TraitsMix traitsMix)
    {
        return new Portrait(
            GenerateSkinColor(),
            GenerateHairSprite(man),
            GenerateHairColor(),
            traitsMix.jobTrait.GetClothesSprite(man)
            );
    }

    private Color GenerateSkinColor()
    {
        return skinColors[Random.Range(0, skinColors.Count)];
    }

    private Sprite GenerateHairSprite(bool man)
    {
        if (man) return manHairs[Random.Range(0, manHairs.Count)];
        return womanHairs[Random.Range(0, womanHairs.Count)];
    }

    private Color GenerateHairColor()
    {
        return hairColors[Random.Range(0, hairColors.Count)];
    }

    #endregion
}
