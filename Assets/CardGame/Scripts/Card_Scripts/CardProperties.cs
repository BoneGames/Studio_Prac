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
        public int intValue;
        public string stringValue;
        public Sprite sprite;
        public Element element;
    }
}