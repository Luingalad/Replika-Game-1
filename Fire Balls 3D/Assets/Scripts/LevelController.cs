using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<PoleController> Poles;
    public List<ObstacleController> Obstacles;
    public List<Transform> Locations;

    public PlayerController Player;

    public UIController UIController;

    void Start()
    {
        foreach(PoleController p in Poles)
        {
            p.onLevelComplete += onLevelCompleteHandler;
        }

        UIController.onLevelStarting += onLevelStart;
    }

    private void onLevelStart()
    {
        StartCoroutine(LevelStart());
    }

    private void onLevelCompleteHandler(PoleController pole)
    {
        Poles.Remove(pole);
        //Move
        Debug.Log("Boss Killed");

        if (Poles.Count > 0)
        {
            StartCoroutine(MovePlayer());
        }
        else
        {
            //LevelDone
            Debug.Log("Level Bitti");
            UIController.ShowHideFinishUI();
        }
    }

    public void onLevelLostHandler()
    {
        Debug.Log("GG Well Played");
        Player.isShootingEnable = false;
        Poles[0].isRotating = false;
        Poles[0].Obstacle.Rotator.StopRotating();
        Poles[0].Obstacle.RiseTheObstacle(-1);
        UIController.ShowHideFailUI();
    }

    public void onBonusPoint()
    {
        Debug.Log("You get 10 points");
    }

    private IEnumerator MovePlayer()
    {
        Player.isShootingEnable = false;
        Player.MoveNext(Locations[0]);

        while(Player.isMoving)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Player.RotatePlayer(Locations[1]);

        while(Player.isRotating)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Player.MoveNext(Locations[1]);
        Poles[0].RiseThePole();
        Poles[0].isRotating = true;

        while (Player.isMoving)
        {
            yield return new WaitForSeconds(0.2f);
        }

        while(Poles[0].isPollRising)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Obstacles[0].RiseTheObstacle(0);

        while(Obstacles[0].isRising)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Obstacles[0].Rotator.StartRotating();

        Player.isShootingEnable = true;
    }

    private IEnumerator LevelStart()
    {
        Obstacles[0].RiseTheObstacle(0);

        while (Obstacles[0].isRising)
        {
            yield return new WaitForSeconds(0.2f);
        }

        Obstacles[0].Rotator.StartRotating();
                
        Obstacles.RemoveAt(0);
        Player.isShootingEnable = true;


    }    
}
