
using UnityEngine;

namespace CardGame
{
    public class CardVisuals : MonoBehaviour
    {
        public Card card;
        public CardVisualProperties[] properties;
        public GameObject statHolder;

        private void Start()
        {
            LoadCard(card);
        }
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
                CardVisualProperties cvp = GetProperties(cp.element);
                if(cvp == null)
                {
                    continue;
                }
                if(cp.element is ElementInt)
                {
                    cvp.text.text = cp.intValue.ToString();
                }
                if (cp.element is ElementImage)
                {
                    cvp.img.sprite = cp.sprite;
                }
                if (cp.element is ElementText)
                {
                    cvp.text.text = cp.stringValue;
                }
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