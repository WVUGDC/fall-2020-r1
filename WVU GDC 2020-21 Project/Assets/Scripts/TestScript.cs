using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Code put here will happen once
    }

    // Update is called once per frame
    void Update()
    {
        //Code put here will happen every frame
        transform.position = new Vector2(Mathf.Cos(Time.time), Mathf.Abs(Mathf.Sin(Time.time * 5)) * 2);
    }
}
