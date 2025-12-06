using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;

[CustomEditor(typeof(Abilities))]
public class AbilityEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Abilities ab = (Abilities)target;
        ab.abillities ??= new List<Abillity>();

        int numAbillities = ab.abillities.Count;
        for (int i = 0; i < numAbillities; i++)
        {
            Abillity abillity = ab.abillities[i];

            abillity.name = EditorGUILayout.TextField("Abillity name: ", abillity?.name ?? "");
            EditorGUILayout.LabelField("Abillity image: ");
            abillity.image = (Sprite)EditorGUILayout.ObjectField(abillity?.image ?? null, typeof(Sprite), false, GUILayout.Width(64), GUILayout.Height(64));
            GUILayout.Space(16);
            EditorGUILayout.LabelField("Attributes: ");
            abillity.attributes[0].name = EditorGUILayout.TextField("1 name: ", abillity?.attributes[0].name ?? "");
            abillity.attributes[0].modifier = EditorGUILayout.FloatField("1 modifier: ", abillity?.attributes[0].modifier ?? 0);
            abillity.attributes[0].type = (Abillity.Attribute.Type)EditorGUILayout.EnumPopup("1 type: ", abillity.attributes[0].type);

            abillity.attributes[1].name = EditorGUILayout.TextField("2 name: ", abillity?.attributes[1].name ?? "");
            abillity.attributes[1].modifier = EditorGUILayout.FloatField("2 modifier: ", abillity?.attributes[1].modifier ?? 0);
            abillity.attributes[1].type = (Abillity.Attribute.Type)EditorGUILayout.EnumPopup("2 type: ", abillity.attributes[1].type);

            abillity.attributes[2].name = EditorGUILayout.TextField("3 name: ", abillity?.attributes[2].name ?? "");
            abillity.attributes[2].modifier = EditorGUILayout.FloatField("3 modifier: ", abillity?.attributes[2].modifier ?? 0);
            abillity.attributes[2].type = (Abillity.Attribute.Type)EditorGUILayout.EnumPopup("3 type: ", abillity.attributes[2].type);

            abillity.attributes[3].name = EditorGUILayout.TextField("4 name: ", abillity?.attributes[3].name ?? "");
            abillity.attributes[3].modifier = EditorGUILayout.FloatField("4 modifier: ", abillity?.attributes[3].modifier ?? 0);
            abillity.attributes[3].type = (Abillity.Attribute.Type)EditorGUILayout.EnumPopup("4 type: ", abillity.attributes[3].type);

            GUILayout.Space(64);

            ab.abillities[i] = abillity;
        }
        if (GUILayout.Button("Add Abillity"))
        {
            ab.abillities.Add(new Abillity("", null, new Abillity.Attribute[] { new Abillity.Attribute("", 0, Abillity.Attribute.Type.Pro), new Abillity.Attribute("", 0, Abillity.Attribute.Type.Pro), new Abillity.Attribute("", 0, Abillity.Attribute.Type.Con), new Abillity.Attribute("", 0, Abillity.Attribute.Type.Con) }));
        }
        if (GUILayout.Button("Remove Abillity"))
        {
            ab.abillities.RemoveAt(numAbillities - 1);
        }
    }

}
