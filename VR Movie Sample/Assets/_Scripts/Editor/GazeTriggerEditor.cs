using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GazeTrigger))]
public class GazeTriggerEditor : Editor
{
    private GazeTrigger gazeTrigger;

    private void OnEnable()
    {
        gazeTrigger = target as GazeTrigger;
        gazeTrigger.sphere = GameObject.Find("Sphere");
        gazeTrigger.sphereRadius = gazeTrigger.sphere.GetComponent<MeshFilter>().mesh.bounds.size.x / 2.0f * gazeTrigger.sphere.transform.localScale.x;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (gazeTrigger.sphere.transform.hasChanged)
        {
            gazeTrigger.sphereRadius = gazeTrigger.sphere.GetComponent<MeshFilter>().mesh.bounds.size.x / 2.0f * gazeTrigger.sphere.transform.localScale.x;
        }

        if (gazeTrigger.transform.hasChanged)
        {
            if (!gazeTrigger.IsOnSphere())
            {
                gazeTrigger.SetOnSphere();
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

}
