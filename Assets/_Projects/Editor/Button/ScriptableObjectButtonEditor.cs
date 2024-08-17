using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ScriptableObject), true)]
[CanEditMultipleObjects]
public class ScriptableObjectButtonEditor : BaseButtonEditor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawButtons(target);
    }
}
