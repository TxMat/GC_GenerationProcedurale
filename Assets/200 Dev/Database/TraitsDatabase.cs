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

    public void CreateNewData<T>(Category category) where T : Traits
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
            newID = Random.Range(1, 100);
        } while (ids.Contains(newID));

        return newID;
    }
#endif

    #endregion
}


public struct TraitsMix
{
    public JobTraits job;
    public StatusTraits status;
    public PersonnalityTraits personnality;
    public LifestyleTraits lifestyle;
}
