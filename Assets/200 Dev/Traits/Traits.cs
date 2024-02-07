using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traits : ScriptableObject
{
    [SerializeField] private int id;
    public int ID => id;

    [SerializeField] protected TraitTags tags;
    public TraitTags Tags => tags;

    [Space(10f)]

    [SerializeField][TextArea] protected List<string> descriptionTexts;
    
    public List<string> DescriptionTexts => descriptionTexts;

    [Header("Exclude")]
    [SerializeField] protected TraitTags excludeTags;
    public TraitTags ExcludeTags => tags;

    public virtual void Init(int _id)
    {
        id = _id;
    }

    public abstract void SetTraitsValue(int index);
}