using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitsDatabase", menuName = "Traits Database")]
public class TraitsDatabase : ScriptableObject
{
    public List<JobTraits> jobTraits;

    public List<StatusTraits> statusTraits = new();

    public List<PersonnalityTraits> personnalityTraits = new();

    public List<LifestyleTraits> lifestyleTraits = new();

    #region Accessors

    public List<JobTraits> GetJobExcluding(TraitTags tags, TraitTags includedTags)
    {
        List<JobTraits> jobs = new();

        foreach (var job in jobTraits)
        {
            if (!job.Tags.Any(tags) && !includedTags.Any(job.ExcludeTags)) jobs.Add(job);
        }

        return jobs;
    }

    public List<StatusTraits> GetStatusExcluding(TraitTags tags, TraitTags includedTags)
    {
        List<StatusTraits> status = new();

        foreach (var s in statusTraits)
        {
            if (!s.Tags.Any(tags) && !includedTags.Any(s.ExcludeTags)) status.Add(s);
        }

        return status;
    }

    public List<PersonnalityTraits> GetPersonnalityExcluding(TraitTags tags, TraitTags includedTags)
    {
        List<PersonnalityTraits> personnalities = new();

        foreach (var p in personnalityTraits)
        {
            if (!p.Tags.Any(tags) && !includedTags.Any(p.ExcludeTags)) personnalities.Add(p);
        }

        return personnalities;
    }

    public List<LifestyleTraits> GetLifestyleExcluding(TraitTags tags, TraitTags includedTags)
    {
        List<LifestyleTraits> lifestyles = new();

        foreach (var l in lifestyleTraits)
        {
            if (!l.Tags.Any(tags) && !includedTags.Any(l.ExcludeTags)) lifestyles.Add(l);
        }

        return lifestyles;
    }

    #endregion

    #region Utility

    public List<T> GetByCategory<T>(Category category) where T : Traits
    {
        switch (category)
        {
            case Category.JOB:
                return jobTraits.Cast<T>().ToList();
            case Category.STATUS:
                return statusTraits.Cast<T>().ToList();
            case Category.PERSONNALITY:
                return personnalityTraits.Cast<T>().ToList();
            case Category.LIFESTYLE:
                return lifestyleTraits.Cast<T>().ToList();
        }
        return null;
    }

    private void AddByCategory<T>(Category category, T newElement) where T : Traits
    {
        switch (category)
        {
            case Category.JOB: jobTraits.Add(newElement as JobTraits); break;
            case Category.STATUS: statusTraits.Add(newElement as StatusTraits); break;
            case Category.PERSONNALITY: personnalityTraits.Add(newElement as PersonnalityTraits); break;
            case Category.LIFESTYLE: lifestyleTraits.Add(newElement as LifestyleTraits); break;
        }
    }
    private void RemoveByCategory(Category category, int index)
    {
        switch (category)
        {
            case Category.JOB: jobTraits.RemoveAt(index); break;
            case Category.STATUS: statusTraits.RemoveAt(index); break;
            case Category.PERSONNALITY: personnalityTraits.RemoveAt(index); break;
            case Category.LIFESTYLE: lifestyleTraits.RemoveAt(index); break;
        }
    }

    #endregion

    #region Editor Utility

#if UNITY_EDITOR

    public T GetAtIndex<T>(Category category, int index) where T : Traits
    {
        List<T> datas = GetByCategory<T>(category);

        if (index >= 0 && index < datas.Count)
            return datas[index];

        Debug.LogError("Index outside of range");
        return null;
    }

    public void FillDatabase<T>(Category category) where T : Traits
    {
        List<T> datas = GetByCategory<T>(category);
        string[] names = GetEnumNamesByCategory(category);

        int dataCount = datas.Count;
        T data;

        for (int i = 0; i < names.Length; i++)
        {
            if (dataCount > i) datas[i].SetTraitsValue(i);
            else
            {
                data = CreateNewData<T>(category);
                data.SetTraitsValue(i);
            }
        }

        if (dataCount > names.Length)
        {
            for (int i = names.Length; i < datas.Count; i++)
            {
                DeleteDataAtIndex<T>(category, i);
            }
        }
    }

    private string[] GetEnumNamesByCategory(Category category)
    {
        switch (category)
        {
            case Category.JOB: return Enum.GetNames(typeof(Jobs));
            case Category.STATUS: return Enum.GetNames(typeof(Status));
            case Category.PERSONNALITY: return Enum.GetNames(typeof(Personalities));
            case Category.LIFESTYLE: return Enum.GetNames(typeof(LifeStyle));
        }
        return null;
    }

    private T CreateNewData<T>(Category category) where T : Traits
    {
        string path = AssetDatabase.GetAssetPath(this);

        Undo.SetCurrentGroupName("Create new data");
        int undoGroup = Undo.GetCurrentGroup();

        T data = CreateInstance<T>();
        Undo.RegisterCreatedObjectUndo(data, "Create");
        data.hideFlags = HideFlags.None;
        data.Init(GenerateUniqueID<T>(category));

        AssetDatabase.AddObjectToAsset(data, path);

        Undo.RegisterCompleteObjectUndo(data, "Init");

        Undo.RecordObject(this, "Add to list");
        AddByCategory(category, data);

        AssetDatabase.SaveAssets();

        Undo.CollapseUndoOperations(undoGroup);

        return data;
    }
    public virtual void DeleteDataAtIndex<T>(Category category, int index) where T : Traits
    {
        List<T> datas = GetByCategory<T>(category);

        if (index >= 0 && index < datas.Count)
        {
            T data = datas[index];
            RemoveByCategory(category, index);
            AssetDatabase.RemoveObjectFromAsset(data);
        }
    }

    /// <returns>An array of the Data IDs in order</returns>
    public int[] GetIDs<T>(Category category) where T : Traits
    {
        List<T> datas = GetByCategory<T>(category);

        int count = datas.Count;

        if (count == 0) { return new int[0]; }

        int[] ids = new int[count];

        for (int i = 0; i < count; i++)
        {
            ids[i] = datas[i].ID;
        }

        return ids;
    }

    /// <returns>An array of the World Consequences Display Name in order</returns>
    //public (int[], string[]) GetIDsAndDisplayNames()
    //{
    //    int count = datas.Count;
    //
    //    if (count == 0) { return (new int[0], new string[0]); }
    //
    //    int[] ids = new int[count];
    //    string[] names = new string[count];
    //
    //    for (int i = 0; i < count; i++)
    //    {
    //        ids[i] = datas[i].ID;
    //        names[i] = GetPickerDisplayNameForIndex(i);
    //    }
    //
    //    return (ids, names);
    //}
    //protected virtual string GetPickerDisplayNameForIndex(int i)
    //{
    //    return datas[i].DisplayName + " (" + datas[i].ID + ")";
    //}

    protected int GenerateUniqueID<T>(Category category) where T : Traits
    {
        List<int> ids = GetIDs<T>(category).ToList();

        int newID;
        do
        {
            newID = UnityEngine.Random.Range(1, 100);
        } while (ids.Contains(newID));

        return newID;
    }
#endif

    #endregion
}


public struct TraitsMix
{
    public TraitsMix(JobTraits _job, StatusTraits _status, PersonnalityTraits _perso, LifestyleTraits _lifestyle)
    {
        job = _job;
        status = _status;
        personnality = _perso;
        lifestyle = _lifestyle;
    }

    public JobTraits job;
    public StatusTraits status;
    public PersonnalityTraits personnality;
    public LifestyleTraits lifestyle;
}
