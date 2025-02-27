using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject rocketPrefab;
    private int numberOfRockets = 100;
    public List<GameObject> simple_Rockets;
    public List<GameObject> matingPool;
    public int generation = 0;
    private int DNAlength = 300;
    public int currIndex = 0;
    public float mutationRate = 0.01f;
    public static GameManager instance { get; private set; }
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
        spawnRockets();
    }
    void initFroces(Simple_Rocket r)
    {
        for (int i = 0; i < DNAlength; i++)
        {
            r.forces.Add(Random.insideUnitCircle.normalized);
        }
    }
    void spawnRockets()
    {
        Debug.Log("Spawning in " + numberOfRockets + " rockets");
        for (int i = 0; i < simple_Rockets.Count; i++) if (simple_Rockets != null) Destroy(simple_Rockets[i]);
        simple_Rockets = new List<GameObject>();
        for (int i = 0; i < numberOfRockets; i++)
        {
            Vector2 pos = transform.position;
            GameObject rocket = Instantiate(rocketPrefab, pos, Quaternion.identity);
            Simple_Rocket r = rocket.GetComponent<Simple_Rocket>();
            r.numForces = DNAlength;
            Debug.Log("Rocket " + i + " created with " + r.numForces + " forces " + DNAlength);
            if (generation == 0) initFroces(r);
            else cross(r);
            simple_Rockets.Add(rocket);
        }
    }
    
    void createMatingPool()
    {
        float maxfit = 0;
        for (int i = 0; i < numberOfRockets; i++) maxfit = System.Math.Max(maxfit, simple_Rockets[i].GetComponent<Simple_Rocket>().evaluate());
        for (int i = 0; i < numberOfRockets; i++) simple_Rockets[i].GetComponent<Simple_Rocket>().fitness /= maxfit;
        matingPool = new List<GameObject>();
        Debug.Log("Max fit " + maxfit);
        for (int i = 0; i < numberOfRockets; i++)
        {
            int n = (int)(simple_Rockets[i].GetComponent<Simple_Rocket>().fitness * 100);
            Debug.Log("Rocket " + i + " has fitness " + simple_Rockets[i].GetComponent<Simple_Rocket>().fitness + " and will be added " + n + " times to mating pool");
            for (int j = 0; j < n; j++) matingPool.Add(simple_Rockets[i]);
        }
        Debug.Log("Created Mating pool with size " + matingPool.Count);
    }

    void cross(Simple_Rocket r)
    {
        Simple_Rocket p1 = matingPool[Random.Range(0, matingPool.Count)].GetComponent<Simple_Rocket>();
        Simple_Rocket p2 = matingPool[Random.Range(0, matingPool.Count)].GetComponent<Simple_Rocket>();
        int mid = Random.Range(0, DNAlength);
        for (int i = 0; i < DNAlength; i++)
        {
            if (i < mid) r.forces.Add(p1.forces[i]);
            else r.forces.Add(p2.forces[i]);
        }
        mutate(r);
    }

    void mutate(Simple_Rocket r)
    {
        for (int i = 0; i < DNAlength; i++)
        {
            if (Random.value < mutationRate) r.forces[i] = Random.insideUnitCircle.normalized;
        }
    }

    void Update()
    {
        if (currIndex >= DNAlength)
        {
            currIndex = 0;
            generation++;
            createMatingPool();
            spawnRockets();
        }
        else
        {
            for(int i = 0; i < numberOfRockets; i++) simple_Rockets[i].GetComponent<Simple_Rocket>().applyFroce(currIndex);
            currIndex++;
            Debug.Log("Curr index " + currIndex);
        }
    }

}
