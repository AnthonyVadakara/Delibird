using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileConverter : MonoBehaviour
{
    [System.Serializable]
    public struct TileRule
    {
        public Tile tile;
        public GameObject obj;
    } 

    public Tilemap map;
    Vector3 offset;
    public List<TileRule> rules;

    // Start is called before the first frame update
    void Start()
    {
        if (map == null)
        {
            map = transform.parent.GetComponent<Tilemap>();
        }

        Dictionary<Tile, GameObject> dictionary = new Dictionary<Tile, GameObject>();
        foreach (TileRule rule in rules)
        {
            dictionary.Add(rule.tile, rule.obj);
        }

        BoundsInt bounds = map.cellBounds;
        TileBase[] tiles = map.GetTilesBlock(bounds);
        offset = (map.origin * 2) + (new Vector3(map.transform.position.x, map.transform.position.z, 0)*2) ;        
        offset.z = offset.y;
        offset.y = 0;

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Tile tile = (Tile)tiles[x + y * bounds.size.x];
                Vector3 pos = new Vector3(x * 2, 0, y * 2) + offset;
                if (tile != null && dictionary.ContainsKey(tile))
                {
                    Instantiate(dictionary[tile], pos, Quaternion.identity);
                }
            }
        }
        map.GetComponent<TilemapRenderer>().enabled = false;
    }
}
