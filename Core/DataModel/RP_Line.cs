using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RoundPointGraph
{
    public class RP_Line
    {
        public string lineName { get; private set; }
        public Transform parent { get; private set; }
        public GameObject prefab { get; private set; }
        public Vector2[] positions { get; private set; }
        public GameObject[] chartIcons { get; private set; }

        protected List<GameObject> objectPool = new List<GameObject>();

        private RP_LineInfo _lineInfo = new RP_LineInfo();
        public float startAngle { get; set; }
        public bool clockwise { get; set; }

        public RP_LineInfo lineInfo { get { return _lineInfo; } }

        public RP_Line(string lineName,Transform parent)
        {
            this.lineName = lineName;
            this.parent = parent;
        }

        public void UpdateLineInfoSelfty(RP_LineInfo lineInfo)
        {
            if(lineInfo != null)
            {
                _lineInfo = lineInfo;
            }
        }

        public GameObject[] ResetPoints(GameObject prefab,float redius,RP_Pos[] points)
        {
            this.prefab = prefab;
            prefab.gameObject.SetActive(false);
            ClearCreated();
            CreateChartIcons(redius, points);
            return chartIcons;
        }

        private void CreateChartIcons(float redius, RP_Pos[] points)
        {
            if (points == null) return;
            chartIcons = new GameObject[points.Length];
            positions = new Vector2[points.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                var icon = chartIcons[i] = GetIconFromPool(prefab);
                var angle = points[i].angle + startAngle;
                if (clockwise) {
                    angle = -angle;
                }
                var ratio = points[i].ratio;
                positions[i] = icon.transform.localPosition = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad) * ratio * redius, Mathf.Sin(angle * Mathf.Deg2Rad) * ratio * redius);
            }
        }

        private GameObject GetIconFromPool(GameObject prefab)
        {
            GameObject icon = objectPool.Find(x => x.name == prefab.name && !x.gameObject.activeSelf);
            if(icon == null)
            {
                icon = GameObject.Instantiate(prefab);
                icon.transform.SetParent(parent, false);
            }
            icon.gameObject.SetActive(true);
            return icon;
        }

        public void ClearCreated()
        {
            if (chartIcons != null)
            {
                foreach (var item in chartIcons)
                {
                    item.gameObject.SetActive(false);
                }
            }
        }
    }
}