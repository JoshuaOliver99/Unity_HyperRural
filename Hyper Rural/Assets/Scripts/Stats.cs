using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stats : MonoBehaviour
{
    public Text Status; // For displaying stats
    public int econemy, environment, appeal, ecoDiversity = 0;
    void Update()
    {
        Status.text = ("Economy: " + econemy + " environment: " + environment + " Appeal: " + appeal +
                " Eco-DIversity: " + ecoDiversity);
    }
}