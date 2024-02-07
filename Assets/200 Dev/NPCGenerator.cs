using _200_Dev;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    #region Global Members

    [Header("Database")]
    [SerializeField] private TraitsDatabase database;

    [Header("References")]
    [SerializeField] private UIManager uiManager;

    #endregion

    #region Seeding

    private int baseSeed = 15012001;
    private int seed;

    public string StrSeed
    {
        set
        {
            if (!int.TryParse(value, out seed))
            {
                seed = baseSeed;
            }
        }
    }

    public void InitRandom()
    {
        Random.InitState(seed);
    }

    #endregion

    #region Initialization

    private void Awake()
    {
        seed = baseSeed;
        InitRandom();
    }

    private void Start()
    {
        GenerateNPCs();
    }

    #endregion

    #region Generation

    private void GenerateNPCs()
    {
        List<NPC> npcs = new();
        for (int i = 0; i < 6; i++)
        {
            npcs.Add(GenerateRandom());
        }

        uiManager.SetNPCs(npcs);
    }

    private NPC GenerateRandom()
    {
        // Init
        TraitTags tags;
        TraitTags excludedTags;

        JobTraits job;
        StatusTraits status;
        PersonnalityTraits personnality;
        LifestyleTraits lifestyle;

        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        bool man = Random.value < 0.5f;

        // --- Get random job ---
        List<JobTraits> jobs = database.GetJobExcluding(TraitTags.NONE, TraitTags.NONE);
        job = jobs[Random.Range(0, jobs.Count)];
        tags = job.Tags;
        excludedTags = job.ExcludeTags;

        // --- Get random coherent status ---
        status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;
        
        // --- Get random coherent personnality ---
        personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;
        
        // --- Get random coherent lifestyle ---
        lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.Generate(man, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, man);

        // Return NPC
        return new NPC(man, traitsMix, portrait, summary);
    }

    private T GetCoherentTraits<T>(Category category, TraitTags tags, TraitTags excludedTags) where T : Traits
    {
        List<T> traits = new();
        switch (category)
        {
            case Category.JOB: traits = database.GetJobExcluding(excludedTags, tags).Cast<T>().ToList(); break;
            case Category.STATUS: traits = database.GetStatusExcluding(excludedTags, tags).Cast<T>().ToList(); break;
            case Category.PERSONNALITY: traits = database.GetPersonnalityExcluding(excludedTags, tags).Cast<T>().ToList(); break;
            case Category.LIFESTYLE: traits = database.GetLifestyleExcluding(excludedTags, tags).Cast<T>().ToList(); break;
        }
        if (traits.Count == 0) Debug.LogError("All traits excluded");

        List<T> ponderateTraits = new();

        int weight;
        foreach (var t in traits)
        {
            weight = (tags & t.Tags).HammingWeight();
            for (int i = 0; i < weight; i++)
            {
                ponderateTraits.Add(t);
            }
        }

        if (ponderateTraits.Count == 0) Debug.LogError("No traits compatible");
        return ponderateTraits[Random.Range(0, ponderateTraits.Count)];
    }

    #endregion

    #region Regeneration

    public NPC RegenerateWithNewJob(NPC npc, int newJobIndex)
    {
        JobTraits job = database.GetTraitByIndex<JobTraits>(Category.JOB, newJobIndex);
        StatusTraits status = npc.TraitsMix.status;
        PersonnalityTraits personnality = npc.TraitsMix.personnality;
        LifestyleTraits lifestyle = npc.TraitsMix.lifestyle;

        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        TraitTags tags = job.Tags;
        TraitTags excludedTags = job.ExcludeTags;

        bool statusCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.status);
        if (statusCompatible) { tags |= status.Tags; excludedTags |= status.ExcludeTags; }

        bool personnalityCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.personnality);
        if (personnalityCompatible) { tags |= personnality.Tags; excludedTags |= personnality.ExcludeTags; }

        bool lifestyleCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.lifestyle);
        if (lifestyleCompatible) { tags |= lifestyle.Tags; excludedTags |= lifestyle.ExcludeTags; }

        // Status Compatibility
        if (!statusCompatible)
        {
            status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;
        
        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
        }

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.UpdatePortrait(npc.Man, npc.Portrait, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, npc.Man);

        // Return NPC
        return new NPC(npc.Man, traitsMix, portrait, summary);
    }

    public NPC RegenerateWithNewStatus(NPC npc, int newStatusIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = database.GetTraitByIndex<StatusTraits>(Category.STATUS, newStatusIndex);
        PersonnalityTraits personnality = npc.TraitsMix.personnality;
        LifestyleTraits lifestyle = npc.TraitsMix.lifestyle;

        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        TraitTags tags = status.Tags;
        TraitTags excludedTags = status.ExcludeTags;

        bool jobCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.job);
        if (jobCompatible) { tags |= job.Tags; excludedTags |= job.ExcludeTags; }

        bool personnalityCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.personnality);
        if (personnalityCompatible) { tags |= personnality.Tags; excludedTags |= personnality.ExcludeTags; }

        bool lifestyleCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.lifestyle);
        if (lifestyleCompatible) { tags |= lifestyle.Tags; excludedTags |= lifestyle.ExcludeTags; }

        // Job Compatibility
        if (!jobCompatible)
        {
            job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
        }

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.UpdatePortrait(npc.Man, npc.Portrait, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, npc.Man);

        // Return NPC
        return new NPC(npc.Man, traitsMix, portrait, summary);
    }

    public NPC RegenerateWithNewPersonnality(NPC npc, int newPersoIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = npc.TraitsMix.status;
        PersonnalityTraits personnality = database.GetTraitByIndex<PersonnalityTraits>(Category.PERSONNALITY, newPersoIndex);
        LifestyleTraits lifestyle = npc.TraitsMix.lifestyle;

        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        TraitTags tags = personnality.Tags;
        TraitTags excludedTags = personnality.ExcludeTags;

        bool jobCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.job);
        if (jobCompatible) { tags |= job.Tags; excludedTags |= job.ExcludeTags; }

        bool statusCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.status);
        if (statusCompatible) { tags |= status.Tags; excludedTags |= status.ExcludeTags; }

        bool lifestyleCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.lifestyle);
        if (lifestyleCompatible) { tags |= lifestyle.Tags; excludedTags |= lifestyle.ExcludeTags; }

        // Job Compatibility
        if (!jobCompatible)
        {
            job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Status Compatibility
        if (!statusCompatible)
        {
            status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
        }

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.UpdatePortrait(npc.Man, npc.Portrait, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, npc.Man);

        // Return NPC
        return new NPC(npc.Man, traitsMix, portrait, summary);
    }

    public NPC RegenerateWithNewLifestyle(NPC npc, int newLifestyleIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = npc.TraitsMix.status;
        PersonnalityTraits personnality = npc.TraitsMix.personnality;
        LifestyleTraits lifestyle = database.GetTraitByIndex<LifestyleTraits>(Category.LIFESTYLE, newLifestyleIndex);

        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        TraitTags tags = lifestyle.Tags;
        TraitTags excludedTags = lifestyle.ExcludeTags;

        bool jobCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.job);
        if (jobCompatible) { tags |= job.Tags; excludedTags |= job.ExcludeTags; }

        bool statusCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.status);
        if (statusCompatible) { tags |= status.Tags; excludedTags |= status.ExcludeTags; }

        bool personnalityCompatible = AreCompatible(tags, excludedTags, npc.TraitsMix.personnality);
        if (personnalityCompatible) { tags |= personnality.Tags; excludedTags |= personnality.ExcludeTags; }

        // Job Compatibility
        if (!jobCompatible)
        {
            job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Status Compatibility
        if (!statusCompatible)
        {
            status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        }

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.UpdatePortrait(npc.Man, npc.Portrait, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, npc.Man);

        // Return NPC
        return new NPC(npc.Man, traitsMix, portrait, summary);
    }

    private bool AreCompatible(TraitTags tags, TraitTags excludedTags, Traits trait2)
    {
        return !excludedTags.Any(trait2.Tags) && !trait2.ExcludeTags.Any(tags);
    }

    #endregion
}
