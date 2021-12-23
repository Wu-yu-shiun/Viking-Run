using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShield : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        int random = Random.Range(0, 3);
        if (random == 0) gameObject.SetActive(true);
        else gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
