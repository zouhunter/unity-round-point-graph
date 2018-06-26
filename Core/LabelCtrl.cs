using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace RoundPointGraph
{
    public class LabelLayout
    {
        private Transform parent;
        private Color _fontColor = Color.black;
        private int _fontSize;
        private Text prefab;

        private Color fontColor { get { return _fontColor; } set { _fontColor = value; } }
        private int fontSize { get { return _fontSize; } set { _fontSize = value; } }
        private List<Text> textPool = new List<Text>();
        private List<Text> _textArray = new List<Text>();
        public List<Text> textArray { get { return _textArray; } }

        public void SetContext(Transform parent,Text prefab)
        {
            this.parent = parent;
            this.prefab = prefab;
            prefab.gameObject.SetActive(false);
        }

        public void SetLabels(Vector3[] points,float offset)
        {
            HideAllLabels();
            if (points == null || points.Length == 0) return;

            var span = (int)(360 / points.Length);
            for (int i = 0; i < points.Length; i++)
            {
                var text = GetTextFromPool(points[i] + points[i].normalized * offset);
                text.text = string.Format("{0}°", i * span);
            }
        }

        public void HideAllLabels()
        {
            foreach (var item in textPool)
            {
                if(item != null){
                    item.gameObject.SetActive(false);
                }
            }
        }

        private Text GetTextFromPool(Vector3 localPos)
        {
            var text = textPool.Find(x => !x.gameObject.activeSelf);
            if (text == null)
            {
                text = CreateText();
                textPool.Add(text);
            }
            else
            {
                text.gameObject.SetActive(true);
            }
            text.transform.localPosition = localPos;
            return text;
        }

        private Text CreateText()
        {
            var text = GameObject.Instantiate(prefab);
            text.transform.SetParent(parent, false);
            text.gameObject.SetActive(true);
            return text;
        }
    }
}