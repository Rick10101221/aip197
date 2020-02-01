using UnityEngine;

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

    /* Paper aspect ratio. */
    [SerializeField]
    private float xDim = 0.2159f, yDim = 0.2794f;
    #pragma warning restore CS0649

    private Vector2 lastPos;

    private void Awake()
    {
        Graphics.SetRenderTarget(paperRT);
        GL.Clear(false, true, Color.white);
    }

    public void Impact(Vector3 pos, float impactSpeed)
    {
        /*Vector3 localPos = transform.InverseTransformPoint(pos);
        Vector2 uv = new Vector2(localPos.x / xDim + 0.5f, localPos.y / yDim + 0.5f);

        Vector2 dir = uv - lastPos;
        dir.Normalize();
        Vector2 perpendicular = new Vector2(-dir.y, dir.x);

        Debug.LogWarning(uv);

        Graphics.SetRenderTarget(paperRT);
        GL.PushMatrix();
        mat.SetPass(0);
        GL.LoadOrtho();

        GL.Begin(GL.QUADS);
        GL.Vertex(uv - (strokeThickness + impactSpeed) * dir);
        GL.Vertex(uv - (strokeThickness + impactSpeed) * perpendicular);
        GL.Vertex(uv + (strokeThickness + impactSpeed) * dir);
        GL.Vertex(uv + (strokeThickness + impactSpeed) * perpendicular);
        GL.End();

        GL.PopMatrix();

        lastPos = uv;*/
    }
}