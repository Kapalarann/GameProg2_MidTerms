using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Turret : MonoBehaviour
{
    public float rotationSpeed, range;
    public GameObject target = null;

    public GameObject bullet, barrel;
    public Transform barrelEnd;
    public float attackSpeed;
    private float attackCooldown;

    private Material material;
    private int currentColor = 0;
    public Color[] colors;

    private Quaternion targetRotation;

    public GameObject gameOver;

    public void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        material.color = colors[currentColor];
        barrel.GetComponent<MeshRenderer>().material.color = colors[currentColor];
    }
    void Update()
    {
        if (Enemy.Entities.Count <= 0) return;

        float dist = float.PositiveInfinity;
        foreach (var obj in Enemy.Entities)
        {
            float d = Vector3.Distance(transform.position, obj.transform.position);
            if (d < dist)
            {
                target = obj.gameObject;
                dist = d;
            }
        }

        if (target)
        {
            targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
            if (targetRotation != transform.rotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        if(attackCooldown > 0) attackCooldown -= Time.deltaTime;
        else if(dist < range)
        {
            Vector3 pos = barrelEnd.transform.position;
            Quaternion rot = this.transform.rotation;

            GameObject instantiatedBullet = Instantiate(bullet, pos, rot);
            Bullet bulletScript = instantiatedBullet.GetComponent<Bullet>();
            bulletScript.color = colors[currentColor];
            bulletScript.colorCode = currentColor;

            attackCooldown += attackSpeed;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ChangeColor();
        }
    }

    public void ChangeColor()
    {
        currentColor++;
        if (currentColor >= colors.Length) currentColor = 0;
        material.color = colors[currentColor];
        barrel.GetComponent<MeshRenderer>().material.color = colors[currentColor];
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            gameOver.SetActive(true);
        }
    }
}
