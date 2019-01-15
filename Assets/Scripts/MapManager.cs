using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    [SerializeField] List<GameObject> Enemies;
    [SerializeField] string SceneName;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void EnemyKilled(GameObject Enemy)
    {
        Enemies.Remove(Enemy);
        if (Enemies.Count == 0)
            RestartLevel();
    }

}