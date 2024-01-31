using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traits : ScriptableObject
{
    [SerializeField] private int id;
    public int ID => id;
}

public class JobTraits : Traits
{

}
public class StatusTraits : Traits
{

}
public class PersonnalityTraits : Traits
{

}

public enum Category
{
    JOB = 0,
    STATUS = 1,
    PERSONNALITY = 2,
}

public enum Jobs
{
    MED = 0,
    PAINTER = 1,
    ENGINEER = 2,
}


[Flags]
public enum TraitTags
{
    RICH = 1 << 0,
    POOR = 1 << 1,
}