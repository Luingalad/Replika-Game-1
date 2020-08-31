using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] 
    private Button btnStart;
    [SerializeField]
    private Button btnTryAgain;
    [SerializeField]
    private Button btnPlayAgain;

    public GameObject StartUI;
    public GameObject FailUI;
    public GameObject FinishUI;

    public delegate void LevelStarting();
    public LevelStarting onLevelStarting;

    void Start()
    {
        btnStart.onClick.AddListener(onStartLevel);
        btnTryAgain.onClick.AddListener(onFail);
        btnPlayAgain.onClick.AddListener(onFinish);
    }
        
    /// <summary>
    /// Button click function
    /// Can click after application load
    /// </summary>
    private void onStartLevel()
    {
        if(onLevelStarting != null)
        {
            onLevelStarting.Invoke();
            ShowHideStartUI();
        }
    }

    /// <summary>
    /// Button click function
    /// Can click atfer level failed
    /// </summary>
    private void onFail()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Button click function
    /// Can click after level finished
    /// </summary>
    private void onFinish()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowHideStartUI()
    {
        StartUI.SetActive(!StartUI.activeSelf);
    }

    public void ShowHideFailUI()
    {
        FailUI.SetActive(!FailUI.activeSelf);
        StartCoroutine(ShowGamesEnd());
    }

    public void ShowHideFinishUI()
    {
        FinishUI.SetActive(!FinishUI.activeSelf);
    }

    private IEnumerator ShowGamesEnd()
    {
        CanvasGroup cg = FailUI.GetComponent<CanvasGroup>();
        
        for(int i = 0; i < 100; i++)
        {
            cg.alpha += 0.01f;
            yield return null;
        }
    }    
}
