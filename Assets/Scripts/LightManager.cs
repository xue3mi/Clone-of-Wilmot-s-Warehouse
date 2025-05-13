using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GridObject player;
   // public GameObject player;

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2Int playerGridPos = Grid.Instance.WorldToGridPosition(playerPos);

        //check all blocks with GridObject
        foreach (GridObject obj in Grid.Instance.GetAllGridObjects())
        {
            LightningTile tile = obj.GetComponent<LightningTile>();
            if (tile != null)
            {
                tile.UpdateLight(playerGridPos);
            }
        }
    }
}
