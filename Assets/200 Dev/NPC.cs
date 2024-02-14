using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
    
    public NPC(bool man, TraitsMix traits, Portrait portrait, string summary)
    {
        Man = man;
        TraitsMix = traits;
        Portrait = portrait;
        Summary = summary;
    }
    
    public NPC(bool man, TraitsMix traits, Portrait portrait, string summary, string name)
    {
        Man = man;
        TraitsMix = traits;
        Portrait = portrait;
        Summary = summary;
        Name = name;
    }

    public bool Man { get; private set; }
    public TraitsMix TraitsMix { get; private set; }
    public Portrait Portrait { get; private set; }
    public string Summary { get; private set; }
    public string Name { get; private set; }
}
