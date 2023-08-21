using Photon.Pun;
using UnityEngine;

namespace Core.Runtime.Units
{
    public abstract class Unit : MonoBehaviourPun
    {
        private Color m_color;
        
        public Color GetColor() => m_color;

        public void SetColor(Color color) => m_color = color;
    }
}