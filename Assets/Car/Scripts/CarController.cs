using UnityEngine;
using UnityEngine.InputSystem;

namespace Car.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    [DefaultExecutionOrder(0)]
    public class CarController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private CarSteeringJoint[] _steeringJoints;
        private CarMotorJoint[] _motorJoints;

        private InputAction _steerAction;
        private InputAction _throttleAction;
        private InputAction _brakingAction;

        public float steeringReduction = 0.1f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _steeringJoints = GetComponentsInChildren<CarSteeringJoint>();
            _motorJoints = GetComponentsInChildren<CarMotorJoint>();
            _steerAction = InputSystem.actions.FindAction("Steer");
            _throttleAction = InputSystem.actions.FindAction("Throttle");
            _brakingAction = InputSystem.actions.FindAction("Braking");

            _rigidbody.solverIterations = 15;
        }

        private void FixedUpdate()
        {
            var speed = _rigidbody.linearVelocity.magnitude;
            var maxSteerAngle = 1f / (1f + speed * steeringReduction);

            var steer = _steerAction.ReadValue<float>() * maxSteerAngle;
            foreach (var joint in _steeringJoints)
                joint.steer = steer;
            var throttle = _throttleAction.ReadValue<float>();
            var braking = _brakingAction.ReadValue<float>();
            foreach (var joint in _motorJoints)
            {
                joint.throttle = throttle;
                joint.braking = braking;
            }
        }
    }
}