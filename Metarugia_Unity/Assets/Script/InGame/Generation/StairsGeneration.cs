using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsGeneration : MonoBehaviour
{
    [SerializeField] private GameObject stairs;//階段オブジェクト
    public int numberOfGeneration;//生成する数
    private float spawnOffset = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //階段の生成
        for(var i = 0; i < numberOfGeneration; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(i * spawnOffset, i * spawnOffset, 0f);
            Instantiate(stairs, spawnPosition, Quaternion.identity);
        }
        
    }

    
}
