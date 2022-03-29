using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawner : MonoBehaviour
{
    // This script enables UFO to be spawned randomly

    public UFO ufoPrefab;
    private UFO ufo;
    public int chance = 2000;

    void Update()
    {
        SpawnUFO();
    }

    void SpawnUFO()
    {
        if (Random.Range(0, chance) == 1 && (!ufo || ufo.destroyed))
        {
            ufo = Instantiate(ufoPrefab, new Vector3(7, 2.5f, 0), Quaternion.identity);
        }
    }
}
