using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    EdgeCollider2D edgeCollider;
    [SerializeField] PhysicsMaterial2D wallPhysicsMaterial;
    private void Start()
    {
        List<Vector2> points = new List<Vector2>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        float width = Camera.main.orthographicSize * Camera.main.aspect;
        float height = Camera.main.orthographicSize;
        points.Add(new Vector2(-width , -height));
        points.Add(new Vector2(-width , height));
        points.Add(new Vector2(width , height));
        points.Add(new Vector2(width , -height));
        edgeCollider.points = points.ToArray();
        edgeCollider.sharedMaterial = wallPhysicsMaterial;

        GameObject abyssgo = new GameObject("Abyss");
        EdgeCollider2D abyss = abyssgo.AddComponent<EdgeCollider2D>();
        points.Clear();
        points.Add(new Vector2(-width, -height));
        points.Add(new Vector2(width, -height));
        abyss.points = points.ToArray();
        abyss.isTrigger = true;
        abyssgo.tag = "Abyss";
    }
}
