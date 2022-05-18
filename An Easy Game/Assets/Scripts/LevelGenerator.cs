using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] List<Transform> parts;
    [SerializeField] private Player player;
    [SerializeField] private float range = 10f;
    private Vector3 lastEndPos;
    private void Awake()
    {
        lastEndPos = start.Find("EndPos").position;
    }

    private void Update()
    {
        if (Vector3.Distance(player.GetComponent<Transform>().position, lastEndPos) < range)
        {
            SpawnPart();
        }
    }

    private void SpawnPart()
    {
        lastEndPos = SpawnPart(lastEndPos).Find("EndPos").position;
    }

    private Transform SpawnPart(Vector3 position)
    {
        return Instantiate(parts[Random.Range(0, parts.Count)], position, Quaternion.identity);
    }
}
