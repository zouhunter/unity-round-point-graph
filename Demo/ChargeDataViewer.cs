using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using RoundPointGraph;

public class ChargeDataViewer : MonoBehaviour
{
    public RP_Pos[] positions_01;
    public RP_Pos[] positions_02;
    [SerializeField]
    protected GameObject prefab01;
    [SerializeField]
    protected GameObject prefab02;
    [SerializeField]
    public RoundChart chart;
    [SerializeField]
    public RP_LineInfo lineInfo_01;
    [SerializeField]
    public RP_LineInfo lineInfo_02;
    private void OnGUI()
    {
        if (GUILayout.Button("CreateLine01"))
        {
            var icons = chart.CreatePoints("line01", positions_01, prefab01, lineInfo_01);
            for (int i = 0; i < icons.Length; i++)
            {
                var icon = icons[i].GetComponent<ChartIcon>();
                icon.iconInfo = string.Format("line01-第{0}个点", i);
            }
            chart.DisplyText(20);
        }
        if (GUILayout.Button("CreateLine02"))
        {
            var icons = chart.CreatePoints("line02", positions_02, prefab02, lineInfo_02);
            for (int i = 0; i < icons.Length; i++)
            {
                var icon = icons[i].GetComponent<ChartIcon>();
                icon.iconInfo = string.Format("line02-第{0}个点", i);
            }
            //CreateTexts(chart.radialPoints);
            chart.DisplyText(20);
        }
        if (GUILayout.Button("Clear"))
        {
            chart.ClearLines();
        }
    }

}
