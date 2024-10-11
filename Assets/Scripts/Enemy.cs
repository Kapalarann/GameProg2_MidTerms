using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public static readonly HashSet<Enemy> Entities = new HashSet<Enemy>();
    public Color[] colors;
    public int colorCode;

    public float speed;

    private Transform transform;
    public Transform playerTransform;

    void Awake()
    {
        Entities.Add(this);
        colorCode = Random.Range(0, colors.Length);
        gameObject.GetComponent<MeshRenderer>().material.color = colors[colorCode];

        transform = gameObject.GetComponent<Transform>();
        
    }

    public void Update()
    {
        foreach (var obj in PlayerMovement.Entities)
        {
            playerTransform = obj.transform;
        }

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        Entities.Remove(this);
    }
}
