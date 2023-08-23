using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Runtime.Selection.Managers
{
    using Units;
    using Selectors;
    using Singleton;
    
    public class UnitSelectionManager : MonoBehaviorSingleton<UnitSelectionManager>
    {
        [SerializeField] private GameObject selectionCirclePrefab;

        private Selector[] _selectors;

        private readonly List<Unit> _selectedUnits = new();
                
        private Vector3 _firstMousePos;
        private Vector3 _lastMousePos;

        public IReadOnlyList<Unit> GetSelectedUnits() => _selectedUnits;
        
        protected override void Awake()
        {
            base.Awake();
            
            _selectors = GetComponents<Selector>();
            
            foreach (var selector in _selectors)
                selector.Init(Camera.main);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                _firstMousePos = Input.mousePosition;

            _lastMousePos = Input.mousePosition;

            foreach (var selector in _selectors)
            {
                selector.SelectHandler(_firstMousePos, _lastMousePos);
            }
        }

        public void DeSelectAll(IEnumerable<Unit> units)
        {
            for (var i = _selectedUnits.Count - 1; i >= 0; i--)
            {
                if (!units.Contains(_selectedUnits[i]))
                {
                    DeSelectUnit(_selectedUnits[i]);
                }
            }
        }

        public void SelectAll(IEnumerable<Unit> units)
        {
            foreach (var unit in units)
            {
                if(!_selectedUnits.Contains(unit))
                    SelectUnit(unit);
                else
                    DeSelectUnit(unit);
            }
        }

        public void SelectUnit(Unit unit)
        {
            unit.SetSelection(true, selectionCirclePrefab);
            
            _selectedUnits.Add(unit);
        }

        public void DeSelectUnit(Unit unit)
        {
            unit.SetSelection(false, selectionCirclePrefab);

            _selectedUnits.Remove(unit);
        }

        private void OnGUI()
        {
            foreach (var selector in _selectors)
                selector.DrawGUI(_firstMousePos, Input.mousePosition);
        }
    }
}