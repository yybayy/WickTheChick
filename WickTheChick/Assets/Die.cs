using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    public Transform player;
    private Vector3 diePoint = new Vector3(0, -20, 0);
    private Vector3 spawnPoint;

    private void Awake()
    {
        spawnPoint = player.transform.position;
    }

    private void Update()
    {
        if (player.position.y <= diePoint.y )
        {
            player.transform.position = spawnPoint;
        }
    }
}
