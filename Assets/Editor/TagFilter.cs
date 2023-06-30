using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class TagFilter : EditorWindow
{
    private List<string> tagFilters = new List<string>(); // La lista de etiquetas

    [MenuItem("Window/Object Filter")]
    public static void ShowWindow()
    {
        GetWindow<TagFilter>("Object Filter");
    }

    private void OnGUI()
    {
        GUILayout.Label("Object Filter", EditorStyles.boldLabel);

        // Botón para agregar una nueva etiqueta al filtro
        if (GUILayout.Button("Add Tag Filter"))
        {
            tagFilters.Add("");
        }

        // Mostrar los campos de texto para cada etiqueta en la lista
        for (int i = 0; i < tagFilters.Count; i++)
        {
            GUILayout.BeginHorizontal();

            tagFilters[i] = EditorGUILayout.TextField("Tag Filter " + (i + 1), tagFilters[i]);

            // Botón para eliminar la etiqueta de la lista
            if (GUILayout.Button("Remove", GUILayout.Width(80)))
            {
                tagFilters.RemoveAt(i);
                break;
            }

            GUILayout.EndHorizontal();
        }

        // Botón para filtrar los objetos en la escena
        if (GUILayout.Button("Filter Objects"))
        {
            FilterObjectsByTags(tagFilters);
        }

        // Botón para activar todos los objetos
        if (GUILayout.Button("Activate All Objects"))
        {
            ActivateAllObjects();
        }
    }

    private void FilterObjectsByTags(List<string> tags)
    {
        // Limpiar selección anterior
        Selection.activeGameObject = null;

        // Obtener todos los objetos en la escena, incluidos los desactivados
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);

        // Seleccionar los objetos que coinciden con las etiquetas
        foreach (GameObject obj in objects)
        {
            foreach (string tag in tags)
            {
                if (obj.CompareTag(tag))
                {
                    Selection.activeGameObject = obj;
                    break;
                }
            }
        }
    }

    private void ActivateAllObjects()
    {
        // Obtener todos los objetos en la escena, incluidos los desactivados
        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>(true);

        // Activar todos los objetos
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }

        // Marcar los cambios en la escena
        EditorUtility.SetDirty(Selection.activeTransform);

        // Forzar una actualización de la escena
        EditorApplication.QueuePlayerLoopUpdate();
    }
}