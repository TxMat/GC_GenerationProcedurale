using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifestyleTraits : Traits
{
    [Header("Lifestyle")]
    [SerializeField] private LifeStyle lifestyle;


    public override void Init(int _id)
    {
        base.Init(_id);

        name = "LT_" + lifestyle;
    }

    private void OnValidate()
    {
        name = "LT_" + lifestyle;
    }
}
