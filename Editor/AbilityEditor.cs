using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Abilities))]
public class AbilityEditor : Editor
{
    private SerializedProperty abilitiesProp;
    private ReorderableList list;

    private void OnEnable()
    {
        abilitiesProp = serializedObject.FindProperty("abillities");

        list = new ReorderableList(serializedObject, abilitiesProp, true, true, true, true);

        list.drawHeaderCallback = (Rect rect) =>
        {
            EditorGUI.LabelField(rect, "Abilities");
        };

        list.onAddCallback = (ReorderableList l) =>
        {
            int index = l.serializedProperty.arraySize;
            l.serializedProperty.arraySize++;
            SerializedProperty newElement = l.serializedProperty.GetArrayElementAtIndex(index);

            newElement.FindPropertyRelative("name").stringValue = "";
            newElement.FindPropertyRelative("image").objectReferenceValue = null;

            SerializedProperty attributes = newElement.FindPropertyRelative("attributes");
            attributes.arraySize = 4;

            for (int i = 0; i < 4; i++)
            {
                SerializedProperty att = attributes.GetArrayElementAtIndex(i);
                att.FindPropertyRelative("name").stringValue = "";
                att.FindPropertyRelative("modifier").floatValue = 0f;
                att.FindPropertyRelative("type").enumValueIndex = i < 2 ? 0 : 1;
            }

            serializedObject.ApplyModifiedProperties();
        };

        list.elementHeightCallback = (int index) =>
        {
            SerializedProperty element = abilitiesProp.GetArrayElementAtIndex(index);
            SerializedProperty attributes = element.FindPropertyRelative("attributes");
            float h = EditorGUIUtility.singleLineHeight * (4 + attributes.arraySize) + 20;
            return h;
        };

        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            SerializedProperty element = abilitiesProp.GetArrayElementAtIndex(index);

            SerializedProperty nameProp = element.FindPropertyRelative("name");
            SerializedProperty imageProp = element.FindPropertyRelative("image");
            SerializedProperty attributesProp = element.FindPropertyRelative("attributes");

            float y = rect.y + 2;
            float lh = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(new Rect(rect.x, y, rect.width, lh), nameProp);
            y += lh + 3;

            EditorGUI.PropertyField(new Rect(rect.x, y, rect.width, lh), imageProp);
            y += lh + 6;

            EditorGUI.LabelField(new Rect(rect.x, y, rect.width, lh), "Attributes:");
            y += lh + 3;

            for (int i = 0; i < attributesProp.arraySize; i++)
            {
                SerializedProperty att = attributesProp.GetArrayElementAtIndex(i);
                float col = rect.width / 3f;

                EditorGUI.PropertyField(new Rect(rect.x, y, col, lh), att.FindPropertyRelative("name"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(rect.x + col + 4, y, col, lh), att.FindPropertyRelative("modifier"), GUIContent.none);
                EditorGUI.PropertyField(new Rect(rect.x + col * 2 + 8, y, col - 8, lh), att.FindPropertyRelative("type"), GUIContent.none);

                y += lh + 3;
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        list.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
