using UnityEngine;

namespace VictorDev.Common
{
    public class LookAtMainCamera : LookAtTarget
    {
        private void Start() => OnValidate();

        override protected void OnValidate()
        {
            if (Camera.main != null) target = Camera.main.transform;
            base.OnValidate();
        }
    }
}

