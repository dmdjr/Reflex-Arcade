using UnityEngine;

public class PlayerNodeManager : MonoBehaviour
{
    private enum NodeState
    {
        Left,
        Right
    };
    
    [SerializeField] private GameObject leftNode;
    [SerializeField] private GameObject rightNode;

    [SerializeField] private NodeState leftNodeState;
    [SerializeField] private NodeState rightNodeState;

    [SerializeField] private Transform[] centerPoints;
    
    void Awake()
    {
        if (leftNode == null)
            leftNode = GameObject.Find("Left Node");
        if (rightNode == null)
            rightNode = GameObject.Find("Right Node");
        
        // null 검증
        if (leftNode == null || rightNode == null)
            Debug.LogError("PlayerNode가 할당되지 않았습니다.");
        
        
    }
    
    void Start()
    {
        // 각 노드를 바깥쪽으로 위치시켜 초기화
        leftNodeState = NodeState.Left;
        leftNode.transform.position = centerPoints[0].position;
        rightNodeState = NodeState.Right;
        rightNode.transform.position = centerPoints[3].position;
    }

    void Update()
    {
        
    }

    public void MoveLeftNode()
    {
        if (leftNodeState == NodeState.Left)
        {
            leftNodeState = NodeState.Right;
            leftNode.transform.position = centerPoints[1].position;
        }
        else if (leftNodeState == NodeState.Right)
        {
            leftNodeState = NodeState.Left;
            leftNode.transform.position = centerPoints[0].position;
        }
    }
    
    public void MoveRightNode()
    {
        if (rightNodeState == NodeState.Left)
        {
            rightNodeState = NodeState.Right;
            rightNode.transform.position = centerPoints[3].position;
        }
        else if (rightNodeState == NodeState.Right)
        {
            rightNodeState = NodeState.Left;
            rightNode.transform.position = centerPoints[2].position;
        }
    }
}
