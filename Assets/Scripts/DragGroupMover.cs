using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DragGroupMover : MonoBehaviour
{
    [Header("�������ڵ� Layer")]
    [SerializeField] LayerMask blockLayer;
    [Header("Player ��Ӧ�� GridObject")]
    [SerializeField] GridObject playerGridObject;

    Camera _cam;
    bool _isDragging;
    Vector2 _lastWorldPos;

    // ѡ�еķ��飨����ȥ�أ�
    HashSet<GridObject> _selected = new HashSet<GridObject>();

    // ��ק��ʼʱ��¼����������
    Vector2Int _startGrid;

    void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDrag();
        else if (_isDragging && Input.GetMouseButton(0))
            ContinueDrag();
        else if (_isDragging && Input.GetMouseButtonUp(0))
            EndDrag();
    }

    void BeginDrag()
    {
        _isDragging = true;
        _selected.Clear();
        _lastWorldPos = ScreenToWorld(Input.mousePosition);

        // ��¼��ק��ʼ����������
        _startGrid = Grid.Instance.WorldToGridPosition(_lastWorldPos);
    }

    void ContinueDrag()
    {
        Vector2 curr = ScreenToWorld(Input.mousePosition);

        // 1) �߶ξ����ķ���
        foreach (var h in Physics2D.LinecastAll(_lastWorldPos, curr, blockLayer))
            TrySelect(h.collider);

        // 2) ��ǰ���ϵķ���
        var hit = Physics2D.OverlapPoint(curr, blockLayer);
        if (hit) TrySelect(hit);

        _lastWorldPos = curr;
    }

    void EndDrag()
    {
        _isDragging = false;

        // ��ק����ʱ������ƫ��
        Vector2Int endGrid = Grid.Instance.WorldToGridPosition(ScreenToWorld(Input.mousePosition));
        Vector2Int offset = endGrid - _startGrid;
        if (offset != Vector2Int.zero)
        {
            // 1) �ƶ� Player
            var pg = playerGridObject.gridPosition + offset;
            playerGridObject.SetGridPosition(pg.x, pg.y);
            playerGridObject.GridSnap();

            // 2) �ƶ�����ѡ�з���
            foreach (var g in _selected)
            {
                var ng = g.gridPosition + offset;
                g.SetGridPosition(ng.x, ng.y);
                g.GridSnap();
            }
        }

        // 3) ȡ������
        foreach (var g in _selected)
            g.Highlight(false);
    }

    Vector2 ScreenToWorld(Vector2 sp) => _cam.ScreenToWorldPoint(sp);

    void TrySelect(Collider2D col)
    {
        var g = col.GetComponent<GridObject>();
        if (g != null && _selected.Add(g))
            g.Highlight(true);
    }
}
