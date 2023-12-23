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

    //public Queue<TowerTile> tilesPool;

    //[SerializeField]
    //public int size;

    //private void Start()
    //{
    //    tilesPool = new Queue<TowerTile>();
    //    TowerTile tile = null;
    //    for(int i = 0; i < size ; i++)
    //    {
    //        tile = Random.value > tower.SpecialTileChance ? tower.TilePrefab :
    //               tower.SpecialTilePrefabs[Random.Range(0, tower.SpecialTilePrefabs.Length)];
    //        Instantiate(tile, tower.transform);
    //       tile.gameObject.SetActive(false);
    //       tilesPool.Enqueue(tile);
    //    }
    //}

    //public TowerTile GetFromPool(Vector3 position, Quaternion rotation)
    //{
    //    TowerTile tile = tilesPool.Dequeue();

    //    tile.gameObject.SetActive(true);
    //    tile.gameObject.transform.position = position;
    //    tile.gameObject.transform.rotation = rotation;

    //    tilesPool.Enqueue(tile);

    //    return tile;
    //}
}
