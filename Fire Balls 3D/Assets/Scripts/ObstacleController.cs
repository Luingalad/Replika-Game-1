using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{    
    public bool isRising;

    private float riseSpeed;
    public float yTarget; //0 
    public float RiseTime;
    private float currentRiseTime = 0;

    public ObstacleRotator Rotator;
    
    void Start()
    {
        
    }

    void Update()
    {
        if(isRising)
        {
            Vector3 pos = transform.localPosition;
            float newY = pos.y + riseSpeed * Time.deltaTime;
            transform.localPosition = new Vector3(pos.x, newY, pos.z);

            currentRiseTime += Time.deltaTime;
        }

        if(currentRiseTime > RiseTime && isRising)
        {
            currentRiseTime = 0;
            isRising = false;
            Debug.Log("obs not rising");

        }
    }

    public void RiseTheObstacle(float y)
    {
        yTarget = y;
        riseSpeed = (yTarget - transform.localPosition.y) / 1f;
        isRising = true;
        Debug.Log("obs rising");
    }

}
