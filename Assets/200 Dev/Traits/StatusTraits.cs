using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTraits : Traits
{
    [Header("Status")]
    [SerializeField] private Status status;

    public Status Status => status;

    public override void SetTraitsValue(int index)
    {
        status = (Status)index;
        OnValidate();
    }

    public override string Name => status.Name();
    public override int TextTags => (int)status.TextTags();

    #region Editor
    public override void Init(int _id)
    {
        base.Init(_id);

        name = "ST_" + status;
    }

    private void OnValidate()
    {
        name = "ST_" + status;
    }
    #endregion
}
