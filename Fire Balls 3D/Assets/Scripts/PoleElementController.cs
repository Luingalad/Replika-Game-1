using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleElementController : MonoBehaviour
{
    public delegate void HitHandler(PoleElementController poleElement);
    public HitHandler onHitHandler;

    public bool isBoss;
    public int BossPower;

    public MeshCollider meshCollider;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Ball"))
        {
            if (isBoss)
            {
                BossPower--;

                if(BossPower <= 0)
                {
                    GetComponent<MeshCollider>().enabled = false;
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
            else
            {
                GetComponent<MeshCollider>().enabled = false;
                GetComponent<MeshRenderer>().enabled = false;
            }

            Destroy(collision.gameObject);

            if(onHitHandler != null)
            {
                onHitHandler.Invoke(this);
            }
        }
    }

}
