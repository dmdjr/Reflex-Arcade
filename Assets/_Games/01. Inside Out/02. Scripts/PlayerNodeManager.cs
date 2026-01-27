using System.Collections;
using UnityEngine;

public class PlayerNodeManager : MonoBehaviour
{
    private enum NodeState { Left, Right };
    
    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;
    [SerializeField] private Transform[] centerPoints;
    [SerializeField] private float moveSpeed = 10f; // Lerp 특성상 조금 조절 필요

    private NodeState leftNodeState;
    private NodeState rightNodeState;

    private Coroutine leftMoveCoroutine;
    private Coroutine rightMoveCoroutine;

    void Start()
    {
        leftNodeState = NodeState.Left;
        rightNodeState = NodeState.Right;
    }

    // 왼쪽 노드 이동 명령
    public void MoveLeftNode()
    {
        // 이미 이동 중이면 기존 코루틴 중지
        if (leftMoveCoroutine != null) StopCoroutine(leftMoveCoroutine);
        
        // 상태 전환 및 이동 시작
        leftNodeState = (leftNodeState == NodeState.Left) ? NodeState.Right : NodeState.Left;
        Vector3 targetPos = (leftNodeState == NodeState.Left) ? centerPoints[0].position : centerPoints[1].position;
        
        leftMoveCoroutine = StartCoroutine(MoveRoutine(leftNode.transform, targetPos));
    }

    public void MoveRightNode()
    {
        if (rightMoveCoroutine != null) StopCoroutine(rightMoveCoroutine);

        rightNodeState = (rightNodeState == NodeState.Left) ? NodeState.Right : NodeState.Left;
        // 오른쪽 노드용 인덱스 사용 (3번과 2번)
        Vector3 targetPos = (rightNodeState == NodeState.Right) ? centerPoints[3].position : centerPoints[2].position;
        
        rightMoveCoroutine = StartCoroutine(MoveRoutine(rightNode.transform, targetPos));
    }

    // 공용 이동 루틴
    IEnumerator MoveRoutine(Transform objTransform, Vector3 targetPos)
    {
        while (Vector2.Distance(objTransform.position, targetPos) > 0.01f)
        {
            // Time.deltaTime을 곱해 프레임 독립적인 속도 구현
            objTransform.position = Vector3.Lerp(objTransform.position, targetPos, Time.deltaTime * moveSpeed);
            
            // 한 프레임을 쉬고 다음 프레임에 계속 실행 (엔진이 멈추지 않음)
            yield return null; 
        }
        objTransform.position = targetPos;
    }
}