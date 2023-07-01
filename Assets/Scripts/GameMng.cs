using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject); // 자기 자신 보존
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 매니저의 sceneLoaded에 체인 연결
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // 체인 걸어서 함수가 매 씬마다 호출되도록 함
    {
        if (scene.name != "Stage1" && scene.name != "Stage2")
        {
            DestroyDontDestroyOnLoadObjects(); // DontDestroyOnLoad 태그 가진 오브젝트 파괴
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 체인 해제
    }

    void DestroyDontDestroyOnLoadObjects()
    {
        // DontDestroyOnLoad로 설정된 모든 오브젝트들을 파괴합니다.
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
    }
}
