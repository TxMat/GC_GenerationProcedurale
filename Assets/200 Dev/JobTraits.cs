using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobTraits : Traits
{
    [Header("Job")]
    public Jobs job;

    public override void Init(int _id)
    {
        base.Init(_id);

        name = "JT_" + job;
    } 

    private void OnValidate()
    {
        name = "JT_" + job;
    }
}
