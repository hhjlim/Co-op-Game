using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;


public class waddle_controll : NetworkBehaviour {

    [SerializeField] private CinemachineVirtualCamera vc;
    [SerializeField] private CinemachineConfiner virtualConfiner;
    private GameObject target;

    public override void OnNetworkSpawn()
    {
        target = GameObject.Find("Confiner");
        Collider2D collider = target.GetComponent<Collider2D>();
        if (IsOwner)
        {
            vc.Priority = 1;
            virtualConfiner.m_BoundingShape2D = collider;
        }
        else
        {
            vc.Priority = 0;
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        float speed = 10f;
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY);
        transform.position += moveDir * speed * Time.deltaTime;
    }
}
