using UnityEngine;

public class OrbitController : MonoBehaviour
{
    public enum ControlSide { Left, Right }

    [SerializeField] private TouchInputHandler inputHandler;

    [SerializeField] private ControlSide controlSide;
    [SerializeField] private float rotateSpeed = 200f;
    [SerializeField] private bool rotateClockwise = true;

    private void Update()
    {
        if (inputHandler == null) return;

        bool isPressed = (controlSide == ControlSide.Left)
            ? inputHandler.IsLeftPressed
            : inputHandler.IsRightPressed;

        if (isPressed)
        {
            RotateOrbit();
        }
    }

    private void RotateOrbit()
    {
        float direction = rotateClockwise ? -1f : 1f;
        transform.Rotate(0, 0, direction * rotateSpeed * Time.deltaTime);
    }
}
