using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] Levels levels;
    [SerializeField] Vector3 stPos;
    [SerializeField] Vector3 endPos;

    private int currIdx = 0;
    private GameManager gameManager;
    private List<GameObject> activeRoads;


    private void Start()
    {
        gameManager = GameManager.instance;
        activeRoads = new List<GameObject>();
        Init();
    }

    private IEnumerator IEMoveRoad(Transform road)
    {
        while (true)
        {
            road.localPosition = Vector3.MoveTowards(road.position, endPos, gameManager.speed * Time.deltaTime);

            yield return null;

            if (Vector3.Distance(road.position, endPos) < 1)
            {
                road.gameObject.SetActive(false);
                activeRoads.Remove(road.gameObject);
                SpawnRoad();
                yield break;
            }
        }
    }


    private void Init()
    {
        for (int i = -1; i <= 2; i++)
        {
            Vector3 pos = new Vector3(0, 0, i * 32.5f);
            Transform road = gameManager.objectPooler.SpwanObject("RoadNormal", pos).transform;
            activeRoads.Add(road.gameObject);
            StartCoroutine(IEMoveRoad(road));
        }
    }

    private void SpawnRoad()
    {
        if (currIdx >= levels.levels[gameManager.currLevel].items.Count()) return;

        string roadName = levels.levels[gameManager.currLevel].items[currIdx];
        Transform road = gameManager.objectPooler.SpwanObject(roadName, stPos).transform;
        activeRoads.Add(road.gameObject);
        StartCoroutine(IEMoveRoad(road));
        currIdx++;
    }

    internal void Reset()
    {
        foreach (GameObject road in activeRoads) road.SetActive(false);

        StopAllCoroutines();
        activeRoads.Clear();
        currIdx = 0;
        Init();
    }
}