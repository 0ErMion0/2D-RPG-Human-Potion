using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMng : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(gameObject); // �ڱ� �ڽ� ����
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �Ŵ����� sceneLoaded�� ü�� ����
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) // ü�� �ɾ �Լ��� �� ������ ȣ��ǵ��� ��
    {
        if (scene.name != "Stage1" && scene.name != "Stage2")
        {
            DestroyDontDestroyOnLoadObjects(); // DontDestroyOnLoad �±� ���� ������Ʈ �ı�
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ü�� ����
    }

    void DestroyDontDestroyOnLoadObjects()
    {
        // DontDestroyOnLoad�� ������ ��� ������Ʈ���� �ı��մϴ�.
        GameObject[] dontDestroyObjects = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
        foreach (GameObject obj in dontDestroyObjects)
        {
            Destroy(obj);
        }
    }
}
