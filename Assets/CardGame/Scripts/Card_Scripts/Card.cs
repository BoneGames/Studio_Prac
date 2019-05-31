using UnityEngine;
using System;

namespace CardGame
{
    [CreateAssetMenu(menuName = "Card", fileName = "New Card")]
    public class Card : ScriptableObject
    {
        public CardType cardType;
        public CardProperties[] properties;
    }
}
