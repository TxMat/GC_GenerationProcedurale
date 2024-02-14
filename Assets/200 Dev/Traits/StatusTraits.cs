using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusTraits : Traits
{
    [Header("Status")]
    [SerializeField] private Status status;

    public Status Status => status;

    [Header("Eventual Accessories")]
    [SerializeField] protected bool hasAccessory;
    [SerializeField] protected List<Sprite> accessories;

    public bool HasAccessory(out Sprite _accessory)
    {
        _accessory = hasAccessory ? accessories[Random.Range(0, accessories.Count)] : null;
        return hasAccessory;
    }

    public override void SetTraitsValue(int index)
    {
        status = (Status)index;
        OnValidate();
    }

    public override string Name => status.Name();
    public override int TextTags => (int)status.TextTags();
    
    public override int TextGoodnessTags => (int)status.GoodnessTag();

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
