/*using System.Collections.Generic;
using UnityEngine;

public class GroupMover : MonoBehaviour
{
    //������ for ���ƶ�blocks
    [Header("����ײ�㣨�������������ϰ���δʰȡ�ķ��飩")]
    public LayerMask obstacleLayer;
    [Header("�������һ���ƶ��ķ����б��ᶯ̬ Add/Remove��")]
    public List<Transform> pickedBlocks = new List<Transform>();
    [Header("�ƶ�����")]
    public float speed = 5f;
    public float smoothTime = 0.1f;

    private Vector2 _velocity;
    private Vector2 _velRef;

    // ��������Ҫ���� Collider2D
    private List<BoxCollider2D> _colliders = new List<BoxCollider2D>();

    void Awake()
    {
        // ����Լ�Ҳ��������
        _colliders.Add(GetComponent<BoxCollider2D>());
    }

    // ��ʰȡһ������ʱ����
    public void PickUp(Transform block)
    {
        if (!pickedBlocks.Contains(block))
        {
            pickedBlocks.Add(block);
            _colliders.Add(block.GetComponent<BoxCollider2D>());
        }
    }

    // ������һ������ʱ����
    public void Drop(Transform block)
    {
        if (pickedBlocks.Remove(block))
            _colliders.Remove(block.GetComponent<BoxCollider2D>());
    }

    void Update()
    {
        // 1) �����������Ŀ���ٶ�
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) input += Vector2.up;
        if (Input.GetKey(KeyCode.S)) input += Vector2.down;
        if (Input.GetKey(KeyCode.A)) input += Vector2.left;
        if (Input.GetKey(KeyCode.D)) input += Vector2.right;
        if (input.sqrMagnitude > 1f) input.Normalize();

        Vector2 targetVel = input * speed;
        _velocity = Vector2.SmoothDamp(_velocity, targetVel, ref _velRef, smoothTime);

        // 2) ��������ײ���ƶ�
        Vector2 delta = _velocity * Time.deltaTime;
        MoveAxis(delta.x, Vector2.right * Mathf.Sign(delta.x));
        MoveAxis(delta.y, Vector2.up * Mathf.Sign(delta.y));
    }

    void MoveAxis(float dist, Vector2 dir)
    {
        if (Mathf.Approximately(dist, 0f)) return;

        float absDist = Mathf.Abs(dist);
        // ������ÿ�� collider �� BoxCast ���
        foreach (var col in _colliders)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                col.bounds.center,
                col.bounds.size,
                0f,
                dir,
                absDist,
                obstacleLayer
            );
            if (hit.collider != null)
            {
                // ֻҪ�κ�һ�����赲����ֹͣ������÷����ƶ�
                if (dir.x != 0) _velocity.x = 0;
                if (dir.y != 0) _velocity.y = 0;
                return;
            }
        }
        // �����û�赲�����ƶ���Һ����� pickedBlocks
        Vector3 move = (Vector3)(dir.normalized * absDist);
        // ���
        transform.position += move;
        // �Լ���ʰȡ�ķ���
        foreach (var b in pickedBlocks)
            b.position += move;
    }
}
*/
using System.Collections.Generic;
using UnityEngine;

public class GroupMover : MonoBehaviour
{
    [Header("��� Transform")]
    public Transform playerTransform;

    [Header("�������һ���ƶ��ķ����б�")]
    public List<Transform> pickedBlocks = new List<Transform>();

    // �洢ÿ����ʰȡ�����������ҵ���ʼƫ����
    private Dictionary<Transform, Vector3> _offsets = new Dictionary<Transform, Vector3>();

    void LateUpdate()
    {
        // ÿ֡������ͬ�����б�ʰȡ�����λ��
        foreach (var block in pickedBlocks)
        {
            if (_offsets.TryGetValue(block, out var offset))
            {
                block.position = playerTransform.position + offset;
            }
        }
    }

    public void PickUp(Transform block)
    {
        if (pickedBlocks.Contains(block)) return;
        pickedBlocks.Add(block);
        _offsets[block] = block.position - playerTransform.position;
    }

   
    public void Drop(Transform block)
    {
        if (!pickedBlocks.Remove(block)) return;
        _offsets.Remove(block);
    }
}
