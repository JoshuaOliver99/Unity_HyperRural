using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChartValues : MonoBehaviour
{
    public Image ecoChart;
    public Image envirChart;
    public Image appChart;
    public Image ecoDChart;

    void Update()
    {
        ecoChart.fillAmount = (float)GameController.economy / 10;
        envirChart.fillAmount = (float)GameController.environment / 10;
        appChart.fillAmount = (float)GameController.appeal / 10;
        ecoDChart.fillAmount = (float)GameController.ecoDiversity / 10;

        if (GameController.economy < 5)
            ecoChart.color = new Color(255, 0, 0);
        else if (GameController.economy > 10)
            ecoChart.color = new Color(0, 255, 0);
        else
            ecoChart.color = new Color(0, 0, 0);

        if (GameController.environment < 5)
            envirChart.color = new Color(255, 0, 0);
        else if (GameController.environment > 10)
            envirChart.color = new Color(0, 255, 0);
        else
            envirChart.color = new Color(0, 0, 0);

        if (GameController.appeal < 5)
            appChart.color = new Color(255, 0, 0);
        else if (GameController.appeal > 10)
            appChart.color = new Color(0, 255, 0);
        else
            appChart.color = new Color(0, 0, 0);

        if (GameController.ecoDiversity < 5)
            ecoDChart.color = new Color(255, 0, 0);
        else if (GameController.ecoDiversity > 10)
            ecoDChart.color = new Color(0, 255, 0);
        else
            ecoDChart.color = new Color(0, 0, 0);

    }
}