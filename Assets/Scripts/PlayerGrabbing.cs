using System.Collections.Generic;
using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{
    [Header("方块所在的 Layer")]
    [SerializeField] private LayerMask blockLayer;
    [Header("玩家对应的 GridObject")]
    [SerializeField] private GridObject playerGridObject;

    private Camera _cam;
    private bool _isDragging;
    private Vector2 _lastWorldPos;
    private Vector2Int _startGrid;
    private HashSet<GridObject> _selected = new HashSet<GridObject>();
    private GroupMover _groupMover;

    void Awake()
    {
        _cam = Camera.main;
        _groupMover = GetComponent<GroupMover>();
    }

    void Update()
    {
        // 1) 优先处理放下：如果不在拖拽且已有方块被拾取，点击就放下所有
        if (!_isDragging && Input.GetMouseButtonDown(0) && _groupMover.pickedBlocks.Count > 0)
        {
            DropAll();
            return;
        }

        // 2) 拖拽逻辑：按下开始、按住继续、松手结束
        if (Input.GetMouseButtonDown(0))
            BeginDrag();
        else if (_isDragging && Input.GetMouseButton(0))
            ContinueDrag();
        else if (_isDragging && Input.GetMouseButtonUp(0))
            EndDrag();
    }

    private void BeginDrag()
    {
        _isDragging = true;
        _selected.Clear();
        _lastWorldPos = ScreenToWorld(Input.mousePosition);
        _startGrid = Grid.Instance.WorldToGridPosition(_lastWorldPos);
    }

    private void ContinueDrag()
    {
        Vector2 curr = ScreenToWorld(Input.mousePosition);
        foreach (var hit in Physics2D.LinecastAll(_lastWorldPos, curr, blockLayer))
            TrySelect(hit.collider);
        var overlap = Physics2D.OverlapPoint(curr, blockLayer);
        if (overlap) TrySelect(overlap);
        _lastWorldPos = curr;
    }

    private void EndDrag()
    {
        _isDragging = false;
        Debug.Log("EndDrag triggered, picking up selected blocks");
        foreach (var g in _selected)
        {
            g.Highlight(false);
            _groupMover.PickUp(g.transform);
        }
    }

    private void DropAll()
    {
        Debug.Log("Dropping all picked blocks");
        // 把列表里的所有方块都放下
        // 注意要先拷贝一份，避免在 Drop 时修改集合
        var toDrop = new List<Transform>(_groupMover.pickedBlocks);
        foreach (var t in toDrop)
            _groupMover.Drop(t);
    }

    private void TrySelect(Collider2D col)
    {
        var g = col.GetComponent<GridObject>();
        if (g != null && _selected.Add(g))
        {
            g.Highlight(true);
            Debug.Log($"TrySelect: added block at {g.gridPosition}");
        }
    }

    private Vector2 ScreenToWorld(Vector2 sp) => _cam.ScreenToWorldPoint(sp);


    /// BFS 查找所有与 root 方块网格四连通的候选方块

    /*private HashSet<GridObject> GetConnectedGroup(HashSet<GridObject> candidates, GridObject root)
{
    Debug.Log($"[Debug] root.gridPosition = {root.gridPosition}");
    Debug.Log($"[Debug] candidates count = {candidates.Count}");
    foreach (var c in candidates)
        Debug.Log($"[Debug] candidate at {c.gridPosition}");

    var result = new HashSet<GridObject>();
    var visited = new HashSet<GridObject> { root };
    var queue   = new Queue<GridObject>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        var cur = queue.Dequeue();
        Debug.Log($"[Debug] Visiting node at {cur.gridPosition}");

        foreach (var dir in new[] { Vector2Int.up *2, Vector2Int.down * 2, Vector2Int.left * 2, Vector2Int.right * 2 })
        {
            var neighborPos = cur.gridPosition + dir;
            Debug.Log($"[Debug]  Checking neighborPos = {neighborPos}");

            foreach (var g in candidates)
            {
                if (!visited.Contains(g) && g.gridPosition == neighborPos)
                {
                    Debug.Log($"[Debug]    Matched candidate at {g.gridPosition}");
                    visited.Add(g);
                    result.Add(g);
                    queue.Enqueue(g);
                }
            }
        }
    }

    Debug.Log($"[Debug] Connected group size = {result.Count}");
    return result;
}*/

    //如果mouse点击一个方块
    //is connected()


    //工具類
    // check mouse release/pressed

    /*if pressed
       moused_pressed = true;
    if released
       mouse_pressed = false;

    if mouse_pressed
        if鼠标的位置 与 boxcollider重合
        {         
            block is picked up/////////如果要检测selection组应该要放在preselectin结束了
            j加入pre_selection
            方块spriterenderer改颜色（lighting调用）
        }

    */
    //check connected to wilmot/ gameobejct present in surrounding 4 gridco
    /*private bool IsConnected() 
     {
         //check surrounding from selected gridobjects
          //记录传过来指令的方块，不给他继续传送is connected指令

         //存在surrounding的selected gameobjects就继续check isconnected()

         //如果一直check到wilmot就true

         //else if 到某一时刻四周不存在未遍历过的selected方块， return false
         return false;
     }*/
}
