using System;
using UnityEngine;





public class ScaleObject : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    [SerializeField] private float maxScale = 3f;
    [SerializeField] private float minScale = 0.5f;

    private void Update()
    {


        ObjectScale();
    }

    private void ObjectScale()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (transform.localScale.x >= maxScale) return;
            transform.localScale += Vector3.one * (Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            if (transform.localScale.x <= minScale) return;
            transform.localScale -= Vector3.one * (Time.deltaTime * speed);
        }
    }
}