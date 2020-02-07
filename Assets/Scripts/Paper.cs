using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
class Paper : MonoBehaviour
{
    #pragma warning disable CS0649
    /* Render texture for the paper. */
    [SerializeField]
    private RenderTexture paperRT;

    /* Material for pencil strokes. */
    [SerializeField]
    private Material mat;

    /* Thickness of pencil stroke. */
    [SerializeField]
    private float strokeThickness;
    #pragma warning restore CS0649

    private Vector2 lastPos;
    private float xDim, yDim;

    private void Awake()
    {
        /* Clear the paper to white. */
        Graphics.SetRenderTarget(paperRT);
        GL.Clear(false, true, Color.white);

        /* Get collider bounds for the paper's aspect ratio. */
        BoxCollider collider = GetComponent<BoxCollider>();
        xDim = collider.bounds.size.x;
        yDim = collider.bounds.size.z;
    }

    public void ClearLast()
    {
        lastPos = Vector2.zero;
    }

    public void Impact(Vector3 pos, float amount)
    {
        Vector3 localPos = transform.InverseTransformPoint(pos);
        Vector2 uv = new Vector2(0.5f - localPos.x / xDim, localPos.y / yDim + 0.5f);
        Vector2 dir = uv - lastPos;
        amount *= strokeThickness;

        if (dir.magnitude > amount)
        {
            dir.Normalize();
            dir.y *= yDim / xDim;

            Vector2 perpendicular = new Vector2(-dir.y, dir.x);

            Graphics.SetRenderTarget(paperRT);
            GL.PushMatrix();
            mat.SetPass(0);
            GL.LoadOrtho();

            GL.Begin(GL.QUADS);
            GL.Vertex(uv - amount * dir);
            GL.TexCoord(new Vector3(0, 0, 0));

            GL.Vertex(uv + amount * perpendicular);
            GL.TexCoord(new Vector3(0, 1, 0));

            GL.Vertex(uv + amount * dir);
            GL.TexCoord(new Vector3(1, 1, 0));

            GL.Vertex(uv - amount * perpendicular);
            GL.TexCoord(new Vector3(1, 0, 0));
            GL.End();

            GL.PopMatrix();
            lastPos = uv;
        }
    }
}