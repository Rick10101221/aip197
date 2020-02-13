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
    private Material pencilMat;

    /* Material for questions and answers. */
    [SerializeField]
    private Material contentMat;

    /* Thickness of pencil stroke. */
    [SerializeField]
    private float strokeThickness;
    #pragma warning restore CS0649

    private Vector2 lastPos;
    private float xDim, yDim;

    private TestData.Answer[] currentAnswers;
    private float[] revolutionSigns;
    private int[] revolutionCounts;

    private void Awake()
    {
        /* Clear the paper to white. */
        Graphics.SetRenderTarget(paperRT);
        GL.Clear(true, true, Color.white);

        /* Get collider bounds for the paper's aspect ratio. */
        BoxCollider collider = GetComponent<BoxCollider>();
        xDim = collider.bounds.size.x;
        yDim = collider.bounds.size.z;
    }

    public void DrawQuestion(TestData.Question question)
    {
        /* Get answer positions. */
        currentAnswers = question.Answers;
        revolutionSigns = new float[currentAnswers.Length];
        revolutionCounts = new int[currentAnswers.Length];

        /* Ortho is from [0, 1] not [-1, 1] stop forgetting stop forgetting */
        Graphics.SetRenderTarget(paperRT);
        GL.PushMatrix();
        contentMat.SetPass(0);
        GL.LoadOrtho();

        Graphics.DrawMeshNow(question.Mesh, question.Position, Quaternion.identity);

        foreach (TestData.Answer answer in currentAnswers) {
            Graphics.DrawMeshNow(answer.Mesh, answer.Position, Quaternion.identity);
        }

        GL.PopMatrix();
    }

    public void ClearLast()
    {
        lastPos = Vector2.zero;
    }

    public void Impact(Vector3 pos, float amount)
    {
        /* Get position on paper and remap to [0-1]. */
        Vector3 localPos = transform.InverseTransformPoint(pos);
        Vector2 uv = new Vector2(0.5f - localPos.x / xDim, localPos.y / yDim + 0.5f);

        Vector2 dir = uv - lastPos;
        amount *= strokeThickness;

        /* Has the pointer moved farther than the actual size of the pencil stroke? */
        if (dir.magnitude > amount)
        {
            dir.Normalize();
            dir.y *= yDim / xDim;    // squash the stroke a bit

            Vector2 perpendicular = new Vector2(-dir.y, dir.x);

            /* Ortho is from [0, 1] not [-1, 1] stop forgetting stop forgetting */
            Graphics.SetRenderTarget(paperRT);
            GL.PushMatrix();
            pencilMat.SetPass(0);
            GL.LoadOrtho();

            /* Draw pencil stroke */
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

            /* Check for a circled answer. */
            for (int i = 0; i < currentAnswers.Length; i++) {
                float angle = Mathf.Sign(Mathf.Atan2(uv.y - currentAnswers[i].Position.y, uv.x - currentAnswers[i].Position.x));

                if (angle != revolutionSigns[i]) {
                    revolutionSigns[i] = angle;
                    revolutionCounts[i]++;

                    if (revolutionCounts[i] > 2) {
                        /* Answer circled logic would go here */
                        GL.Clear(true, true, currentAnswers[i].Correct ? Color.green : Color.red);
                        break;
                    }
                }
            }

            lastPos = uv;
        }
    }
}