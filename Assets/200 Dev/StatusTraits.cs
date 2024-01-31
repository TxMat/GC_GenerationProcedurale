using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTraits : Traits
{
    private void OnValidate()
    {
        name = "ST_" + ID;
    }
}
