
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(Recipe))]
public class RecipeMaker : Editor
{
    public List<string> allIng = new List<string>();
    void populateIngList()
    {
        allIng.Clear();
        foreach(string id in AssetDatabase.FindAssets("t:Ingredient"))
        {
            string path = AssetDatabase.GUIDToAssetPath(id);
            Ingredient ing = AssetDatabase.LoadAssetAtPath<Ingredient>(path);
            if (ing != null)
            {
                allIng.Add(ing.ItemName);
            }
        }
        
    }

    void partsMenu(Recipe r, VisualElement parts)
    {
        parts.Clear();
        r.CollapseParts();
        foreach (RecipePart part in r.parts)
        {
            parts.Add(new Label(part.type + " - " + part.amount));
            var b = new Button(() =>
            {
                r.parts.Remove(part);

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(r);
                this.partsMenu(r, parts);
            });
            b.Add(new Label("remove"));
            parts.Add(b);
        }
    }
    public override VisualElement CreateInspectorGUI()
    {

        serializedObject.Update();
        VisualElement v = new VisualElement();

        v.Add(new IMGUIContainer(OnInspectorGUI));
        v.Add(new Label("Recipe Creater"));
        populateIngList();
        if (target is Recipe r)
        {
            v.Add(new Label("Current "));
            VisualElement parts = new VisualElement();
            partsMenu(r, parts);
            v.Add(parts);
            v.Add(new Label("Add Ing"));
            var typeselect = new PopupField<string>();
            typeselect.choices = allIng;
            typeselect.RegisterValueChangedCallback((evt) =>
            {
                if(evt.newValue == "")
                {
                    return;
                }
                Debug.Log(r.parts.Count);
                r.parts.Add(new RecipePart(evt.newValue, 1));

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(r);
                typeselect.value = "";
                this.partsMenu(r, parts);
            });
            v.Add(typeselect);
        }
        else
        {
            throw new Exception("Non recipe with recipe ui element");
        }

        return v;
    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }
}
