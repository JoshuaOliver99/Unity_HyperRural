using UnityEngine;
using UnityEngine.UI;

public class ChartValues : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image ecoChart; // Links to images of charts
    [SerializeField] Image envirChart;
    [SerializeField] Image appChart;
    [SerializeField] Image ecoDChart;

    void Update()
    {
        ecoChart.fillAmount = (float)GameController.economy / 10; // Int value of stats conveted to float, applied to fill amount (0-1) 
        envirChart.fillAmount = (float)GameController.environment / 10;
        appChart.fillAmount = (float)GameController.appeal / 10;
        ecoDChart.fillAmount = (float)GameController.ecoDiversity / 10;

        if (GameController.economy < 3) // If (economy is low)
            ecoChart.color = new Color(255, 0, 0); // Colour bar red
        else if (GameController.economy > 8) // If (economy is heigh)
            ecoChart.color = new Color(0, 255, 0); // Colour bar green
        else
            ecoChart.color = new Color(0, 0, 0); // else, black

        if (GameController.environment < 3)
            envirChart.color = new Color(255, 0, 0);
        else if (GameController.environment > 8)
            envirChart.color = new Color(0, 255, 0);
        else
            envirChart.color = new Color(0, 0, 0);

        if (GameController.appeal < 3)
            appChart.color = new Color(255, 0, 0);
        else if (GameController.appeal > 8)
            appChart.color = new Color(0, 255, 0);
        else
            appChart.color = new Color(0, 0, 0);

        if (GameController.ecoDiversity < 3)
            ecoDChart.color = new Color(255, 0, 0);
        else if (GameController.ecoDiversity > 8)
            ecoDChart.color = new Color(0, 255, 0);
        else
            ecoDChart.color = new Color(0, 0, 0);
    }
}