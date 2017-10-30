using UnityEngine;
using System.Collections;

[System.Serializable]
public class ColorToPrefab
{
    public Color32 color;
    public GameObject prefab;
}

public class LevelGenerator : MonoBehaviour {

    public Texture2D map;

    public ColorToPrefab[] colorMappings;

	void Start ()
    {
        
        GenerateLevel();
	}
    //Generates the entire level
	void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }
    //Geberates each tile
    void GenerateTile(int x, int y)
    {
        //Gets the colors of the map texture
        Color32 pixelcolor = map.GetPixel(x, y);

        if(pixelcolor.a == 0)
        {
            //The pixel is transparent.
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            //The pixel is connected to a prefab and it gets instantiated
            if(colorMapping.color.Equals(pixelcolor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
