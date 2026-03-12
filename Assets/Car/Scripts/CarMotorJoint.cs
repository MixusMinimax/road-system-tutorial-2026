using UnityEngine;

namespace Car.Scripts
{
    [RequireComponent(typeof(ConfigurableJoint))]
    [DefaultExecutionOrder(10)]
    public class CarMotorJoint : MonoBehaviour
    {
        private ConfigurableJoint _joint;

        [Range(0, 1)] public float throttle;
        [Range(0, 1)] public float braking;

        public bool reverse;
        public float forwardMaxSpeed = 300;
        public float backwardMaxSpeed = 100;
        public float maxTorque = 5000;

        private void Start()
        {
            _joint = GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            throttle = Mathf.Clamp01(throttle);
            braking = Mathf.Clamp01(braking);
            var targetSpeed = throttle - braking;
            var torque = Mathf.Abs(targetSpeed) * maxTorque;
            targetSpeed *= targetSpeed > 0 ? forwardMaxSpeed : backwardMaxSpeed;
            _joint.targetAngularVelocity = new Vector3(reverse ? -targetSpeed : targetSpeed, 0, 0);
            var drive = _joint.angularXDrive;
            drive.maximumForce = torque;
            _joint.angularXDrive = drive;
        }
    }
}