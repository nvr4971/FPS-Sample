using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    [Header("Sight Mesh")]
    [SerializeField] private float sightDistance;
    [SerializeField] private float sightAngle;
    [SerializeField] private float sightHeight;

    private Color sightMeshColor = Color.red;
    private Mesh sightMesh;

    [Header("Detection")]
    [SerializeField] private int scanFrequency;
    [SerializeField] private LayerMask layers;
    [SerializeField] private LayerMask occlusionLayers;
    private readonly Collider[] colliders = new Collider[50];

    private int count;
    private float scanInterval;
    private float scanTimer;

    [SerializeField] private List<GameObject> objects = new();

    private void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    private void Update()
    {
        scanTimer -= Time.deltaTime;

        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    private void OnValidate()
    {
        sightMesh = CreateSightMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    public bool IsObjectInSight()
    {
        return objects.Count != 0;
    }

    public Transform GetEnemy()
    {
        return objects[0].transform;
    }

    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(transform.position, sightDistance, colliders, layers, QueryTriggerInteraction.Collide);
        objects.Clear();

        for (int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;

            if (IsInSight(obj))
            {
                objects.Add(obj);
            }
        }
    }

    private bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        if (direction.y < 0 || direction.y > sightHeight)
        {
            return false;
        }

        direction.y = 0;
        float deltaAngle = Vector3.Angle(direction, transform.forward);

        if (deltaAngle > sightAngle)
        {
            return false;
        }

        origin.y += sightHeight / 2;
        dest.y = origin.y;

        if (Physics.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }

        return true;
    }

    private Mesh CreateSightMesh()
    {
        Mesh mesh = new();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -sightAngle, 0) * Vector3.forward * sightDistance;
        Vector3 bottomRight = Quaternion.Euler(0, sightAngle, 0) * Vector3.forward * sightDistance;

        Vector3 topCenter = bottomCenter + Vector3.up * sightHeight;
        Vector3 topRight = bottomRight + Vector3.up * sightHeight;
        Vector3 topLeft = bottomLeft + Vector3.up * sightHeight;

        int vert = 0;

        // left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -sightAngle;
        float deltaAngle = (sightAngle * 2) / segments;

        for (int i = 0; i < segments; ++i)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * sightDistance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * sightDistance;

            topRight = bottomRight + Vector3.up * sightHeight;
            topLeft = bottomLeft + Vector3.up * sightHeight;

            // far side
            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            // top
            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            // bottom
            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnDrawGizmos()
    {
        if (sightMesh)
        {
            Gizmos.color = sightMeshColor;
            Gizmos.DrawMesh(sightMesh, transform.position, transform.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, sightDistance);

        for (int i = 0; i < count; ++i)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 1f);
        }

        Gizmos.color = Color.green;

        foreach (var obj in objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 1f);
        }
    }
}
