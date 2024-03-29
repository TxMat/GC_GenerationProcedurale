using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Portrait

public struct Portrait
{
    public Portrait(Sprite _skinSprite, Color _skinColor, Sprite _hairSprite, int _hairSpriteIndex, Color _hairColor, 
        Sprite _clothesSprite, 
        bool _hasClothesAccessory, Sprite _clothesAccessory,
        bool _hasAccessory, Sprite _accessory)
    {
        skinSprite = _skinSprite;
        skinColor = _skinColor;
        hairSprite = _hairSprite;
        hairSpriteIndex = _hairSpriteIndex;
        hairColor = _hairColor;
        clothesSprite = _clothesSprite;
        hasClothesAccessory = _hasClothesAccessory;
        clothesAccessory = _clothesAccessory;
        hasAccessory = _hasAccessory;
        accessory = _accessory;
    }

    public Sprite skinSprite;
    public Color skinColor;

    public Sprite hairSprite;
    public int hairSpriteIndex;
    public Color hairColor;

    public Sprite clothesSprite;

    public bool hasClothesAccessory;
    public Sprite clothesAccessory;
    
    public bool hasAccessory;
    public Sprite accessory;
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

    [SerializeField] private Sprite baseSkinSprite;
    [SerializeField] private List<Color> skinColors;

    [Space(10f)]

    [SerializeField] private List<Sprite> manHairs;
    [SerializeField] private List<Sprite> womanHairs;

    [Space(5f)]

    [SerializeField] private List<Sprite> manHairsWithHelmet;
    [SerializeField] private List<Sprite> womanHairsWithHelmet;

    [Space(5f)]

    [SerializeField] private List<Color> hairColors;

    #endregion

    #region Generation

    public static Portrait Generate(bool man, TraitsMix traitsMix)
    {
        if (Instance == null) return default;

        bool overrideSkin = traitsMix.personnality.OverrideSkin(out Sprite skinSprite);
        (Sprite hairSprite, int hairSpriteIndex) = Instance.GenerateHairSprite(man, traitsMix.job.WearsHelmet);
        bool hasClothesAccessory = traitsMix.status.HasAccessory(out Sprite clothesAccessory);
        bool hasAccessory = traitsMix.lifestyle.HasAccessory(out Sprite accessory);

        return new Portrait(
            overrideSkin ? skinSprite : Instance.baseSkinSprite,
            Instance.GenerateSkinColor(),
            hairSprite, hairSpriteIndex,
            Instance.GenerateHairColor(),
            traitsMix.job.GetClothesSprite(man),
            hasClothesAccessory, clothesAccessory,
            hasAccessory, accessory);
    }

    public static Portrait UpdatePortrait(bool man, Portrait basePortrait, TraitsMix traitsMix)
    {
        if (Instance == null) return default;

        bool overrideSkin = traitsMix.personnality.OverrideSkin(out Sprite skinSprite);
        (Sprite hairSprite, int hairSpriteIndex) =
            Instance.GetHairSpriteWithIndex(basePortrait.hairSpriteIndex, man, traitsMix.job.WearsHelmet);
        bool hasClothesAccessory = traitsMix.status.HasAccessory(out Sprite clothesAccessory);
        bool hasAccessory = traitsMix.lifestyle.HasAccessory(out Sprite accessory);

        return new Portrait(
            overrideSkin ? skinSprite : Instance.baseSkinSprite,
            basePortrait.skinColor,
            hairSprite, hairSpriteIndex,
            basePortrait.hairColor,
            traitsMix.job.GetClothesSprite(man),
            hasClothesAccessory, clothesAccessory,
            hasAccessory, accessory);
    }

    private Color GenerateSkinColor()
    {
        return skinColors[Random.Range(0, skinColors.Count)];
    }

    private (Sprite, int) GenerateHairSprite(bool man, bool helmet)
    {
        int index = Random.Range(0, man ? manHairs.Count : womanHairs.Count);
        return (man ? 
            (helmet ? manHairsWithHelmet[index] : manHairs[index]) 
            : (helmet ? womanHairsWithHelmet[index] : womanHairs[index]) 
            , index);
    }
    private (Sprite, int) GetHairSpriteWithIndex(int index, bool man, bool helmet)
    {
        return (man ? 
            (helmet ? manHairsWithHelmet[index] : manHairs[index]) 
            : (helmet ? womanHairsWithHelmet[index] : womanHairs[index]) 
            , index);
    }

    private Color GenerateHairColor()
    {
        return hairColors[Random.Range(0, hairColors.Count)];
    }

    #endregion
}
