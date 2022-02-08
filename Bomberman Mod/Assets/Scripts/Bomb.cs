using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject explosionPrefab2;
    public LayerMask levelMask;
    public bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));  

        GetComponent<MeshRenderer>().enabled = false;
        exploded = true;
        transform.Find("Collider").gameObject.SetActive(false);
        Destroy(gameObject, 0.3f);
    }

    private IEnumerator CreateExplosions(Vector3 direction)
    {
        //1
        for (int i = 1; i < 3; i++) 
        { 
            //2
            RaycastHit hit; 
            //3
            Physics.Raycast(transform.position + new Vector3(0,.5f,0), direction, out hit, 
                    i, levelMask); 

            //4
            if (!hit.collider) 
            { 
                if (i == 1)
                {
                    Instantiate(explosionPrefab, transform.position + (i * direction),
                    //5 
                    explosionPrefab.transform.rotation); 
                    //6
                }
                else
                {
                    Instantiate(explosionPrefab2, transform.position + (i * direction),
                    //5 
                    explosionPrefab2.transform.rotation); 
                    //6
                }
            } 
            else 
            { //7
              break; 
            }

            //8
            yield return new WaitForSeconds(.05f); 
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!exploded && other.CompareTag("Explosion"))
        { // 1 & 2  
            CancelInvoke("Explode"); // 2
            Explode(); // 3
        }  
    }
}
