using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CardGame
{
    [Serializable]
    public class CardProperties
    {
        public string value;
        public int intValue;
        public Sprite sprite;
        public Element element;
    }
}