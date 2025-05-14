/*using System.Collections.Generic;
using UnityEngine;

public class GroupMover : MonoBehaviour
{
    //工具类 for 可移动blocks
    [Header("可碰撞层（包括所有其他障碍和未拾取的方块）")]
    public LayerMask obstacleLayer;
    [Header("跟随玩家一起移动的方块列表（会动态 Add/Remove）")]
    public List<Transform> pickedBlocks = new List<Transform>();
    [Header("移动参数")]
    public float speed = 5f;
    public float smoothTime = 0.1f;

    private Vector2 _velocity;
    private Vector2 _velRef;

    // 缓存所有要动的 Collider2D
    private List<BoxCollider2D> _colliders = new List<BoxCollider2D>();

    void Awake()
    {
        // 玩家自己也算在组里
        _colliders.Add(GetComponent<BoxCollider2D>());
    }

    // 当拾取一个方块时调用
    public void PickUp(Transform block)
    {
        if (!pickedBlocks.Contains(block))
        {
            pickedBlocks.Add(block);
            _colliders.Add(block.GetComponent<BoxCollider2D>());
        }
    }

    // 当放下一个方块时调用
    public void Drop(Transform block)
    {
        if (pickedBlocks.Remove(block))
            _colliders.Remove(block.GetComponent<BoxCollider2D>());
    }

    void Update()
    {
        // 1) 计算组整体的目标速度
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) input += Vector2.up;
        if (Input.GetKey(KeyCode.S)) input += Vector2.down;
        if (Input.GetKey(KeyCode.A)) input += Vector2.left;
        if (Input.GetKey(KeyCode.D)) input += Vector2.right;
        if (input.sqrMagnitude > 1f) input.Normalize();

        Vector2 targetVel = input * speed;
        _velocity = Vector2.SmoothDamp(_velocity, targetVel, ref _velRef, smoothTime);

        // 2) 分轴检测碰撞并移动
        Vector2 delta = _velocity * Time.deltaTime;
        MoveAxis(delta.x, Vector2.right * Mathf.Sign(delta.x));
        MoveAxis(delta.y, Vector2.up * Mathf.Sign(delta.y));
    }

    void MoveAxis(float dist, Vector2 dir)
    {
        if (Mathf.Approximately(dist, 0f)) return;

        float absDist = Mathf.Abs(dist);
        // 对组中每个 collider 做 BoxCast 检测
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
                // 只要任何一个被阻挡，就停止整个组该方向移动
                if (dir.x != 0) _velocity.x = 0;
                if (dir.y != 0) _velocity.y = 0;
                return;
            }
        }
        // 如果都没阻挡，就移动玩家和所有 pickedBlocks
        Vector3 move = (Vector3)(dir.normalized * absDist);
        // 玩家
        transform.position += move;
        // 以及被拾取的方块
        foreach (var b in pickedBlocks)
            b.position += move;
    }
}
*/
using System.Collections.Generic;
using UnityEngine;

public class GroupMover : MonoBehaviour
{
    [Header("玩家 Transform")]
    public Transform playerTransform;

    [Header("跟随玩家一起移动的方块列表")]
    public List<Transform> pickedBlocks = new List<Transform>();

    // 存储每个被拾取方块相对于玩家的起始偏移量
    private Dictionary<Transform, Vector3> _offsets = new Dictionary<Transform, Vector3>();

    void LateUpdate()
    {
        // 每帧结束后同步所有被拾取方块的位置
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
