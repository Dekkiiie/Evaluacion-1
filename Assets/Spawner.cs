using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Player pl;
    public List<GameObject> myPrefabs = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(Spawn());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Spawn()
    {
        while (true)
        {

            if (pl.totalDistance > 20)
            {
                Instantiate(myPrefabs[Random.Range(0, myPrefabs.Count -1)], new Vector3(transform.position.x, 2.56f, 0), Quaternion.identity);
                pl.totalDistance = 0;
                if(pl.wallDistance > 200)//hardcodeado por q me dio paja xd
                {
                    Instantiate(myPrefabs[4], new Vector3(transform.position.x, 2.56f, 0), Quaternion.identity);
                    pl.wallDistance = 0;
                }

            }
            // Instantiate at position (0, 0, 0) and zero rotation.
            yield return null;
        }
    }
}
