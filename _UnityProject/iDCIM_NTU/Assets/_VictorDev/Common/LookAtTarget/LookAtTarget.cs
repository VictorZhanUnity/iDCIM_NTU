using UnityEngine;

namespace VictorDev.Common
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] protected Transform target;

        protected void Update() => ToFaceTarget();

        protected virtual void OnValidate() => ToFaceTarget();

        public void ToFaceTarget()
        {
            if (target != null)
            {
                transform.LookAt(target);
            }
        }
    }
}

