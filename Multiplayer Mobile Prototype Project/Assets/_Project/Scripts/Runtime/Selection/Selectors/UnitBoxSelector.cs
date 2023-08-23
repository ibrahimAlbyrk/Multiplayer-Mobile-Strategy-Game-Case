using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Core.Runtime.Selection.Managers;

namespace Core.Runtime.Selection.Selectors
{
    using Units;
    using Units.Managers;
    
    public class UnitBoxSelector : Selector
    {
        private bool _isSelecting;

        public override void DrawGUI(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            if (!_isSelecting) return;
            
            // Create a rect from both mouse positions
            var rect = GetScreenRect(firsMousePos, lastMousePos);
            DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
        }

        public override void SelectHandler(Vector3 firsMousePos, Vector3 lastMousePos)
        {
            base.SelectHandler(firsMousePos, lastMousePos);

            _isSelecting = Input.GetMouseButton(0);

            if ((firsMousePos - lastMousePos).magnitude < 60)
            {
                _isSelecting = false;
                return;
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                var selectedUnits = TrySelect(m_firsMousePos, m_lastMousePos);
                
                SelectionControl(selectedUnits);
            }
        }

        public override void SelectionControl(IEnumerable<Unit> units)
        {
            if (!units.Any()) return;

            var selectedUnits = UnitSelectionManager.Instance.GetSelectedUnits().ToList();
            
            // Deselect units which are not in the incoming units list
            for (var i = selectedUnits.Count - 1; i >= 0; i--)
            {
                if (!units.Contains(selectedUnits[i]))
                {
                    UnitSelectionManager.Instance.DeSelectUnit(selectedUnits[i]);
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
            var selectedUnits = new List<Unit>();
            
            for (var i = 0; i < UnitManager.Instance.GetUnits().Count; i++)
            {
                var unit = UnitManager.Instance.GetUnits()[i];

                var isSelected = IsWithinSelectionBounds(unit.gameObject);
                
                if(!isSelected) continue;

                selectedUnits.Add(unit);
            }

            return selectedUnits;
        }
        
        private bool IsWithinSelectionBounds(GameObject obj)
        {
            var viewportBounds =
                GetViewportBounds(m_cam, m_firsMousePos, m_lastMousePos);

            return m_cam != null && viewportBounds.Contains(
                m_cam.WorldToViewportPoint(obj.transform.position));
        }
        
        private static void DrawScreenRect(Rect rect, Color color)
        {
            GUI.color = color;
            GUI.DrawTexture(rect, Texture2D.whiteTexture);
            GUI.color = Color.white;
        }

        private static Rect GetScreenRect(Vector3 screenPosition1, Vector3 screenPosition2)
        {
            // Convert to coordinates from bottom left.
            screenPosition1.y = Screen.height - screenPosition1.y;
            screenPosition2.y = Screen.height - screenPosition2.y;

            // Get top left and bottom right points, based on the starting and end positions.
            var topLeft = Vector3.Min(screenPosition1, screenPosition2);
            var bottomRight = Vector3.Max(screenPosition1, screenPosition2);

            // Create rect from top left and bottom right points.
            return Rect.MinMaxRect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y);
        }

        private static Bounds GetViewportBounds(Camera camera, Vector3 screenPosition1, Vector3 screenPosition2)
        {
            if (camera == null) return default;

            var v1 = camera.ScreenToViewportPoint(screenPosition1);
            var v2 = camera.ScreenToViewportPoint(screenPosition2);
            var min = Vector3.Min(v1, v2);
            var max = Vector3.Max(v1, v2);
            min.z = camera.nearClipPlane;
            max.z = camera.farClipPlane;

            var bounds = new Bounds();
            bounds.SetMinMax(min, max);
            return bounds;
        }
    }
}