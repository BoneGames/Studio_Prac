
using UnityEngine;

namespace CardGame
{
    public class CardVisuals
    {
        public Card card;
        public CardVisualProperties[] properties;
        public GameObject statHolder;
        public void LoadCard(Card c)
        {
            if(c == null)
            {
                return;
            }
            card = c;
            //c.cardType.OnSetType(this);

            for (int i = 0; i < c.properties.Length; i++)
            {
                CardProperties cp = c.properties[i];
            }
        }

        // get elemental property
        public CardVisualProperties GetProperties(Element e)
        {
            CardVisualProperties result = null;
            for (int i = 0; i < properties.Length; i++)
            {
                if(properties[i].element == e)
                {
                    result = properties[i];
                    break;
                }
            }
            return result;
        }
    }
}