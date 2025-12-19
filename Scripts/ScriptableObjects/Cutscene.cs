using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Scriptable Objects/Cutscene")]
public class Cutscene : ScriptableObject
{
    public Sprite art;
    [TextArea(5, 10)]
    public string[] monologue;

    public RuntimeAnimatorController controller;
}
