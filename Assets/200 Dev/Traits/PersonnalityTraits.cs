using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnalityTraits : Traits
{
    [Header("Personnality")]
    [SerializeField] private Personalities personalities;

    public Personalities Personality => personalities;

    [Header("Face")]
    [SerializeField] protected bool overrideSkin;
    [SerializeField] protected Sprite skin;

    public bool OverrideSkin(out Sprite _skin)
    {
        _skin = overrideSkin ? skin : null;
        return overrideSkin;
    }

    public override void SetTraitsValue(int index)
    {
        personalities = (Personalities)index;
        OnValidate();
    }

    public override string Name => personalities.Name();
    public override int TextGoodnessTags => (int)personalities.GoodnessTag();

    public override bool NeedsSuffix => personalities.NeedsSuffix();

    #region Editor
    public override void Init(int _id)
    {
        base.Init(_id);

        name = "PT_" + personalities;
    }

    private void OnValidate()
    {
        name = "PT_" + personalities;
    }
    #endregion
}
