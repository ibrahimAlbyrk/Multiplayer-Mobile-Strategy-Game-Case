using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Selection.Selectors
{
    using Units;
    
    public abstract class Selector : MonoBehaviour
    {
        protected Vector3 m_firsMousePos;
        protected Vector3 m_lastMousePos;

        protected Camera m_cam;

        public void Init(Camera cam)
        {
            m_cam = cam;
        }

        public virtual void DrawGUI(Vector3 firsMousePos, Vector3 lastMousePos){}

        public virtual void SelectHandler(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            m_firsMousePos = firsMousePos;
            m_lastMousePos = lastMousePos;   
        }

        public abstract void SelectionControl(IEnumerable<Unit> units);
        
        public virtual IEnumerable<Unit> TrySelect(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            return Enumerable.Empty<Unit>();
        }
    }
}