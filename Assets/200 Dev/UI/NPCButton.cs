using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCButton : MonoBehaviour
{
    [Header("NPC Button")]
    [SerializeField] private UIPortrait portrait;

    public int NPCIndex { get; private set; }
    public NPC NPC { get; private set; }
    private Action<int> onClick;

    public void Assign(NPC npc, int index, Action<int> _onClick)
    {
        NPC = npc;
        NPCIndex = index;
        onClick = _onClick;

        portrait.Generate(npc.Portrait);
    }
    public void UpdateNPC(NPC npc)
    {
        NPC = npc;

        portrait.Generate(npc.Portrait);
    }


    public void OnClick()
    {
        onClick?.Invoke(NPCIndex);
    }
}
