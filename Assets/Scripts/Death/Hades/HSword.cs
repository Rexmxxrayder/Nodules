using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HSword : MonoBehaviour {
    HPhysics hPhysics;
    MeshFilter m;

    private void Start() {
        hPhysics = GetComponentInParent<HPhysics>();
        hPhysics.Forces.Add("Sword", Vector3.zero);
        m = GetComponent<MeshFilter>();
        MakeHalfCircleColldier(Vector3.right, 5, 20);
    }



    void MakeHalfCircleColldier(Vector3 direction, float large, int point) {
        Mesh mesh = new();
        List<Vector3> vertices = new();
        List<int> triangles = new();
        Vector3 center;
        center = transform.position + Vector3.up * 2;
        vertices.Add(center);
        vertices.Add(center - Vector3.up);
        for (int i = 0; i < point + 1; i++) {
            Vector3 first = center + new Vector3(Mathf.Cos(Mathf.PI / point * i), 0, Mathf.Sin(Mathf.PI / point * i)).normalized * large;
            vertices.Add(first);
            vertices.Add(first - Vector3.up);
            triangles.Add(i * 2);
            triangles.Add(i * 2 + 2);
            triangles.Add(i * 2 + 1);
            triangles.Add(i * 2 + 1);
            triangles.Add(i * 2 + 2);
            triangles.Add(i * 2 + 3);
        }
        triangles.Add(point * 2 + 2);
        triangles.Add(0);
        triangles.Add(point * 2 + 3);
        triangles.Add(point * 2 + 3);
        triangles.Add(0);
        triangles.Add(1);

        for (int i = 0; i < point + 1; i++) {
            triangles.Add(0);
            triangles.Add(i * 2 + 2);
            triangles.Add(i * 2);
            triangles.Add(1);
            triangles.Add(i * 2 + 1);
            triangles.Add(i * 2 + 3);
        }


        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        m.mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("enter");
    }
}
