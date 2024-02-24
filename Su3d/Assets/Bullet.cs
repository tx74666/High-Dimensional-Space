using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        GameObject.Destroy(gameObject, 3f);
    }

    void Update()
    {
        transform.position += 40 * Time.deltaTime * transform.forward;
    }
}
