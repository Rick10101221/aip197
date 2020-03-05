using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR;

public class Student : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField]
    private GameObject pencil, pointer;

    /* How hard the trigger must be squeezed to start drawing. */
    [SerializeField]
    private float triggerThreshold = 0.1f;

    /* Dot at the end of Pointer */
    [SerializeField]
    private GameObject m_Dot;

    /* How much the pencil is tilted from the pointer. */
    [SerializeField]
    private float pencilSlant = 30f;
#pragma warning restore CS0649

    /* Debouncer for trigger */
    private bool held;

    private LineRenderer pointerLine;

    private void Awake()
    {
        Debug.Assert(pencil);
        Debug.Assert(pointer);

        pointerLine = pointer.GetComponent<LineRenderer>();
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

        RaycastHit hit;
        if (!Physics.Raycast(pointer.transform.position, pointer.transform.rotation * new Vector3(0, 0, 1), out hit))
        {
            /* Skip if we didn't hit anything (shouldn't really ever happen). */
            return;
        }

        /* Shrink pointer not to extend beyond the position we hit. */
        pointerLine.SetPosition(pointerLine.positionCount - 1, new Vector3(0, 0, hit.distance));
		m_Dot.transform.position = hit.point;

        /* Check for input */
        float trigger = SteamVR_Actions._default.Squeeze.GetAxis(SteamVR_Input_Sources.RightHand);
        if (trigger > triggerThreshold)
        {
            if (!held && hit.collider != null)
            {
                held = true;

                /* Hit a button? */
                Button button = hit.collider.GetComponent<Button>();

				if (button)
				{
					ExecuteEvents.Execute(button.gameObject,
										  new BaseEventData(EventSystem.current),
										  ExecuteEvents.submitHandler);
				}
            }
        }
        else
        {
            held = false;
        }
    }
}