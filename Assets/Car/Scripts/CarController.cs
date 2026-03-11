using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Car.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private CarSteeringJoint[] _steeringJoints;

        private InputAction _steerAction;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _steeringJoints = GetComponentsInChildren<CarSteeringJoint>();
            _steerAction = InputSystem.actions.FindAction("Steer");

            _rigidbody.solverIterations = 15;
        }

        // Update is called once per frame
        private void Update()
        {
            var steer = _steerAction.ReadValue<float>();

            foreach (var joint in _steeringJoints)
                joint.steer = steer;
        }
    }
}