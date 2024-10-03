using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex
{
    public int id;
    public Vector3 position;
    public Vector3 velocity, force;

    public JellyVertex(int _id, Vector3 _pos)
    {
        id = _id;
        position = _pos;
    }

    public void Shake(Vector3 target, float m, float s, float d)
    {
        force = (target - position) * s; // Tính lực tác dụng
        velocity = (velocity + force / m) * d; // Cập nhật vận tốc
        position += velocity; // Cập nhật vị trí

        // Hiệu ứng dao động liên tục
        float oscillation = Mathf.Sin(Time.time * 5f) * 0.2f; // Tăng cường độ dao động
        position += new Vector3(oscillation, oscillation, 0); // Tạo hiệu ứng dao động qua lại theo chiều X

        // Điều kiện để giữ vị trí
        if ((velocity + force + force / m).magnitude < 0.001f)
        {
            position = target;
        }
    }
}

public class JellyMesh : MonoBehaviour
{
    public float intensity = 1f; // Cường độ dao động
    public float mass = 1f; // Khối lượng
    public float stiffness = 1f; // Độ cứng
    public float damping = 0.75f; // Độ giảm chấn

    private Mesh originalMesh, MeshClone;
    private JellyVertex[] jv; // Mảng các đỉnh jelly
    private Vector3[] vertexArray; // Mảng các đỉnh của mesh

    private MeshRenderer meshRenderer;

    private void Start()
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh; // Lấy mesh gốc
        MeshClone = Instantiate(originalMesh); // Tạo bản sao của mesh
        GetComponent<MeshFilter>().sharedMesh = MeshClone; // Gán bản sao cho MeshFilter
        meshRenderer = GetComponent<MeshRenderer>();

        jv = new JellyVertex[MeshClone.vertices.Length];
        for (int i = 0; i < MeshClone.vertices.Length; i++)
        {
            jv[i] = new JellyVertex(i, transform.TransformPoint(MeshClone.vertices[i])); // Khởi tạo các đỉnh jelly
        }
    }

    private void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;

        // Kiểm tra bounds hợp lệ
        if (!float.IsNaN(GetComponent<Renderer>().bounds.min.x) && !float.IsInfinity(GetComponent<Renderer>().bounds.min.x) &&
            !float.IsNaN(GetComponent<Renderer>().bounds.max.x) && !float.IsInfinity(GetComponent<Renderer>().bounds.max.x))
        {
            for (int i = 0; i < jv.Length; i++)
            {
                Vector3 target = transform.TransformPoint(vertexArray[jv[i].id]);
                float intensity = (1 - (GetComponent<Renderer>().bounds.max.y) / GetComponent<Renderer>().bounds.size.y) * this.intensity; // Tính cường độ
                jv[i].Shake(target, mass, stiffness, damping); // Gọi hàm Shake để điều chỉnh vị trí
                target = transform.InverseTransformPoint(jv[i].position);
                vertexArray[jv[i].id] = Vector3.Lerp(vertexArray[jv[i].id], target, intensity); // Cập nhật vị trí
            }
        }
        else
        {
            Debug.LogWarning("Invalid bounds for the mesh!");
        }

        MeshClone.vertices = vertexArray; // Cập nhật vertices của mesh
        MeshClone.RecalculateBounds(); // Tính lại kích thước của mesh
        MeshClone.RecalculateNormals(); // Tính lại pháp tuyến của mesh
    }
}
