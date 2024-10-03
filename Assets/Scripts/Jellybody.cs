using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Softbodies
{
    public struct Vector3WithID
    {
        public int id;
        public Vector3 vertex;

        public Vector3WithID(int id, Vector3 vertex)
        {
            this.id = id;
            this.vertex = vertex;
        }
    }

    [RequireComponent(typeof(MeshFilter))]
    public class Jellybody : MonoBehaviour
    {
        private Mesh _originalMesh;
        private Dictionary<int, int[]> _jellyVertexToMeshVertex;
        private Vector3[] _deformedVertices;

        private int _distinctVCount;
        private int _vCount;

        [Tooltip("Amplitude of the jelly effect.")]
        [SerializeField]
        private float _amplitude = 0.05f;
        [Tooltip("Frequency of the jelly oscillation.")]
        [SerializeField]
        private float _frequency = 2f;

        private Vector3[] _originalVertices; // Store the original positions of the vertices

        void Start()
        {
            InitData();
            MapMeshVerticesToJellyVertices();
        }

        private void InitData()
        {
            _originalMesh = GetComponent<MeshFilter>().mesh;

            _vCount = _originalMesh.vertices.Length;

            _deformedVertices = new Vector3[_vCount];

            _originalVertices = _originalMesh.vertices.Clone() as Vector3[];
        }

        private void MapMeshVerticesToJellyVertices()
        {
            _jellyVertexToMeshVertex = new Dictionary<int, int[]>();
            List<Vector3WithID> vids = new List<Vector3WithID>();

            for (int i = 0; i < _vCount; i++)
            {
                vids.Add(new Vector3WithID(i, _originalMesh.vertices[i]));
            }

            var groups = vids.GroupBy(v => v.vertex);

            int k = 0;
            foreach (var v in groups)
            {
                int[] verts = new int[v.Count()];
                int j = 0;
                foreach (var w in v)
                {
                    verts[j] = w.id;
                    j++;
                }

                _jellyVertexToMeshVertex.Add(k, verts);
                k++;
            }

            _distinctVCount = k;
        }

        void FixedUpdate()
        {
            ApplyJellyEffect();
            UpdateJellybody();
        }

        private void ApplyJellyEffect()
        {
            // Apply a sinusoidal oscillation to simulate the jelly effect
            for (int i = 0; i < _distinctVCount; i++)
            {
                foreach (int j in _jellyVertexToMeshVertex.ElementAt(i).Value)
                {
                    // Calculate the new position with sinusoidal oscillation
                    Vector3 originalPosition = _originalVertices[j];
                    Vector3 offset = Vector3.up * Mathf.Sin(Time.time * _frequency + j) * _amplitude;
                    _deformedVertices[j] = originalPosition + offset;
                }
            }
        }

        private void UpdateJellybody()
        {
            _originalMesh.vertices = _deformedVertices;
            _originalMesh.RecalculateNormals();
        }
    }
}
