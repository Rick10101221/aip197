using UnityEngine;
using System.Collections.Generic;
using Valve.VR;

public class Student : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField]
    private GameObject pencil, pointer;

    /* How hard the trigger must be squeezed to start drawing. */
    [SerializeField]
    private float triggerThreshold = 0.1f;

    /* How much the pencil is tilted from the pointer. */
    [SerializeField]
    private float pencilSlant = 30f;
    #pragma warning restore CS0649

    /* Debouncer for trigger */
    private bool held;

    private void Awake()
    {
        Debug.Assert(pencil);
        Debug.Assert(pointer);
    }

    private void LateUpdate()
    {
        /* Parent pencil/pointer to right hand */
        pointer.transform.position = pencil.transform.position;
        pointer.transform.rotation = pencil.transform.rotation;

        MeshRenderer pencilClone = pencil.GetComponentInChildren<MeshRenderer>();

        if (pencilClone)
        {
            pencilClone.transform.localRotation = Quaternion.AngleAxis(180f - pencilSlant, Vector3.left);
        }

        /* Do drawing */
        float trigger = SteamVR_Actions._default.Squeeze.GetAxis(SteamVR_Input_Sources.RightHand);
        RaycastHit hit;

        if (trigger > triggerThreshold && Physics.Raycast(pointer.transform.position, pointer.transform.rotation * new Vector3(0, 0, 1), out hit))
        {
            Paper paper = hit.collider?.GetComponent<Paper>();

            if (!held)
            {
                paper?.ClearLast();
                held = true;
            }

            paper?.Impact(hit.point, trigger);
        }
        else
        {
            held = false;
        }
    }
}