using UnityEngine;

public class FreeAspectCamera : MonoBehaviour
{
    [Header("Component")]
    public Transform cam;

    [Header("Move")]
    [Tooltip("이동속도")]
    public float moveSpeed = 3f;
    [Tooltip("전진 키")]
    public KeyCode forwardKey = KeyCode.W;
    [Tooltip("후진 키")]
    public KeyCode backKey = KeyCode.S;
    [Tooltip("왼쪽 키")]
    public KeyCode leftKey = KeyCode.A;
    [Tooltip("오른쪽 키")]
    public KeyCode rightKey = KeyCode.D;
    [Tooltip("위 키")]
    public KeyCode upKey = KeyCode.E;
    [Tooltip("아래 키")]
    public KeyCode downKey = KeyCode.Q;
    [Space]
    [Tooltip("고속")]
    public float highSpeed = 2f;
    [Tooltip("고속 키")]
    public KeyCode highSpeedKey = KeyCode.LeftShift;

    [Header("Rotate")]
    [Tooltip("회전속도")]
    public float rotateSpeed = 1f;
    [Tooltip("X축 회전범위")]
    public Vector2 angleRangeX = new Vector2(-89f, 89f);

    [Header("Activation")]
    [Tooltip("활성화/비활성화")]
    public bool activation = true;
    [Tooltip("활성화 여부 변경 키")]
    public KeyCode changeActivationKey = KeyCode.Escape;

    //__________________________________________________________________________ Awake
    private void Awake()
    {
        if (cam == null)
            cam = transform;
    }

    //__________________________________________________________________________ Move & Rotate
    private void Update()
    {
        Activate();
        Move();
        Rotate();
    }

    private void Activate()
    {
        if (Input.GetKeyDown(changeActivationKey))
            activation = !activation;
    }
    private void Move()
    {
        if (!activation) return;

        float speed = Input.GetKey(highSpeedKey) ? highSpeed : moveSpeed;

        Vector3 dir = Vector3.zero;
        if (Input.GetKey(forwardKey))
            dir += cam.forward;
        if (Input.GetKey(backKey))
            dir -= cam.forward;
        if (Input.GetKey(leftKey))
            dir -= cam.right;
        if (Input.GetKey(rightKey))
            dir += cam.right;
        if (Input.GetKey(upKey))
            dir += cam.up;
        if (Input.GetKey(downKey))
            dir -= cam.up;

        cam.position += speed * Time.deltaTime * Vector3.Normalize(dir); //방향벡터의 길이를 1로 계산
    }
    private void Rotate()
    {
        if (!activation) return;

        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        Vector3 camAngles = cam.eulerAngles;
        camAngles.x = camAngles.x > 180f ? camAngles.x - 360f : camAngles.x; //eulerAngles는 음수로 값을 저장하지 않으므로, Inspector와 동일하게 변경

        camAngles.x = Mathf.Clamp(camAngles.x - vertical * rotateSpeed, angleRangeX.x, angleRangeX.y); //X축 범위 제한
        camAngles.y += horizontal * rotateSpeed;

        cam.eulerAngles = camAngles;
    }
}