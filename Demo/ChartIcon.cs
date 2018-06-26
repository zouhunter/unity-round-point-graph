using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RoundPointGraph
{
    public class ChartIcon : MonoBehaviour, IPointerEnterHandler
    {
        public string iconInfo { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log(iconInfo);
        }
    }
}