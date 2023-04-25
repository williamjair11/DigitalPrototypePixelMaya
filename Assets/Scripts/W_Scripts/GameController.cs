using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _itemRestoreHealthPrefab;
    [SerializeField] private GameObject _EnemyPrefab;
    void Start()
    {
        SpawnEnemys();
    }
    void Update()
    {
        
    }

    public void RestarLevel() 
    {
        SceneManager.LoadScene("testVR");
    }

    public void SpawnEnemys() 
    {       
            Instantiate(_itemRestoreHealthPrefab, new Vector3(0, 0, 10), new Quaternion(0, 0, 0, 0));
            Instantiate(_EnemyPrefab, new Vector3(0, 0, -10), new Quaternion(0, 0, 0, 0));      
    }
}
