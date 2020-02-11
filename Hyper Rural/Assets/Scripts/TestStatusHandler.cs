using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStatusHandler : MonoBehaviour
{
    public int stat1, stat2, stat3, stat4, stat5 = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("stat1: " + stat1 +
                " stat2: " + stat2 +
                " stat3: " + stat3 +
                " stat4: " + stat4 +
                " stat5: " + stat5);

        }
    }
}
