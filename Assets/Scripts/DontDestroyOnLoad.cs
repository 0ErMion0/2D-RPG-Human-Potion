using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad s_Instance = null;
    //Scene scene;

    private void Awake()
    {
        //scene = SceneManager.GetActiveScene();

        //if (scene.name == "Stage1" || scene.name == "Stage2")
        //{
        //    if (s_Instance)
        //    {
        //        DestroyImmediate(this.gameObject); // s_Instance�� ������ ���� ���� ������Ʈ �ı�
        //                                           // -> �ٸ� ������Ʈ�� DontDestroyOnLoad�� �����Ǿ� ���� �� �ߺ� ���� ���� ���� ����
        //    }

        //    //���� ���� ������Ʈ �Ҵ��ϰ� ���� ������Ʈ�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ��
        //    s_Instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        if (s_Instance)
        {
            DestroyImmediate(this.gameObject); // s_Instance�� ������ ���� ���� ������Ʈ �ı�
                                               // -> �ٸ� ������Ʈ�� DontDestroyOnLoad�� �����Ǿ� ���� �� �ߺ� ���� ���� ���� ����
        }

        //���� ���� ������Ʈ �Ҵ��ϰ� ���� ������Ʈ�� �� ��ȯ �ÿ��� �ı����� �ʵ��� ��
        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    //private void OnEnable()
    //{
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{

    //}

    //private void OnDisable()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //private void Update()
    //{
    //    DestroyCheck();
    //}

}

