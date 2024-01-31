using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitsDatabase", menuName = "Traits Database")]
public class TraitsDatabase : ScriptableObject
{
    [SerializeField] private List<JobTraits> jobTraits;


    #region Utility

    public List<T> GetByCategory<T>(Category category) where T : Traits
    {
        switch (category)
        {
            case Category.JOB:
                return jobTraits.Cast<T>().ToList();
        }
        return null;
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
        List<T> datas = GetByCategory<T>(category);

        string path = AssetDatabase.GetAssetPath(this);

        Undo.SetCurrentGroupName("Create new data");
        int undoGroup = Undo.GetCurrentGroup();

        T data = CreateInstance<T>();
        Undo.RegisterCreatedObjectUndo(data, "Create");
        data.hideFlags = HideFlags.None;

        AssetDatabase.AddObjectToAsset(data, path);

        Undo.RegisterCompleteObjectUndo(data, "Init");

        Undo.RecordObject(this, "Add to list");
        datas.Add(data);

        AssetDatabase.SaveAssets();

        Undo.CollapseUndoOperations(undoGroup);
    }
    public virtual void DeleteDataAtIndex<T>(Category category, int index) where T : Traits
    {
        List<T> datas = GetByCategory<T>(category);

        if (index >= 0 && index < datas.Count)
        {
            T data = datas[index];
            datas.Remove(data);
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
