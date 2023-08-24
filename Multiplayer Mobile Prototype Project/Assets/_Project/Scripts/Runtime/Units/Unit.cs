using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Runtime.Units
{
    using Managers;
    
    public abstract class Unit : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Color m_color;
        [SerializeField] private NavMeshAgent _agent;

        private GameObject _selectionCircle;
        
        private bool _isSelected;

        public NavMeshAgent GetAgent() => _agent;
        
        public Color GetColor() => m_color;

        public void SetColor(Color color)
        {
            photonView.RPC("RPC_SetColor", RpcTarget.AllBuffered,
                color.r, color.g, color.b);
        }

        [PunRPC]
        protected void RPC_SetColor(float r, float g, float b)
        {
            m_color = new Color(r, g, b);
        }

        public void SetSelection(bool selected, GameObject selectionCirclePrefab)
        {
            if ((_isSelected && selected) || (!_isSelected && !selected)) return;

            _isSelected = selected;

            if (selected)
                _selectionCircle = Instantiate(selectionCirclePrefab, transform, false);
            else Destroy(_selectionCircle);

        }

        protected virtual void Awake()
        {
            UnitManager.Instance.AddUnit(this);
        }

        protected virtual void OnDestroy()
        {
            UnitManager.Instance.RemoveUnit(this);
        }
    }
}