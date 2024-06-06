using UnityEngine;
namespace VictorDev.InputHandler.Mobile
{
    public static class MobileInputHandler
    {
        private static Touch touchZero;
        private static Touch touchOne;
        private static Vector2 touchZeroPrevPos;
        private static Vector2 touchOnePrevPos;
        private static float prevTouchDeltaMag;
        private static float touchDeltaMag;
        private static float zoomSpeed = 5f;

        /// <summary>
        /// 用兩指縮放
        /// </summary>
        public static float ZoomByFingerScale()
        {
            if (Input.touchCount == 2)
            {
                touchZero = Input.GetTouch(0);
                touchOne = Input.GetTouch(1);

                // 計算兩指之間的距離變化
                touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // 計算縮放差距並應用到攝影機的視野中
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
                return deltaMagnitudeDiff * zoomSpeed * Time.deltaTime;
            }
            return 0;
        }
    }
}