using UnityEngine;

public class WallpaperManager : MonoBehaviour
{
    public Vector2 wallpaperSize = new Vector2(19.2f, 12f);
    
    public static WallpaperManager instance { get; private set; }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        setFrames();
        createBoundries();
    }
    void setFrames()
    {
        Application.targetFrameRate = 30;
    }
    void createBoundries()
    {
        createBoundry("Top", new Vector2(0, wallpaperSize.y / 2), new Vector2(wallpaperSize.x, 0.1f));
        createBoundry("Bottom", new Vector2(0, -wallpaperSize.y / 2), new Vector2(wallpaperSize.x, 0.1f));
        createBoundry("Left", new Vector2(-wallpaperSize.x / 2, 0), new Vector2(0.1f, wallpaperSize.y));
        createBoundry("Right", new Vector2(wallpaperSize.x / 2, 0), new Vector2(0.1f, wallpaperSize.y));
    }

    void createBoundry(string name, Vector2 pos, Vector2 size)
    {
        GameObject boundry = new GameObject(name);
        boundry.transform.position = pos;
        boundry.AddComponent<BoxCollider2D>();
        boundry.GetComponent<BoxCollider2D>().size = size;
        boundry.AddComponent<SpriteRenderer>();
        boundry.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        boundry.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
}
