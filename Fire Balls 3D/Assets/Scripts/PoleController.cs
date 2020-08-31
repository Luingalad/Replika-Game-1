using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PoleController : MonoBehaviour
{
    public List<PoleElementController> PoleElements;
    public PoleElementController LastBlock;
    public ObstacleController Obstacle;

    public float yTarget;
    private float speedOfRising;

    public float RotateSpeed;
    public bool isRotating;

    public delegate void LevelComplete(PoleController pole);
    public LevelComplete onLevelComplete;

    public TextMeshProUGUI txtCount;
    public int Count;

    private void Start()
    {
        foreach(PoleElementController p in PoleElements)
        {
            p.onHitHandler += ElementHitHandler;
        }

        LastBlock.onHitHandler += ElementHitHandler;

        Count = PoleElements.Count + LastBlock.BossPower;
        txtCount.text = Count.ToString();
    }

    public bool isPollRising;
    private bool isPollLowering;
    private float lowerTarget = 0.25f;
    private float speedOfLowering = 12f;

    public float RisingTime;
    private float _currentRisingTime = 0;

    private void Update()
    {
        if(isPollRising)
        {
            Vector3 pos = transform.position;

            transform.position = new Vector3(pos.x, pos.y + speedOfRising * Time.deltaTime, pos.z);
            _currentRisingTime += Time.deltaTime;
        }

        if(_currentRisingTime > RisingTime)
        {
            isPollRising = false;
            _currentRisingTime = 0;
        }

        if(isRotating)
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.Self);
        }

        if (isPollLowering)
        {
            float yCurrent = transform.position.y;

            yCurrent = Mathf.Lerp(yCurrent, lowerTarget, Time.deltaTime * speedOfLowering);

            Vector3 pos = transform.position;

            transform.position = new Vector3(pos.x, yCurrent, pos.z);
        }

        if ( -transform.position.y + yTarget < -0.01)
        {
            isPollLowering = false;
        }
    }

    private void ElementHitHandler(PoleElementController poleElement)
    {
        if (poleElement.isBoss)
        {
            if (poleElement.BossPower == 0)
            {
                onLevelComplete.Invoke(this);
            }           
        }
        else
        {
            PoleElements.Remove(poleElement);

            if (PoleElements.Count > 0)
            {
                PoleElements[0].meshCollider.enabled = true;
            }
            else if(PoleElements.Count == 0)
            {
                Obstacle.Rotator.StopRotating();
                Obstacle.RiseTheObstacle(-1);
            }

            lowerTarget -= 0.5f;
            LoweringThePole();
        }

        Count = PoleElements.Count + LastBlock.BossPower;
        txtCount.text = Count.ToString("00");
    }

    public void RiseThePole()
    {
        speedOfRising = (- transform.position.y + yTarget) / 2f;
        isPollRising = true;
    }

    private void LoweringThePole()
    {
        isPollLowering = true;
        
    }
}
