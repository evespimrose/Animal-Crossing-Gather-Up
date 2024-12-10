using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneManager : SingletonManager<GameSceneManager>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float travelTime = 4f;
    private bool isTransitioning = false;

    protected override void Awake()
    {
        base.Awake();
        if (fadeImage != null)
        {
            DontDestroyOnLoad(fadeImage.canvas.gameObject);
            // �ʱ� ���´� ����
            fadeImage.color = new Color(0, 0, 0, 0);  
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isTransitioning)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            ChangeScene(currentScene);
        }
    }
    public void ChangeScene(string currentScene)
    {
        if (!isTransitioning)
        { 
            currentScene = SceneManager.GetActiveScene().name;
            if (currentScene == "GameScene")
            {
                StartCoroutine(LoadSceneWithDelay("MileIsland"));
            }
            else
            {
                StartCoroutine(LoadSceneWithDelay("GameScene"));
            }
        }
    }
  

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        isTransitioning = true;

        // �ð� ����
        Time.timeScale = 0f;

        // ���̵� ��
        float time = 0f;
        while (time < fadeDuration)
        {
            time += Time.unscaledDeltaTime;  // timeScale ������ ���� �ʴ� deltaTime ���
            fadeImage.color = new Color(0, 0, 0, time / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 1);

        // ���� ������Ʈ ����
        var bugs = FindObjectsOfType<Bug>();
        var fishes = FindObjectsOfType<OceanFish>();
        foreach (var bug in bugs) Destroy(bug.gameObject);
        foreach (var fish in fishes) Destroy(fish.gameObject);

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSecondsRealtime(0.5f);  // timeScale ������ ���� �ʴ� ���

        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.AddHours(travelTime);
        }

        // ���̵� �ƿ�
        time = fadeDuration;
        while (time > 0)
        {
            time -= Time.unscaledDeltaTime;  // timeScale ������ ���� �ʴ� deltaTime ���
            fadeImage.color = new Color(0, 0, 0, time / fadeDuration);
            yield return null;
        }
        fadeImage.color = new Color(0, 0, 0, 0);

        // �ð� �ٽ� ����
        Time.timeScale = 1f;

        isTransitioning = false;
    }


}
