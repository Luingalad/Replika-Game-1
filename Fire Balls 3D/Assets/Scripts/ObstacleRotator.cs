using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotator : MonoBehaviour
{
    /// <summary>
    /// x => angle for turn, y => time for turn
    /// </summary>
    public List<Vector2> Pattern;    
    private bool isRotating = false;

    private bool isRunning = false;
    private float time;
    private float angleSpeed;
    private float currentTime;

    private void Update()
    {
        if(isRunning)
        {
            transform.eulerAngles += new Vector3(0, angleSpeed * Time.deltaTime, 0);
            currentTime += Time.deltaTime;
        }

        if(currentTime > time )
        {
            isRunning = false;
        }
    }

    public void StartRotating()
    {        
        isRotating = true;
        StartCoroutine(Rotating());
    }

    public void StopRotating()
    {
        isRotating = false;
        isRunning = false;
    }

    private IEnumerator Rotating()
    {
        while (isRotating)
        {
            for(int i = 0; i < Pattern.Count; i++)
            {
                time = Pattern[i].y;
                angleSpeed = Pattern[i].x / time;
                currentTime = 0;
                isRunning = true;
                while(isRunning)
                {
                    yield return new WaitForEndOfFrame();
                }

                if (!isRotating)
                    break;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("GG");
        }
    }
}
