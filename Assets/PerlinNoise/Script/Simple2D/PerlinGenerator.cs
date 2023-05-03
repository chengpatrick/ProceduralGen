using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGenerator : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;

    [Header("Tile Prefabs")]
    public GameObject prefab_grass;
    public GameObject prefab_earth;
    public GameObject prefab_water;
    public GameObject prefab_rock;

    Dictionary<int, GameObject> tile_group;

    [Header("Map Size")]
    public int width = 16;
    public int height=9;

    [Header("Generate Setting")]
    public bool use_seed = false;
    public float magnification = 7.0f;
    public int x_offset=0, y_offset=0;
    public bool random_seed = false;
    public int seed = 0;

    List<List<int>> noise_grid=new List<List<int>>();
    List<List<GameObject>> tile_grid=new List<List<GameObject>>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGenerate()
    {
        CreateTileSet();
        CreateTileGroup();
        GenerateMap();
    }

    public void ClearScene()
    {
        foreach (List<GameObject> list in tile_grid)
        {
            foreach(GameObject obj in list)
            {
                DestroyImmediate(obj);
            }
        }

        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        foreach (Transform child in transform)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
        tile_grid = new List<List<GameObject>>();
    }
    
    void CreateTileSet()
    {
        tileset=new Dictionary<int,GameObject>();
        tileset.Add(0,prefab_grass);
        tileset.Add(1,prefab_earth);
        tileset.Add(2,prefab_water);
        tileset.Add(3,prefab_rock);
    }

    void CreateTileGroup()
    {
        tile_group=new Dictionary<int,GameObject>();
        foreach(KeyValuePair<int,GameObject> pair in tileset)
        {
            GameObject group=new GameObject(pair.Value.name);
            group.transform.parent = transform;
            group.transform.localPosition=new Vector3(0,0,0);
            tile_group.Add(pair.Key, group);
        }
    }

    void GenerateMap()
    {
        if (use_seed)
        {
            if (random_seed)
            {
                GetRandomSeed();
            }
            GetSeedValue();
        }

        for (int x=0;x<width;x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());

            for(int y= 0; y<height;y++)
            {
                int tile_id=GetIdByPerlin(x,y);
                noise_grid[x].Add(tile_id);
                CreateTile(tile_id,x,y);
            }
        }
    }

    int GetIdByPerlin(int x, int y)
    {
        
        float raw_perlin=Mathf.PerlinNoise((x-x_offset)/magnification,(y-y_offset)/magnification);
        float clamp_perlin=Mathf.Clamp(raw_perlin,0.0f,1.0f);
        float scale_perlin=clamp_perlin* tileset.Count;
        if (scale_perlin == 4)
        {
            scale_perlin = 3;
        }
            
        return Mathf.FloorToInt(scale_perlin);
    }

    void CreateTile(int id,int x,int y)
    {
        GameObject tile_prefab=tileset[id];
        GameObject group = tile_group[id];
        GameObject tile=Instantiate(tile_prefab,group.transform);

        tile.name=string.Format("tile_x[{0}]_y[{1}]",x,y);
        tile.transform.localPosition=new Vector3(x,0,y);

        tile_grid[x].Add(tile);
    }

    void GetRandomSeed()
    {
        seed = Random.Range(999, 9999);
    }

    void GetSeedValue()
    {

        float pi=Mathf.PI*seed;
        int pi_int = (int)pi;
        string pi_string = pi_int.ToString();

        x_offset = pi_string[pi_string.Length - 1]*pi_string[0]+ pi_string[pi_string.Length - 1] * pi_string[1];
        y_offset = pi_string[pi_string.Length - 2] * pi_string[0] + pi_string[pi_string.Length - 1] * pi_string[1];
    }
}
