using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Student : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField]
    private GameObject pencil, paper;

    /* How hard the pencil can hit the test to automatically leave behind a mark */
    [SerializeField]
    private float impactThreshold = 0.5f;

    /* How much the pencil is tilted up from the pointer. */
    [SerializeField]
    private float pencilSlant = 30f;
    #pragma warning restore CS0649

    private LineRenderer pointer;

    private void Awake()
    {
        pointer = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {
        pointer.transform.position = pencil.transform.position;
        pointer.transform.rotation = pencil.transform.rotation;
        pencil.transform.rotation *= Quaternion.AngleAxis(pencilSlant, Vector3.left);
    }
}