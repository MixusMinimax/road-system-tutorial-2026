using UnityEngine;

namespace Car.Scripts
{
    [RequireComponent(typeof(ConfigurableJoint))]
    public class CarSteeringJoint : MonoBehaviour
    {
        private ConfigurableJoint _joint;

        [Range(-1, 1)] public float steer;

        public bool invert = true;

        private float _limit;

        private void Start()
        {
            _joint = GetComponent<ConfigurableJoint>();
            _limit = _joint.angularYLimit.limit;
        }

        private void Update()
        {
            steer = Mathf.Clamp(steer, -1, 1);
            _joint.targetRotation = Quaternion.Euler(0, (invert ? -steer : steer) * _limit, 0);
        }
    }
}