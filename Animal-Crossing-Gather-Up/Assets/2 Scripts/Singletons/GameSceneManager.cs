using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    private static GameSceneManager instance;
    public static GameSceneManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameSceneManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("GameSceneManager");
                    instance = go.AddComponent<GameSceneManager>();
                }
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "GameScene")
            {
                LoadScene("MileIsland");
            }
            else
            {
                LoadScene("GameScene");
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        // 기존 스폰된 오브젝트들 정리
        var bugs = FindObjectsOfType<Bug>();
        var fishes = FindObjectsOfType<OceanFish>();

        foreach (var bug in bugs) Destroy(bug.gameObject);
        foreach (var fish in fishes) Destroy(fish.gameObject);

        SceneManager.LoadScene(sceneName);
    }


}
