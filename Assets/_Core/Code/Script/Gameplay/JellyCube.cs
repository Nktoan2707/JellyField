using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyField
{
    using UnityEngine;

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    //[RequireComponent(typeof(Rigidbody))]
    public class JellyCube : MonoBehaviour
    {
        public float springForce = 10f;
        public float damping = 5f;
        public float deformationSpeed = 2f;

        private MeshFilter meshFilter;
        private Mesh originalMesh;
        private Mesh deformedMesh;
        private Vector3[] originalVertices;
        private Vector3[] displacedVertices;
        private Vector3[] vertexVelocities;

        void Start()
        {
            // Initialize the Mesh
            meshFilter = GetComponent<MeshFilter>();
            originalMesh = meshFilter.mesh;
            deformedMesh = Instantiate(originalMesh);
            meshFilter.mesh = deformedMesh;

            originalVertices = originalMesh.vertices;
            displacedVertices = new Vector3[originalVertices.Length];
            vertexVelocities = new Vector3[originalVertices.Length];

            // Copy the original vertices to start with
            for (int i = 0; i < originalVertices.Length; i++)
            {
                displacedVertices[i] = originalVertices[i];
            }
        }

        void Update()
        {
            // Apply Jelly Effect by deforming the mesh
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                UpdateVertex(i);
            }

            // Update the mesh with the new vertices
            deformedMesh.vertices = displacedVertices;
            deformedMesh.RecalculateNormals();
        }

        private void UpdateVertex(int index)
        {
            // Calculate spring back to the original position
            Vector3 velocity = vertexVelocities[index];
            Vector3 displacement = displacedVertices[index] - originalVertices[index];

            // Hooke's Law for spring simulation
            Vector3 springForceVector = -displacement * springForce;
            Vector3 dampingForce = -velocity * damping;
            Vector3 force = springForceVector + dampingForce;

            // Update velocity and position
            vertexVelocities[index] += force * Time.deltaTime;
            displacedVertices[index] += vertexVelocities[index] * Time.deltaTime;

            // Optional damping for more natural look
            vertexVelocities[index] *= 1f - (deformationSpeed * Time.deltaTime);
        }
    }

}
