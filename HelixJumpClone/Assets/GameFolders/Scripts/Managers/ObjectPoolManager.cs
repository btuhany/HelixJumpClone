using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] GameObject _splashPrefab;
    Queue<GameObject> _splashPool = new Queue<GameObject>();
    public static ObjectPoolManager Instance { get; private set; }
    private void Awake()
    {
        SingletonThisObject();
        InitializePool();
    }
    
    public void InitializePool()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject newObject = Instantiate(_splashPrefab, this.transform);
            newObject.transform.SetParent(this.transform);
            newObject.SetActive(false);
            _splashPool.Enqueue(newObject);

        }
    }
    public GameObject GetSplashFromPool()
    {
        if(_splashPool.Count == 0)
        {
            IncreasePoolSize();

        }

        GameObject gameObj = _splashPool.Dequeue();
        gameObj.SetActive(true);
        StartCoroutine(SetPoolTimer(gameObj));
        return gameObj;
    }

    public void SetSplashToPool(GameObject obj)
    {
        if (obj == null) return;
        obj.transform.SetParent(this.transform);
        obj.SetActive(false);
        _splashPool.Enqueue(obj);
    }
    void SingletonThisObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator SetPoolTimer(GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        SetSplashToPool(obj);
        yield return null;
    }
    private void IncreasePoolSize()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject newObject = Instantiate(_splashPrefab, this.transform);
            newObject.transform.SetParent(this.transform);
            newObject.SetActive(false);
            _splashPool.Enqueue(newObject);

        }
    }

}
