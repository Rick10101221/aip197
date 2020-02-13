using System;
using UnityEngine;

class TestData : MonoBehaviour
{
    public class TestItem
    {
        #pragma warning disable CS0649
        [SerializeField]
        private GameObject mesh;

        public Vector3 Position;
        #pragma warning restore CS0649

        public Mesh Mesh
        {
            get
            {
                return mesh.GetComponent<MeshFilter>().sharedMesh;
            }
        }
    }

    [Serializable]
    public class Question : TestItem
    {
        #pragma warning disable CS0649
        public Answer[] Answers;
        #pragma warning restore CS0649
    }

    [Serializable]
    public class Answer : TestItem
    {
        #pragma warning disable CS0649
        public bool Correct;
        #pragma warning restore CS0649
    }

    #pragma warning disable CS0649
    [SerializeField]
    private Question[] test;

    [SerializeField]
    private Paper paper;
    #pragma warning restore CS0649

    public void Start()
    {
        /* Draw first question on start. */
        Debug.Assert(test.Length > 0);
        paper.DrawQuestion(test[0]);
    }
}