using UnityEngine;

public class FreeAspectCamera : MonoBehaviour
{
    [Header("Component")]
    public Transform cam;

    [Header("Move")]
    [Tooltip("�̵��ӵ�")]
    public float moveSpeed = 3f;
    [Tooltip("���� Ű")]
    public KeyCode forwardKey = KeyCode.W;
    [Tooltip("���� Ű")]
    public KeyCode backKey = KeyCode.S;
    [Tooltip("���� Ű")]
    public KeyCode leftKey = KeyCode.A;
    [Tooltip("������ Ű")]
    public KeyCode rightKey = KeyCode.D;
    [Tooltip("�� Ű")]
    public KeyCode upKey = KeyCode.E;
    [Tooltip("�Ʒ� Ű")]
    public KeyCode downKey = KeyCode.Q;
    [Space]
    [Tooltip("���")]
    public float highSpeed = 2f;
    [Tooltip("��� Ű")]
    public KeyCode highSpeedKey = KeyCode.LeftShift;

    [Header("Rotate")]
    [Tooltip("ȸ���ӵ�")]
    public float rotateSpeed = 1f;
    [Tooltip("X�� ȸ������")]
    public Vector2 angleRangeX = new Vector2(-89f, 89f);

    [Header("Activation")]
    [Tooltip("Ȱ��ȭ/��Ȱ��ȭ")]
    public bool activation = true;
    [Tooltip("Ȱ��ȭ ���� ���� Ű")]
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

        cam.position += speed * Time.deltaTime * Vector3.Normalize(dir); //���⺤���� ���̸� 1�� ���
    }
    private void Rotate()
    {
        if (!activation) return;

        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        Vector3 camAngles = cam.eulerAngles;
        camAngles.x = camAngles.x > 180f ? camAngles.x - 360f : camAngles.x; //eulerAngles�� ������ ���� �������� �����Ƿ�, Inspector�� �����ϰ� ����

        camAngles.x = Mathf.Clamp(camAngles.x - vertical * rotateSpeed, angleRangeX.x, angleRangeX.y); //X�� ���� ����
        camAngles.y += horizontal * rotateSpeed;

        cam.eulerAngles = camAngles;
    }
}