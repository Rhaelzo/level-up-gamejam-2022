using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyToSpawn;

    [SerializeField]
    private Transform _enemySpawnPoint;
    
    private void Awake() 
    {
        Instantiate(_enemyToSpawn, _enemySpawnPoint);
    }
}
