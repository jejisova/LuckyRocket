using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UIElements;


public class RandomPosition : MonoBehaviour
{

    [SerializeField] Vector3 maxMovePosition;
    [SerializeField] Vector3 maxMovePosition_teleport;
    [SerializeField][Range(0, 1)] float moveProgress;
    [SerializeField] Vector3 startPosition;
    [SerializeField] float moveSpead;
    bool switcher1 = false;
    Vector3 offsetTeleporting;
    bool switcher = false;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Moveing();
    }

    async void Moveing()
    {
        if (Input.GetKey(KeyCode.Space) == true)
        {
            for (int i = 0; i < 1; i++)
            {
                Invoke("Swticher", 1f);
                startPosition = new Vector3(-5, 10, -2);
            }

        }

        if (transform.position.z >= 2)
        {
            System.Random rdm = new System.Random();
            offsetTeleporting.x = maxMovePosition_teleport.x * rdm.Next(1, 100) / 100;
            offsetTeleporting.y = maxMovePosition_teleport.y * rdm.Next(1, 100) / 100;
            offsetTeleporting.z = 0;
        }

        if (switcher == true)
        {
            moveProgress = Mathf.PingPong(Time.time, 1);
            Vector3 offset = moveProgress * maxMovePosition;
            transform.position = startPosition + offset + offsetTeleporting;
        }
    }

    void Swticher()
    {
        switcher = true;
    }
}
