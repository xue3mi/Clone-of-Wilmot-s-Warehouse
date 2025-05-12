using System;
using System.Collections.Generic;
using UnityEngine;

public class DragSelector : MonoBehaviour
{
    
    [SerializeField] private LayerMask _blockLayer;

    private readonly HashSet<GridObject> _selected = new HashSet<GridObject>();

    private Camera _camera;
    private bool _isDragging;
    private Vector2 _lastWorldPos;

    
    public event Action<List<GridObject>> OnSelectionComplete;

    private void Awake()
    {
  
        _camera = Camera.main;
    }

    private void Update()
    {
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
    }

    private void ContinueDrag()
    {
        Vector2 curr = ScreenToWorld(Input.mousePosition);

        
        foreach (var hit in Physics2D.LinecastAll(_lastWorldPos, curr, _blockLayer))
            TrySelect(hit.collider);

     
        var overlap = Physics2D.OverlapPoint(curr, _blockLayer);
        if (overlap != null)
            TrySelect(overlap);

        _lastWorldPos = curr;
    }

    private void EndDrag()
    {
        _isDragging = false;

        OnSelectionComplete?.Invoke(new List<GridObject>(_selected));
    }

    private Vector2 ScreenToWorld(Vector3 screenPos)
        => _camera.ScreenToWorldPoint(screenPos);

    private void TrySelect(Collider2D col)
    {
        var grid = col.GetComponent<GridObject>();
        if (grid != null && _selected.Add(grid))
            grid.Highlight(true);
    }
}
