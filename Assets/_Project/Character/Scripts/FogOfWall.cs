using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FOW;

namespace Character
{
    public class FogOfWall : MonoBehaviour
    {
        private FogOfWarRevealer2D _revealer2D;
        [SerializeField] private bool _isLightMode;

        private void Start()
        {
            _revealer2D = GetComponent<FogOfWarRevealer2D>();
            if (_revealer2D == null)
            {
                _revealer2D = gameObject.AddComponent<FogOfWarRevealer2D>();
            }

            SetMode(_isLightMode);
        }



        #region Public Methods 
        public void SetMode(bool isLightMode = false)
        {
            _revealer2D.ViewAngle = isLightMode ? 70 : 360;
        }

        public void UpdateRotation(Vector2 direction)
        {

            if (!_isLightMode || direction == Vector2.zero)
                return;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.localRotation = Quaternion.Euler(0 ,0, angle);
        }
        #endregion
    }
}

