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
            if (currentScene == "MainIsland")
            {
                LoadScene("MileIsland");
            }
            else
            {
                LoadScene("MainIsland");
            }
        }
    }

    public void LoadScene(string sceneName)
    {      
        SceneManager.LoadScene(sceneName);
    }


}
