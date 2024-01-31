using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Manager")]
    [SerializeField] private List<NPCButton> npcButtons;

    [Space(10f)]

    [SerializeField] private UIPortrait uiPortrait;
    [SerializeField] private TextMeshProUGUI summaryText;
    [Space(5f)]
    [SerializeField] private TMP_Dropdown jobDropdown;
    [SerializeField] private TMP_Dropdown statusDropdown;
    [SerializeField] private TMP_Dropdown personnalityDropdown;
    [SerializeField] private TMP_Dropdown lifestyleDropdown;


    #region Core Behaviour

    private void Awake()
    {
        Init();
    }

    #endregion

    #region Initialization

    public void Init()
    {
        jobDropdown.ClearOptions();
        List<string> list = new();
        foreach (var job in Enum.GetNames(typeof(Jobs)))
        {
            list.Add(job);
        }
        jobDropdown.AddOptions(list);

        list.Clear();
        statusDropdown.ClearOptions();
        foreach (var status in Enum.GetNames(typeof(Status)))
        {
            list.Add(status);
        }
        statusDropdown.AddOptions(list);

        list.Clear();
        personnalityDropdown.ClearOptions();
        foreach (var personnalities in Enum.GetNames(typeof(Personalities)))
        {
            list.Add(personnalities);
        }
        personnalityDropdown.AddOptions(list);
        
        list.Clear();
        lifestyleDropdown.ClearOptions();
        foreach (var lifestyle in Enum.GetNames(typeof(LifeStyle)))
        {
            list.Add(lifestyle);
        }
        lifestyleDropdown.AddOptions(list);
    }

    public void SetNPCs(List<NPC> list)
    {
        for (int i = 0; i < Mathf.Min(list.Count, npcButtons.Count); i++)
        {
            npcButtons[i].Assign(list[i], OnSelectNPC);
        }
    }

    #endregion

    #region Selection

    private void OnSelectNPC(NPC npc)
    {
        jobDropdown.SetValueWithoutNotify((int)npc.TraitsMix.job.Job);
        statusDropdown.SetValueWithoutNotify((int)npc.TraitsMix.status.Status);
        personnalityDropdown.SetValueWithoutNotify((int)npc.TraitsMix.personnality.Personality);
        lifestyleDropdown.SetValueWithoutNotify((int)npc.TraitsMix.lifestyle.Lifestyle);

        summaryText.text = npc.Summary;

        uiPortrait.Generate(npc.Portrait);
    }

    #endregion
}
