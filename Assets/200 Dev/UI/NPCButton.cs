using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCButton : MonoBehaviour
{
    [Header("NPC Button")]
    [SerializeField] private UIPortrait portrait;

    public NPC NPC { get; private set; }
    private Action<NPC> onClick;

    public void Assign(NPC npc, Action<NPC> _onClick)
    {
        NPC = npc;
        onClick = _onClick;
    }


    public void OnClick()
    {
        onClick?.Invoke(NPC);
    }
}
