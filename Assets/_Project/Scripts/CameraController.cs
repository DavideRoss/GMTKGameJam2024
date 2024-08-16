using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    public float CameraSpeed = 7.5f;
    public float ZoomSpeed = 7.5f;

    public float ZoomMin = 4.0f;
    public float ZoomMax = 20.0f;

    Transform _cameraTransform;

    bool _lockRotation = false;
    float _rotation = 0.0f;

    const float ANIMATION_TIME = 0.2f;

    private void Start()
    {
        _cameraTransform = transform.GetChild(0);
    }

    private void Update()
    {
        Vector3 movement = new();
        if (Input.GetKey(KeyCode.D)) movement += Vector3.right;
        if (Input.GetKey(KeyCode.A)) movement -= Vector3.right;
        if (Input.GetKey(KeyCode.W)) movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) movement -= Vector3.forward;

        if (!_lockRotation && Input.GetKeyDown(KeyCode.E)) Rotate(-90.0f);
        if (!_lockRotation && Input.GetKeyDown(KeyCode.Q)) Rotate(90.0f);

        transform.position += Quaternion.AngleAxis(_rotation, Vector3.up) * (CameraSpeed * Time.deltaTime * movement);

        float zoom = 0.0f;
        if (Input.GetKey(KeyCode.Z)) zoom += 1.0f;
        if (Input.GetKey(KeyCode.X)) zoom -= 1.0f;

        _cameraTransform.localPosition += Vector3.forward * zoom * ZoomSpeed * Time.deltaTime;
        _cameraTransform.localPosition = new(
            0.0f, 0.0f, Mathf.Clamp(_cameraTransform.localPosition.z, -ZoomMax, -ZoomMin)
        );
    }

    private void Rotate(float rotation)
    {
        _lockRotation = true;
        _rotation += rotation;
        transform.DORotate(new Vector3(0.0f, rotation, 0.0f), ANIMATION_TIME).SetRelative(true).OnComplete(() => _lockRotation = false);
    }
}
