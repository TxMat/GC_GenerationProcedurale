using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traits
{
    
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