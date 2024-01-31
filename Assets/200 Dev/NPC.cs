using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC
{
    public bool man;
    public TraitsMix TraitsMix { get; private set; }
    public Portrait Portrait { get; private set; }
    public string Summary { get; private set; }
}
