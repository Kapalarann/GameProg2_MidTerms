using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static readonly HashSet<PlayerMovement> Entities = new HashSet<PlayerMovement>();
    private Transform transform;
    public float speed;

    public void Awake()
    {
        Entities.Add(this);
        transform = GetComponent<Transform>();
    }

    public void Update()
    {
        int right = 0, up = 0;
        if (Input.GetKey(KeyCode.A))
        {
            right -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            right += 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            up += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            up -= 1;
        }


        Vector3 direction = new Vector3(right, 0f, up);
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
