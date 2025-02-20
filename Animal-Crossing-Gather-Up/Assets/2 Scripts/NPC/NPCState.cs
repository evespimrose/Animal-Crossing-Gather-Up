using UnityEngine;
using System.Collections.Generic;

public enum NPCStateType
{
    Idle,
    LookAround,
    Walk,
    Talk
}

public abstract class NPCState : MonoBehaviour, INPCState
{
    protected Animator anim;

    [SerializeField] private float rotateSpeed = 1.5f;
    [SerializeField] private float rotateToPlayerSpeed = 5f;
    protected float moveSpeed = 2f; // 기본 이동 속도

    protected IDialogState dialogState;
    protected NPCStateType npcState;
    protected Vector3 currentTarget;
    protected Quaternion originalRotation;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        dialogState = GetComponent<IDialogState>();
        npcState = NPCStateType.Idle; // 기본 상태 설정
    }

    protected virtual void Update()
    {
        switch (npcState)
        {
            case NPCStateType.Idle:
                HandleIdle();
                break;
            case NPCStateType.LookAround:
                HandleLookAround();
                break;
            case NPCStateType.Walk:
                HandleWalk();
                break;
            case NPCStateType.Talk:
                HandleTalk();
                break;
        }
    }

    private void HandleIdle()
    {
        PlayAnimation("Idle");
    }

    private void HandleLookAround()
    {
        PlayAnimation("ChangeLook");
    }

    private void HandleWalk()
    {
        Wander();
    }

    protected virtual void HandleTalk()
    {
        PlayAnimation("Talk");
        RotateTowardsPlayer();
        ManageDialogState();
    }

    private void PlayAnimation(string animationName)
    {
        anim.Play(animationName);
        anim.SetFloat("Speed", 0f);
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (GameManager.Instance.player.transform.position - transform.position).normalized;
        direction.y = 0f; // y축 무시

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateToPlayerSpeed * Time.deltaTime);
        }
    }

    private void ManageDialogState()
    {
        if (dialogState == null) return;

        if (dialogState.currentCoroutine != null && UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            anim.SetBool("Talk", true);
        }
        else if (dialogState.currentCoroutine == null && UIManager.Instance.dialogUI.dialogPanel.activeSelf)
        {
            anim.SetBool("Talk", false);
        }
    }

    public void Wander()
    {
        if (currentTarget == Vector3.zero || Vector3.Distance(transform.position, currentTarget) < 0.5f)
        {
            if (Vector3.Distance(transform.position, currentTarget) < 6f)
            {
                currentTarget = RandomWaypoint();
            }
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (currentTarget - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime);
        UpdateAnimationSpeed();
    }

    private void UpdateAnimationSpeed()
    {
        float currentSpeed = Vector3.Distance(transform.position, currentTarget) > 0.1f ? 0.3f : 0.05f;
        anim.SetFloat("Speed", currentSpeed);
    }

    protected abstract Vector3 RandomWaypoint();

    public void SetCurrentState(NPCStateType newState)
    {
        npcState = newState;
    }
    
    protected void HandleRotationBackToOriginal()
    {
        anim.SetBool("Talk", false);
        transform.rotation = Quaternion.Slerp(transform.rotation, originalRotation, rotateToPlayerSpeed * Time.deltaTime);
        if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
        {
            SetCurrentState(NPCStateType.LookAround);
        }
    }

    protected Vector3 GetRandomWaypoint(float minX, float maxX, float minZ, float maxZ, float y)
    {
        float x = Random.Range(minX, maxX);
        float z = Random.Range(minZ, maxZ);
        return new Vector3(x, y, z);
    }
}