using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObject : MonoBehaviour
{
    [SerializeField] Vector3 movePosition;
    [SerializeField] float moveSpead;
    [SerializeField][Range(0, 1)] float moveProgress;
    Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        moveProgress = Mathf.PingPong(moveSpead*Time.time, 1);
        Vector3 offset = movePosition * moveProgress; //сдвиг
        transform.position = startPosition + offset;
    }
}
