using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class TileSpawner : MonoBehaviour
{

    [SerializeField]
    private Tower tower;
    [SerializeField]
    private int minSize, maxSize;

    #region Singleton
    public static TileSpawner Instance;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<TowerTile>(CreatedTile, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, minSize, maxSize);
    }
    #endregion
    public ObjectPool<TowerTile> _pool;

    TowerTile CreatedTile()
    {
        TowerTile tile = Random.value > tower.SpecialTileChance ? tower.TilePrefab :
             tower.SpecialTilePrefabs[Random.Range(0, tower.SpecialTilePrefabs.Length)];
        tile = Instantiate(tile, parent: tower.gameObject.transform);
        tile.gameObject.SetActive(false);
        return tile;
    }

    void OnTakeFromPool(TowerTile Instance)
    {
        Instance.gameObject.SetActive(true);
    }

    private void OnReturnedToPool(TowerTile Instance)
    {
        Instance.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(TowerTile Instance)
    {
        Destroy(Instance.gameObject);
    }
}
