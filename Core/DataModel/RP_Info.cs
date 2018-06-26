using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace RoundPointGraph
{
    [System.Serializable]
    public class RP_Info
    {
        public int accuracy = 100;
        public RP_LineInfo radialLineInfo;//径向
        public RP_LineInfo angleLineInfo;//角向
        public float startAngle = 0;//超始角度
        public float angleSpan = 10;//角度跨度
        public int radialCount = 5;//径向圆个数
        public bool clockwise = false;//顺时针
        public int fontSize = 18;//字体大小
        public Color fontColor = Color.black;//字体颜色
    }
}