using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Subsystems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Addressables;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : MonoBehaviour
{
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        SetUnactive(other);
    }

    private void SetUnactive(Collider other)
    {
        if (other.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
