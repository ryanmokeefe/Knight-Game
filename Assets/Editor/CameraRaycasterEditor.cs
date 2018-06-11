using UnityEditor;


// TODO consider changing to a property drawer
[CustomEditor(typeof(CameraRaycaster))]
public class CameraRaycasterEditor : Editor
{
    bool isLayerPrioritiesUnfolded = true; // store the UI state

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Serialize cameraRaycaster instance

        isLayerPrioritiesUnfolded = EditorGUILayout.Foldout(isLayerPrioritiesUnfolded, "Layer Priorities");
        if (isLayerPrioritiesUnfolded)
        {
            EditorGUI.indentLevel++;
            {
                BindArraySize();
                BindArrayElements();
                // PrintString();
            }
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties(); // De-serialize back to cameraRaycaster (and create undo point)
    }

    void BindArraySize()
    {
        // find Size of serialized 'layerPriorities', type Array, and assign it to new var
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        // make var requiredArraySize for starting display, use EditorGUILayout to create int field, give params: "title to display", varValueToShow
        int requiredArraySize = EditorGUILayout.IntField("Size", currentArraySize);
        // if changed:
        if (requiredArraySize != currentArraySize)
        {
            // make new currentValue the requiredValue and set the number in memory only
            serializedObject.FindProperty("layerPriorities.Array.size").intValue = requiredArraySize;
        }
    }

    void BindArrayElements()
    {
        int currentArraySize = serializedObject.FindProperty("layerPriorities.Array.size").intValue;
        for (int i = 0; i < currentArraySize; i++)
        {
            // find each int in array
            var prop = serializedObject.FindProperty(string.Format("layerPriorities.Array.data[{0}]", i));
            // use EditorGUI to create LayerField for each {}, i
            prop.intValue = EditorGUILayout.LayerField(string.Format("Layer {0}:", i), prop.intValue);
        }
    }


// TEST:
    // void PrintString() 
    // {
    //     // find serialized 'stringToPrint' field and assign it to new var
    //     var currentText = serializedObject.FindProperty("stringToPrint");
    //     // assign var to the Editor GUI, make it a text field, give params: "title to display", varValueToShow
    //     currentText.stringValue = EditorGUILayout.TextField("String to print: ", currentText.stringValue);
    // }
    
}
