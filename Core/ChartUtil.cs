using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace RoundPointGraph
{
    public static class ChartUtil
    {
        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="line_width"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static UIVertex[] GetLine(Vector2 start, Vector2 end,float line_width,Color color)
        {
            Vector2 v1 = end - start;//沿线方向
            Vector2 v2 = new Vector2(v1.y, -v1.x).normalized;//垂直方向
            v2 *= line_width / 2f;
            Vector2[] pos = new Vector2[4];
            pos[0] = start + v2;
            pos[1] = end + v2;
            pos[2] = end - v2;
            pos[3] = start - v2;
            return GetQuad(pos, color);
        }
        /// <summary>
        /// 画框
        /// </summary>
        /// <param name="vertPos"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static UIVertex[] GetQuad(Vector2[] vertPos,Color color)
        {
            UIVertex[] vs = new UIVertex[4];
            Vector2[] uv = new Vector2[4];
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 0);
            uv[3] = new Vector2(1, 1);
            for (int i = 0; i < 4; i++)
            {
                UIVertex v = UIVertex.simpleVert;
                v.color = color;
                v.position = vertPos[i];
                v.uv0 = uv[i];
                vs[i] = v;
            }
            return vs;
        }
    }
}