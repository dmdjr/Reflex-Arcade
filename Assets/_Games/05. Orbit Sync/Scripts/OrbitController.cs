using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public enum ControlSide { Left, Right }

    [SerializeField] private TouchInputHandler inputHandler;

    [SerializeField] private ControlSide controlSide;

    [SerializeField] private float activeRotateSpeed = 200f;
    [SerializeField] private float idleRotateSpeed = 100f;

    [SerializeField] private bool activeClockwise = true;

    private void Update()
    {
        if (OrbitSyncManager.Instance != null && !OrbitSyncManager.Instance.IsGameRunning)
            return;

        if (inputHandler == null) return;

        bool isPressed = (controlSide == ControlSide.Left)
            ? inputHandler.IsLeftPressed
            : inputHandler.IsRightPressed;

        float currentSpeed = isPressed ? activeRotateSpeed : idleRotateSpeed;

        bool currentIsClockwise = isPressed ? activeClockwise : !activeClockwise;

        RotateOrbit(currentIsClockwise, currentSpeed);
    }

    private void RotateOrbit(bool isClockwise, float speed)
    {
        float direction = isClockwise ? -1f : 1f;
        transform.Rotate(0, 0, direction * speed * Time.deltaTime);
    }
}
