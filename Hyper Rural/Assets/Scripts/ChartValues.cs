using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChartValues : MonoBehaviour
{
    public static int ecoChartMax = 0; //Maximum value for each for the charts
    public static int envirChartMax = 0;
    public static int appChartMax = 0;
    public static int ecoDChartMax = 0;
    public Image ecoChart;
    public Image envirChart;
    public Image appChart;
    public Image ecoDChart;

    private void Start()
    {
        ChartManagerStart();
    }

    void Update()
    {
        ChartManagerUpdate();
    }

    public void ChartManagerStart()
    {
        ecoChart.fillAmount = ecoChartMax;
        envirChart.fillAmount = envirChartMax;
        appChart.fillAmount = appChartMax;
        ecoDChart.fillAmount = ecoDChartMax;
    }

    private void ChartManagerUpdate()
    {
        ecoChart.fillAmount = GameController.economy / 15;
        envirChart.fillAmount = GameController.environment / 15;
        appChart.fillAmount = GameController.appeal / 15;
        ecoDChart.fillAmount = GameController.ecoDiversity / 15;
    }
}