using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTraits : Traits
{
    [Header("Status")]
    [SerializeField] private Status status;

    public override void Init(int _id)
    {
        base.Init(_id);

        name = "ST_" + status;
    }

    private void OnValidate()
    {
        name = "ST_" + status;
    }
}
