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

    #endregion

    #region Initialization

    private void Awake()
    {
        seed = baseSeed;
        InitRandom();
    }

    public void InitRandom()
    {
        Random.InitState(seed);
    }

    #endregion

    #region Generation

    public NPC GenerateRandom()
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

        return ponderateTraits[Random.Range(0, ponderateTraits.Count)];
    }

    #endregion
}
