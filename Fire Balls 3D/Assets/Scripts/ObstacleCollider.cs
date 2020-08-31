using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollider : MonoBehaviour
{
    public LevelController levelController;

    private Transform ball;
    private Vector3 velocity;
    private float yTop = 0.4f;
    private float t = 0.5f;
    private float t1 = 0;
    private bool isBallTB = false;


    private void Update()
    {
        if(isBallTB)
        {
            t1 += Time.deltaTime;
            Vector3 pos = ball.position;
            pos += velocity * Time.deltaTime;
            pos = new Vector3(pos.x, pos.y + yTop * Mathf.Cos(Mathf.PI * t1 / t), pos.z);

            ball.position = pos;            
        }

        if(t1 > t)
        {
            isBallTB = false;
            t1 = 0;
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ball = collision.transform;
            levelController.onLevelLostHandler();
            ThrowBackToPlayer();
        }
    }
    
    private void ThrowBackToPlayer()
    {
        Transform player = levelController.Player.transform;

        velocity = (player.position - ball.position) / t;

        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        isBallTB = true;

    }
}
