using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        Vector2 playerPos = player.transform.position;
        Vector2Int playerGridPos = new Vector2Int(Mathf.RoundToInt(playerPos.x), Mathf.RoundToInt(playerPos.y));

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
