using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RoundPointGraph
{
    [RequireComponent(typeof(RectTransform))]
    public class RoundChart : Graphic
    {
        [SerializeField]
        protected RP_Info _graphInfo;
        [SerializeField]
        protected Text label_prefab;
        protected Vector3[] conners = new Vector3[4];
        protected List<RP_Line> lines = new List<RP_Line>();
        protected LabelLayout labelLayout = new LabelLayout();
        protected float Radius
        {
            get
            {
                return (conners[2].x - conners[1].x) * 0.5f;
            }
        }

        public Vector3[] radialPoints { get; private set; }
        public RP_Info graphInfo { get { return _graphInfo; } }
        protected override void Start()
        {
            labelLayout.SetContext(transform, label_prefab);
            SetAllDirty();
        }
        protected override void OnValidate()
        {
            base.OnValidate();
            SetAllDirty();
        }
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            rectTransform.GetLocalCorners(conners);
            if (Radius > 0)
            {
                vh.Clear();
                DrawCurcles(vh);
                DrawRadialLines(vh);
                DrawCustomLines(vh);
            }
        }

        public void DisplyText(float offset)
        {
            labelLayout.SetLabels(radialPoints, offset);
        }

        /// <summary>
        /// 清空所有线条
        /// </summary>
        public void ClearLines()
        {
            foreach (var line in lines)
            {
                line.ClearCreated();
            }
            lines.Clear();
            SetAllDirty();
        }

        /// <summary>
        /// 设置显示点
        /// </summary>
        /// <param name="positions"></param>
        public GameObject[] CreatePoints(string lineName, RP_Pos[] positions, GameObject prefab, RP_LineInfo lineInfo = null)
        {
            GameObject[] created = null;
            var oldLine = lines.Find(x => x.lineName == lineName);
            if (oldLine != null)
            {
                oldLine.UpdateLineInfoSelfty(lineInfo);
                oldLine.startAngle = graphInfo.startAngle;
                oldLine.clockwise = graphInfo.clockwise;
                created = oldLine.ResetPoints(prefab, Radius, positions);
            }
            else
            {
                var line = new RP_Line(lineName, transform);
                line.startAngle = graphInfo.startAngle;
                line.clockwise = graphInfo.clockwise;
                line.UpdateLineInfoSelfty(lineInfo);
                lines.Add(line);

                created = line.ResetPoints(prefab, Radius, positions);
            }
            SetAllDirty();
            return created;
        }

        /// <summary>
        /// 绘制用户自定义线
        /// </summary>
        /// <param name="vh"></param>
        protected virtual void DrawCustomLines(VertexHelper vh)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var points = lines[i].positions;
                for (int j = 0; j < points.Length - 1; j++)
                {
                    var p1 = points[j];
                    var p2 = points[j + 1];
                    vh.AddUIVertexQuad(ChartUtil.GetLine(p1, p2, line.lineInfo.width, line.lineInfo.color * color));
                }
                if (points.Length > 2)
                {
                    var p1 = points[0];
                    var p2 = points[points.Length - 1];
                    vh.AddUIVertexQuad(ChartUtil.GetLine(p1, p2, line.lineInfo.width, line.lineInfo.color * color));
                }
            }
        }

        /// <summary>
        /// 绘制相贯线
        /// </summary>
        protected virtual void DrawRadialLines(VertexHelper vh)
        {
            int radialLineCount = Mathf.FloorToInt(360 / graphInfo.angleSpan);
            radialPoints = new Vector3[radialLineCount];
            for (int i = 0; i < radialLineCount; i++)
            {
                var angle = (i / (radialLineCount + 0f)) * 360;
                angle += graphInfo.startAngle;
                if (graphInfo.clockwise)
                {
                    angle = -angle;
                }
                radialPoints[i] = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * Radius;
                vh.AddUIVertexQuad(ChartUtil.GetLine(Vector3.zero, radialPoints[i], graphInfo.angleLineInfo.width, graphInfo.angleLineInfo.color * color));
            }
        }

        /// <summary>
        /// 绘制圆环
        /// </summary>
        protected virtual void DrawCurcles(VertexHelper vh)
        {
            for (int i = 1; i <= graphInfo.radialCount; i++)
            {
                var radius = Radius * i / (graphInfo.radialCount + 0f);
                var points = GetCurclePoints(radius, graphInfo.accuracy);
                for (int j = 0; j < points.Length - 1; j++)
                {
                    var p1 = points[j];
                    var p2 = points[j + 1];
                    vh.AddUIVertexQuad(ChartUtil.GetLine(p1, p2, graphInfo.radialLineInfo.width, graphInfo.radialLineInfo.color * color));
                }
            }
        }

        /// <summary>
        /// 计算得到一个圆上的点列表
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="accuracy"></param>
        /// <returns></returns>
        protected Vector2[] GetCurclePoints(float radius, int accuracy)
        {
            if (accuracy == 0) return new Vector2[0];
            var points = new Vector2[accuracy + 1];
            int i = 0;
            for (; i < accuracy; i++)
            {
                var angle = (i / (accuracy + 0f)) * 2 * Mathf.PI;
                points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
            }
            points[i] = points[0];
            return points;
        }

    }
}