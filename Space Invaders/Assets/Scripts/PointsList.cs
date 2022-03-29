using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsList : MonoBehaviour
{

    // This is the script for the "points earned for killing each monster" animation on the menu page
    void Start()
    {
        StartCoroutine("StartAnimate");   
    }

    IEnumerator StartAnimate()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(6).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameObject.transform.GetChild(7).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);


    }
}


