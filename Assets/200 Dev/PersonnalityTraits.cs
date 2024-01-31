using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonnalityTraits : Traits
{
    private void OnValidate()
    {
        name = "PT_" + ID;
    }
}
