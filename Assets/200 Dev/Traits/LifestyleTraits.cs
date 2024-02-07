using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifestyleTraits : Traits
{
    [Header("Lifestyle")]
    [SerializeField] private LifeStyle lifestyle;

    public LifeStyle Lifestyle => lifestyle;

    public override void SetTraitsValue(int index)
    {
        lifestyle = (LifeStyle)index;
        OnValidate();
    }

    public override string Name => lifestyle.Name();
    public override int TextTags => 0;

    #region Editor
    public override void Init(int _id)
    {
        base.Init(_id);

        name = "LT_" + lifestyle;
    }

    private void OnValidate()
    {
        name = "LT_" + lifestyle;
    }
    #endregion
}
