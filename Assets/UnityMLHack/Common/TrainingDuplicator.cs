using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TrainingDuplicator : MonoBehaviour
{
    #region Unity Inspector Variables
    [Tooltip("The agent prefab to generate instances of")]
    public GameObject AgentPrefab;

    [Tooltip("The brain to link to each agent scene.")]
    public Brain Brain;

    [Tooltip("The total number of columns to generate")]
    public int Columns = 3;

    [Tooltip("The spacing between generated columns")]
    public float ColumnSpacing = 5f;

    [Tooltip("The total number of rows to generate")]
    public int Rows = 3;

    [Tooltip("The spacing between generated rows")]
    public float RowSpacing = 5f;

    [Tooltip("The total number of stacks to generate")]
    public int Stacks = 3;

    [Tooltip("The spacing between generated stacks")]
    public float StackSpacing = 5f;
    #endregion // Unity Inspector Variables

    #region Internal Methods
    public void ClearInstances()
    {
        /*
        if (AgentPrefab == null) { return; }

        foreach (Transform childTransform in transform)
        {
            GameObject child = childTransform.gameObject;
            PrefabType type = PrefabUtility.GetPrefabType(child);

            if (type == PrefabType.PrefabInstance)
            {
                UnityEngine.Object childPrefab = PrefabUtility.GetPrefabParent(child);
                if (childPrefab == AgentPrefab)
                {
                    child.SafeDestroy();
                }
            }
        }
        */
        Debug.Log($"About to destroy {transform.childCount} object(s)");

        int count = 0;
        for (int i=transform.childCount-1; i >=0; i--)
        {
            count++;
            transform.GetChild(i).gameObject.SafeDestroy();
        }
        Debug.Log($"Destroyed {count} object(s)");
    }

    public void Regenerate()
    {
        // Clear all currently generated instaces
        ClearInstances();

        // If invalid configuration or brain, bail
        if ((AgentPrefab == null) || (Brain == null) || (Columns < 1) || (Stacks < 1)) { return; }

        // Generate new
        var count = 1;
        Vector3 Cursor = new Vector3();

        for (var y=0; y < Stacks; y++)
        {
            Cursor.y = y * StackSpacing; // No below, only grows in height

            for (var z=0; z < Rows; z++)
            {
                // Even or odd?
                if ((z > 0) && (z % 2 == 0))
                {
                    Cursor.z = -((z - 1) * RowSpacing);
                }
                else
                {
                    Cursor.z = z * RowSpacing;
                }


                for (var x=0; x < Columns; x++)
                {
                    // Even or odd?
                    if ((x > 0) && (x % 2 == 0))
                    {
                        Cursor.x = -((x - 1) * ColumnSpacing);
                    }
                    else
                    {
                        Cursor.x = x * ColumnSpacing;
                    }

                    var env = Instantiate(AgentPrefab, Cursor, Quaternion.identity);
                    env.transform.SetParent(transform, worldPositionStays: false);
                    env.name = "Environment" + count;
                    count++;

                    var agentScript = env.GetComponentInChildren<Agent>();
                    agentScript.GiveBrain(Brain);
                }
            }
        }
    }
    #endregion // Internal Methods

    // Use this for initialization
    private void OnEnable()
    {
        Regenerate();
    }

    private void Awake()
    {
    }
}