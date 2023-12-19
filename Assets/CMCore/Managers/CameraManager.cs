using CMCore.Contracts;
using CMCore.Utilities.Extensions;
using UnityEngine;


namespace CMCore.Managers
{
    public class CameraManager
    {
        
        #region Properties & Fields

        public Camera MainCamera => _mainCamera == null
            ? _mainCamera = GameObject.Find("Game Camera(Main)").Get<Camera>()
            : _mainCamera;

        private Camera _mainCamera;

        public Camera UICamera =>
            _uiCamera == null ? _uiCamera = GameObject.Find("UI Camera").Get<Camera>() : _uiCamera;

        private Camera _uiCamera;

        public Transform MainCameraTransform => _mainCameraTransform == null
            ? _mainCameraTransform = _mainCamera.transform
            : _mainCameraTransform;

        private Transform _mainCameraTransform;
        public Transform UICameraTransform => _uiCameraTransform == null
            ? _uiCameraTransform = _uiCamera.transform
            : _uiCameraTransform;
        private Transform _uiCameraTransform;
        #endregion

        public CameraManager()
        {
        }
        public void SetPosition(Vector3 position)
        {
            MainCameraTransform.position = position;
        }

        public void SetOrthographicSize(float size)
        {
            MainCamera.orthographicSize = size;
        }


        ////////////////////////////////////////////////////////////////////
    }
}