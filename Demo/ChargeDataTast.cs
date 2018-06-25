using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using RoundPointGraph;

public class ChargeDataTast : MonoBehaviour {
    public RP_Pos[] positions_01;
    public RP_Pos[] positions_02;
    [SerializeField]
    protected GameObject prefab01;
    [SerializeField]
    protected GameObject prefab02;
    [SerializeField]
    public RoundChart chart;
    [SerializeField]
    public RP_LineInfo lineInfo;
    private void OnGUI()
    {
        if(GUILayout.Button("CreateLine01"))
        {
            chart.CreatePoints("line01", positions_01, prefab01, lineInfo);
        }
        if (GUILayout.Button("CreateLine02"))
        {
            chart.CreatePoints("line02", positions_02, prefab02, lineInfo);
        }
        if(GUILayout.Button("Clear"))
        {
            chart.ClearLines();
        }
    }
}
