using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(TraitsDatabase))]
public class TraitsDatabaseEditor : Editor
{
    protected TraitsDatabase database;

    protected ReorderableList jobList;
    protected ReorderableList statusList;
    protected ReorderableList personnalityList;
    protected ReorderableList lifestyleList;

    private void OnEnable()
    {
        database = (TraitsDatabase)target;

        jobList = CreateList<JobTraits>(Category.JOB, "jobTraits", "Job Traits");
        statusList = CreateList<StatusTraits>(Category.STATUS, "statusTraits", "Status Traits");
        personnalityList = CreateList<PersonnalityTraits>(Category.PERSONNALITY, "personnalityTraits", "Personnality Traits");
        lifestyleList = CreateList<LifestyleTraits>(Category.LIFESTYLE, "lifestyleTraits", "Lifestyle Traits");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Traits Database");

        EditorGUILayout.Space(15f);
        
        jobList.DoLayoutList();
        EditorGUILayout.Space(5f);

        statusList.DoLayoutList();
        EditorGUILayout.Space(5f);

        personnalityList.DoLayoutList();
        EditorGUILayout.Space(5f);

        lifestyleList.DoLayoutList();

        serializedObject.ApplyModifiedProperties();
    }

    
    private ReorderableList CreateList<T>(Category category, string propertyName, string header) where T : Traits
    {
        serializedObject.Update();
        SerializedProperty list = serializedObject.FindProperty(propertyName);

        return new ReorderableList(serializedObject, list, false, true, true, false)
        {
            drawHeaderCallback = (rect) =>
            {
                DrawListHeader(rect, header);
            },

            drawElementCallback = (rect, index, active, focused) =>
            {
                DrawListElement<T>(category, list, rect, index, active, focused);
            },

            onAddCallback = (list) =>
            {
                AddListElement<T>(category, list);
            },

            //onRemoveCallback = (list) =>
            //{
            //    RemoveListElement<T>(category, list);
            //},

            elementHeightCallback = (index) =>
            {
                return GetListElementHeight(list, index);
            }
        };
    }

    protected virtual void DrawListHeader(Rect rect, string header)
    {
        EditorGUI.LabelField(rect, header);
    }
    protected virtual void DrawListElement<T>(Category category, SerializedProperty list, Rect rect, int index, bool active, bool focused) where T : Traits
    {
        var element = list.GetArrayElementAtIndex(index);

        T data = database.GetAtIndex<T>(category, index);

        EditorGUI.indentLevel++;
        EditorGUI.BeginDisabledGroup(true);

        EditorGUI.PropertyField(
            new Rect(rect.x, rect.y, rect.width - 120f, rect.height), element, GUIContent.none, true);

        EditorGUI.EndDisabledGroup();

        EditorGUI.LabelField(
            new Rect(rect.x + rect.width - 120f, rect.y, 50f, rect.height), data.ID.ToString());

        if (GUI.Button(
                    new Rect(rect.x + rect.width - 65f, rect.y + 2, 65f, rect.height - 4), "Edit"))
        {
            OnEditListElement(data);
        }

        EditorGUI.indentLevel--;
    }

    protected virtual void AddListElement<T>(Category category, ReorderableList list) where T : Traits
    {
        //database.CreateNewData<T>(category);
        database.FillDatabase<T>(category);
    }
    protected virtual void RemoveListElement<T>(Category category, ReorderableList list) where T : Traits
    {
        database.DeleteDataAtIndex<T>(category, list.index);
    }

    protected virtual float GetListElementHeight(SerializedProperty list, int index)
    {
        return EditorGUI.GetPropertyHeight(list.GetArrayElementAtIndex(index));
    }

    protected virtual void OnEditListElement<T>(T data) where T : Traits
    {
        AssetDatabase.OpenAsset(data);
    }
}
