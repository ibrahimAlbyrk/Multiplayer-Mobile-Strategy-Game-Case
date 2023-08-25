using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Core.Runtime.NETWORK;
using Core.Runtime.Units.Managers;

namespace Core.Runtime.Selection.Selectors
{
    using Units;
    using Managers;
    
    public class UnitMultipleSelector : Selector
    {
        private bool _isMultiple;
        
        public override void SelectHandler(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            base.SelectHandler(firsMousePos, lastMousePos);

            if (Input.GetMouseButtonDown(0))
            {
                var selectedUnits = TrySelect(Input.mousePosition, default);
                
                SelectionControl(selectedUnits);
            }

            _isMultiple = Input.GetKey(KeyCode.LeftShift);
        }

        public override void SelectionControl(IEnumerable<Unit> units)
        {
            if (!units.Any()) return;

            var selectedUnits = UnitSelectionManager.Instance.GetSelectedUnits().ToList();
            
            if(!_isMultiple)
            {
                // Deselect units which are not in the incoming units list
                for (var i = selectedUnits.Count - 1; i >= 0; i--)
                {
                    if (!units.Contains(selectedUnits[i]))
                    {
                        UnitSelectionManager.Instance.DeSelectUnit(selectedUnits[i]);
                    }
                }
            }
            
            // Select all incoming units
            foreach (var unit in units)
            {
                UnitSelectionManager.Instance.SelectUnit(unit);
            }
        }

        public override IEnumerable<Unit> TrySelect(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            var ray = m_cam.ScreenPointToRay(firsMousePos);

            var unitLayerName = UnitManager.Instance.GetUnitLayer();

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, LayerMask.GetMask(unitLayerName)))
                return Enumerable.Empty<Unit>();

            if (!hit.collider.TryGetComponent(out Unit unit))
                return Enumerable.Empty<Unit>();
            
            if (unit.GetColor() != LocalPlayer.GetColor())
                return Enumerable.Empty<Unit>();;

            return new[] { unit };
        }
    }
}