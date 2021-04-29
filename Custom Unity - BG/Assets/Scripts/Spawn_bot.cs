using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_bot : MonoBehaviour
{
    public GameObject[] bots;
    private GameObject inst_bot;

    void Start()
    {
        int rand = Random.Range(0, bots.Length - 1);
        inst_bot = Instantiate(bots[rand], bots[rand].transform.position, Quaternion.identity) as GameObject;
        inst_bot.transform.localScale = new Vector3(0.84f, 0.505f, - 1.591f);
    }


    void Update()
    {
        
    }
}
