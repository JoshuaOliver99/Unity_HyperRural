using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestStatusHandler : MonoBehaviour
{
    public Text Status; // For status

    public int stat1, stat2, stat3, stat4, stat5 = 0;

    static CompetitionTable competitionTable;

    void Start()
    {
    }

    void Update()
    {

        Status.text = ("stat1: " + stat1 + " stat2: " + stat2 + " stat3: " + stat3 +
                " stat4: " + stat4 + " stat5: " + stat5);
    }
}
