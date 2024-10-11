using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, range;
    private float distanceTravelled = 0;
    public Material material;
    public Color color;
    public int colorCode = 0; 

    private SphereCollider collider;

    public void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = color;

        collider = GetComponent<SphereCollider>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        distanceTravelled += speed * Time.deltaTime;

        if(distanceTravelled>range)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            if(collision.gameObject.GetComponent<Enemy>().colorCode == colorCode)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
