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

    public void Generate()
    {
        InitRandom();
        GenerateNPCs();
    }

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

        bool man = Random.value < 0.5f;
        // Generate name
        string npcName = NameGenerator.GenerateName(man);

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

        return GenerateNPC(man, job, status, personnality, lifestyle, npcName);
    }
    private NPC GenerateNPC(bool man, 
        JobTraits job, StatusTraits status, PersonnalityTraits personnality, LifestyleTraits lifestyle, 
        string name)
    {
        TraitsMix traitsMix;
        Portrait portrait;
        string summary;

        // Create Traits Mix
        traitsMix = new(job, status, personnality, lifestyle);

        // Create Portrait
        portrait = PortraitGenerator.Generate(man, traitsMix);

        // Create Summary
        summary = _200_Dev.TextGenerator.GenerateText(traitsMix, man, name);

        // Return NPC
        return new NPC(man, traitsMix, portrait, summary, name);
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
        if (traits.Count == 0)
        {
            Debug.LogError("All traits excluded");
            return null;
        }

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

        if (ponderateTraits.Count == 0)
        {
            Debug.LogError("No traits compatible");
            return null;
        }
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
            if (status == null) return CleanRegenerateWithJob(npc, job);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;
        
        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
            if (personnality == null) return CleanRegenerateWithJob(npc, job);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
            if (lifestyle == null) return CleanRegenerateWithJob(npc, job);
        }

        // Return NPC
        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }
    private NPC CleanRegenerateWithJob(NPC npc, JobTraits job)
    {
        StatusTraits status;
        PersonnalityTraits personnality;
        LifestyleTraits lifestyle;

        TraitTags tags = job.Tags;
        TraitTags excludedTags = job.ExcludeTags;

        status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);

        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }

    public NPC RegenerateWithNewStatus(NPC npc, int newStatusIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = database.GetTraitByIndex<StatusTraits>(Category.STATUS, newStatusIndex);
        PersonnalityTraits personnality = npc.TraitsMix.personnality;
        LifestyleTraits lifestyle = npc.TraitsMix.lifestyle;

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
            if (job == null) return CleanRegenerateWithStatus(npc, status);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
            if (personnality == null) return CleanRegenerateWithStatus(npc, status);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
            if (lifestyle == null) return CleanRegenerateWithStatus(npc, status);
        }

        // Return NPC
        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }
    private NPC CleanRegenerateWithStatus(NPC npc, StatusTraits status)
    {
        JobTraits job;
        PersonnalityTraits personnality;
        LifestyleTraits lifestyle;

        TraitTags tags = status.Tags;
        TraitTags excludedTags = status.ExcludeTags;

        job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        tags |= job.Tags;
        excludedTags |= job.ExcludeTags;

        personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);

        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }

    public NPC RegenerateWithNewPersonnality(NPC npc, int newPersoIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = npc.TraitsMix.status;
        PersonnalityTraits personnality = database.GetTraitByIndex<PersonnalityTraits>(Category.PERSONNALITY, newPersoIndex);
        LifestyleTraits lifestyle = npc.TraitsMix.lifestyle;

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
            if (job == null) return CleanRegenerateWithPersonnality(npc, personnality);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Status Compatibility
        if (!statusCompatible)
        {
            status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
            if (status == null) return CleanRegenerateWithPersonnality(npc, personnality);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Lifestyle Compatibility
        if (!lifestyleCompatible)
        {
            lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);
            if (lifestyle == null) return CleanRegenerateWithPersonnality(npc, personnality);
        }

        // Return NPC
        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }
    private NPC CleanRegenerateWithPersonnality(NPC npc, PersonnalityTraits personnality)
    {
        JobTraits job;
        StatusTraits status;
        LifestyleTraits lifestyle;

        TraitTags tags = personnality.Tags;
        TraitTags excludedTags = personnality.ExcludeTags;

        job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        tags |= job.Tags;
        excludedTags |= job.ExcludeTags;

        status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        lifestyle = GetCoherentTraits<LifestyleTraits>(Category.LIFESTYLE, tags, excludedTags);

        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }

    public NPC RegenerateWithNewLifestyle(NPC npc, int newLifestyleIndex)
    {
        JobTraits job = npc.TraitsMix.job;
        StatusTraits status = npc.TraitsMix.status;
        PersonnalityTraits personnality = npc.TraitsMix.personnality;
        LifestyleTraits lifestyle = database.GetTraitByIndex<LifestyleTraits>(Category.LIFESTYLE, newLifestyleIndex);

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
            if (job == null) return CleanRegenerateWithLifestyle(npc, lifestyle);
        }
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        // Status Compatibility
        if (!statusCompatible)
        {
            status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
            if (status == null) return CleanRegenerateWithLifestyle(npc, lifestyle);
        }
        tags |= personnality.Tags;
        excludedTags |= personnality.ExcludeTags;

        // Personnality Compatibility
        if (!personnalityCompatible)
        {
            personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);
            if (personnality == null) return CleanRegenerateWithLifestyle(npc, lifestyle);
        }

        // Return NPC
        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }
    private NPC CleanRegenerateWithLifestyle(NPC npc, LifestyleTraits lifestyle)
    {
        JobTraits job;
        StatusTraits status;
        PersonnalityTraits personnality;

        TraitTags tags = lifestyle.Tags;
        TraitTags excludedTags = lifestyle.ExcludeTags;

        job = GetCoherentTraits<JobTraits>(Category.JOB, tags, excludedTags);
        tags |= job.Tags;
        excludedTags |= job.ExcludeTags;

        status = GetCoherentTraits<StatusTraits>(Category.STATUS, tags, excludedTags);
        tags |= status.Tags;
        excludedTags |= status.ExcludeTags;

        personnality = GetCoherentTraits<PersonnalityTraits>(Category.PERSONNALITY, tags, excludedTags);

        return GenerateNPC(npc.Man, job, status, personnality, lifestyle, npc.Name);
    }

    private bool AreCompatible(TraitTags tags, TraitTags excludedTags, Traits trait2)
    {
        return !excludedTags.Any(trait2.Tags) && !trait2.ExcludeTags.Any(tags);
    }

    #endregion
}
