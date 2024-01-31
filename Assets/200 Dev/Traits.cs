using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traits : ScriptableObject
{
    [SerializeField] private int id;
    public int ID => id;

    [SerializeField] protected TraitTags tags;

    [Space(10f)]

    [SerializeField][TextArea] protected string manText;
    [SerializeField][TextArea] protected string womanText;

    [Header("Exclude")]
    [SerializeField] private TraitTags excludeTags;

    public virtual void Init(int _id)
    {
        id = _id;
    }
}