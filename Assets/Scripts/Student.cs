using UnityEngine;
using System.Collections.Generic;

public class Student : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField]
    private GameObject paper, pencil, pointer;

    /* How hard the pencil can hit the test to automatically leave behind a mark */
    [SerializeField]
    private float impactThreshold = 0.5f;

    /* How much the pencil is tilted from the pointer. */
    [SerializeField]
    private float pencilSlant = 120f;
    #pragma warning restore CS0649

    private void LateUpdate()
    {
        pointer.transform.position = pencil.transform.position;
        pointer.transform.rotation = pencil.transform.rotation;

        pencil.GetComponentInChildren<MeshRenderer>().transform.rotation *= Quaternion.AngleAxis(pencilSlant, Vector3.left);
    }
}