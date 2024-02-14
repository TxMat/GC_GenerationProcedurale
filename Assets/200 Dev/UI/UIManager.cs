using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NPCGenerator npcGenerator;

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
        npcList = new(list);

        for (int i = 0; i < Mathf.Min(list.Count, npcButtons.Count); i++)
        {
            npcButtons[i].Assign(list[i], i, OnSelectNPC);
        }

        OnSelectNPC(0);
    }

    #endregion

    #region Selection

    private List<NPC> npcList = new();

    public int CurrentNPCIndex { get; private set; }

    private void OnSelectNPC(int index)
    {
        CurrentNPCIndex = index;
        NPC currentNPC = npcList[CurrentNPCIndex];

        jobDropdown.SetValueWithoutNotify((int)currentNPC.TraitsMix.job.Job);
        statusDropdown.SetValueWithoutNotify((int)currentNPC.TraitsMix.status.Status);
        personnalityDropdown.SetValueWithoutNotify((int)currentNPC.TraitsMix.personnality.Personality);
        lifestyleDropdown.SetValueWithoutNotify((int)currentNPC.TraitsMix.lifestyle.Lifestyle);

        summaryText.text = currentNPC.Summary;

        uiPortrait.Generate(currentNPC.Portrait);
    }

    #endregion

    #region NPC Modification

    public void OnChangeJob(int newJobIndex)
    {
        NPC newNPC = npcGenerator.RegenerateWithNewJob(npcList[CurrentNPCIndex], newJobIndex);
        npcList[CurrentNPCIndex] = newNPC;
        npcButtons[CurrentNPCIndex].UpdateNPC(newNPC);

        OnSelectNPC(CurrentNPCIndex);
    }
    public void OnChangeStatus(int newStatusIndex)
    {
        NPC newNPC = npcGenerator.RegenerateWithNewStatus(npcList[CurrentNPCIndex], newStatusIndex);
        npcList[CurrentNPCIndex] = newNPC;
        npcButtons[CurrentNPCIndex].UpdateNPC(newNPC);

        OnSelectNPC(CurrentNPCIndex);
    }
    public void OnChangePersonnality(int newPersoIndex)
    {
        NPC newNPC = npcGenerator.RegenerateWithNewPersonnality(npcList[CurrentNPCIndex], newPersoIndex);
        npcList[CurrentNPCIndex] = newNPC;
        npcButtons[CurrentNPCIndex].UpdateNPC(newNPC);

        OnSelectNPC(CurrentNPCIndex);
    }
    public void OnChangeLifestyle(int newLifestyleIndex)
    {
        NPC newNPC = npcGenerator.RegenerateWithNewLifestyle(npcList[CurrentNPCIndex], newLifestyleIndex);
        npcList[CurrentNPCIndex] = newNPC;
        npcButtons[CurrentNPCIndex].UpdateNPC(newNPC);

        OnSelectNPC(CurrentNPCIndex);
    }

    #endregion
}
