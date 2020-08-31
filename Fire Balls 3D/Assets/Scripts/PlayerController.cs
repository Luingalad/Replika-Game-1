using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject ProjectilePrefab;

    public float SpeedOfProjectile;
    public float SpeedOfMovement;
    public float RotateSpeed;

    public Transform turretTransform;

    public bool isMoving = false;
    public bool isRotating = false;
    private bool isFired = false;

    private Vector3 moveSpeed;
    private Vector3 targetEulerRotation;

    void Start()
    {
        
    }

    private float currentTime = 0;
    private float FirePeriod = 0.5f; //sec
    private float movingTime;
    public bool isShootingEnable;

    void Update()
    {
        if(Input.GetAxis("Fire1") > 0.5f && !isFired && isShootingEnable)
        {
            FireBall();
        }
        
        if(isFired)
        {
            if(currentTime < FirePeriod )
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                isFired = false;
            }
        }

        if (isMoving)
        {
            transform.position += moveSpeed * Time.deltaTime;
            movingTime += Time.deltaTime;
        }

        if(isMoving && movingTime > 2f)
        {
            isMoving = false;
        }

        if (isRotating)
        {
            transform.Rotate( Vector3.up * -45 * Time.deltaTime);
            Debug.Log(targetEulerRotation.y + " " + transform.eulerAngles.y);
        }

        if(targetEulerRotation.y > transform.eulerAngles.y && isRotating)
        {
            Vector3 e = transform.eulerAngles;
            transform.eulerAngles = new Vector3(e.x, targetEulerRotation.y, e.z);
            isRotating = false;
        }
    }

    public void MoveNext(Transform target)
    {
        Debug.Log("Player moving");
        moveSpeed = (target.position - transform.position) / 2f;
        isMoving = true;
        movingTime = 0;
    }

    public void RotatePlayer(Transform eulerTarget)
    {
        targetEulerRotation = eulerTarget.eulerAngles;
        RotateSpeed = targetEulerRotation.y - transform.eulerAngles.y;
        isRotating = true;        
    }

    public void FireBall()
    {
        GameObject projectile = Instantiate(ProjectilePrefab);
        projectile.transform.position = turretTransform.position;

        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();

        float yAngle = transform.eulerAngles.y * Mathf.Deg2Rad;
        rigidbody.velocity = Vector3.right * SpeedOfProjectile * Mathf.Cos(yAngle) + Vector3.back * SpeedOfProjectile * Mathf.Sin(yAngle);

        isFired = true;
        Destroy(projectile, 3f);
    }

    
}
