using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(FireTower))]
public class HasLauncher : Editor
{
    public override void OnInspectorGUI()
    {
        FireTower tower = (FireTower)target;

        DrawDefaultInspector();

        if (tower.IsHasLauncher)
        {
            EditorGUILayout.LabelField("Additional Settings", EditorStyles.boldLabel);
            tower._launcher = (Transform)EditorGUILayout.ObjectField(
                "Launcher",
                tower._launcher,
                typeof(Transform),
                true);
        }

        // Обновляем объект, если были изменения
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif