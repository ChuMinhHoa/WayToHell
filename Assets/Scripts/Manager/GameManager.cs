using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MapManager mapManager = new MapManager();
    public SpawnEnemyManager spawnManager;
    public Transform playerTransform;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    private void Start()
    {
        mapManager.InitData();
    }
    private void Update()
    {
        mapManager.Update();
    }
    public void SetPositionForPlayer(Transform spawnPoint) {
        playerTransform.position = spawnPoint.position;
    }
}
