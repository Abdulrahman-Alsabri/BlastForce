using UnityEngine;
using UnityEngine.SceneManagement;

public class FalseFloorTrigger : MonoBehaviour
{
    // reference terrain and its data
    public Terrain terrain;
    private TerrainData terrainData;
    public AudioSource tutorialMusic;
    public AudioSource preTutorialMusic;
    public DeactivateFalseFloor deactivateFalseFloor;

    // initialize needed variables
    private float originalFloorDepth = 0.166667f;
    private float desiredFloorDepth = 0f;
    private int startX = 125;
    private int startY = 316;
    private int EndX = 145;
    private int EndY = 335;

    // Start is called before the first frame update
    void Start()
    {
        // searches for terrain object
        GameObject temp = GameObject.Find("Terrain");
        if (temp != null)
        {
            // gets Terrain
            terrain = temp.GetComponent<Terrain>();
            terrainData = terrain.terrainData;
        }
        else
        {
            Debug.Log("Terrain not found");
        }

        // searches for DeactivateFalseFloor object
        GameObject temp2 = GameObject.Find("DeactivateFalseFloor");
        if (temp2 != null)
        {
            // gets Terrain
            preTutorialMusic = temp2.GetComponent<AudioSource>();
        }
        else
        {
            Debug.Log("DeactivateFalseFloor not found");
        }

        // gets Tutorials's Audio
        tutorialMusic = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Scene m_Scene = SceneManager.GetActiveScene();
        string sceneName = m_Scene.name;
        if (sceneName == "Level1")
        {
            tutorialMusic.Stop();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            preTutorialMusic.Stop();
            tutorialMusic.Play();
            ActivateFalseFloor();
        }
    }

    void ActivateFalseFloor()
    {
        // get overall height and width of terrain
        int heightmapWidth = terrainData.heightmapWidth;
        int heightmapHeight = terrainData.heightmapHeight;

        // get all heights from map
        float[,] heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);

        // change desired floor area's heights to desiredFloorDepth
        for (int x = startX; x < EndX; x++)
        {
            for (int y = startY; y < EndY; y++)
            {
                heights[x, y] = desiredFloorDepth;
            }
        }

        // set the heights and apply to terrain
        terrainData.SetHeights(0, 0, heights);
    }

    public void DeactivateFalseFloor()
    {
        // get overall height and width of terrain
        int heightmapWidth = terrainData.heightmapWidth;
        int heightmapHeight = terrainData.heightmapHeight;

        // get all heights from map
        float[,] heights = terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);

        // change desired floor area's heights to original delpth
        for (int x = startX; x < EndX; x++)
        {
            for (int y = startY; y < EndY; y++)
            {
                heights[x, y] = originalFloorDepth;
            }
        }

        // set the heights and apply to terrain
        terrainData.SetHeights(0, 0, heights);
    }
}
