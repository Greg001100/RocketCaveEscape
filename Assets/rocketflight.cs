﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocketflight : MonoBehaviour
{
    Rigidbody rigidbody;
    AudioSource enginesound;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        enginesound = GetComponent<AudioSource>();      
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        Thrust();
    }   

    private void Rotate()
    {
        rigidbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }

        rigidbody.freezeRotation = false;
    }

    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            if (enginesound.isPlaying == false)
            {
                enginesound.Play();
            }
        }

        else if (Input.GetKeyUp(KeyCode.Space))
        {
            enginesound.Stop();
        }
    }
}
