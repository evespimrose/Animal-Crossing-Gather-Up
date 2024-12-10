using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : SingletonManager<GameSceneManager>
{

    public void ChangeScene(string currentScene)
    {
        currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "GameScene")
        {
            LoadScene("MileIsland");
        }
        else
        {
            LoadScene("GameScene");
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
