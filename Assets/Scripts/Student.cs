using UnityEngine;
using System.Collections.Generic;

public class Student : MonoBehaviour
{
    #pragma warning disable CS0649
    [SerializeField]
    private GameObject pencil, paper;

    /* Mouse sensitivity on each axis. */
    [SerializeField]
    private Vector2 sensitivity = new Vector2(90f, 90f);

    /* How many frames to average mouse movement over. */
    [SerializeField]
    private int windowSize = 20;

    /* Pencil min/max bounds, movement slow */
    [SerializeField]
    private float armLength = 0.6f, pencilSpeed = 25f, pencilNear = 0.1f;

    /* How hard the pencil can hit the test to automatically leave behind a mark */
    [SerializeField]
    private float impactThreshold = 0.5f;
    #pragma warning restore CS0649

    private Queue<Vector2> movementHistory;    // previous mouse movements
    private Vector2 average;    // averaged motion vector
    private float yaw, pitch;    // camera rotation
    private pencilDist, pencilLat;    // pencil position

    private void Awake()
    {
        movementHistory = new Queue<Vector2>(windowSize);
        Cursor.lockState = CursorLockMode.Locked;

        /* Get starting position/attitude. */
        pitch = transform.localEulerAngles.x;
        yaw = transform.localEulerAngles.y;

        pencilLat = pencil.transform.localPosition.x;
        pencilDist = pencil.transform.localPosition.z;
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        movement = Vector2.Scale(movement, sensitivity * Time.deltaTime);
        average += movement / windowSize;

        /* Discard old movement vectors (sliding window). */
        movementHistory.Enqueue(movement);
        if (movementHistory.Count >= windowSize)
        {
            average -= movementHistory.Dequeue() / windowSize;
        }

        /* Hold down shift = move pencil */
        if (Input.GetKey("left shift"))
        {
            ApplyToPencil(average);
        }
        else
        {
            ApplyToCamera(average);
        }
    }

    private void ApplyToCamera(Vector2 movement)
    {
        yaw += movement.x;
        pitch = Mathf.Clamp(pitch - movement.y, -90f, 90f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void ApplyToPencil(Vector2 movement)
    {
        /* Move pencil */
        movement /= pencilSpeed;
        pencilLat = Mathf.Clamp(pencilLat + movement.x, -armLength / 2, armLength / 2);
        pencilDist = Mathf.Clamp(pencilDist + movement.y, pencilNear, armLength);

        pencil.transform.localPosition = new Vector3(pencilLat, 0.0f, pencilDist);
        pencil.transform.position = new Vector3(pencil.transform.position.x, paper.transform.position.y, pencil.transform.position.z);

        float impactSpeed = movement.magnitude;

        if (Input.GetMouseButton(0) || impactSpeed > impactThreshold)
        {
            /* Draw on paper */
            paper.GetComponent<Paper>().Impact(pencil.transform.position, impactSpeed);
        }
    }
}