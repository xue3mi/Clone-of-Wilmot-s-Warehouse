using UnityEngine;

public class PlayerGrabbing : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //如果mouse点击一个方块
        //is connected()
    
    //check connected to wilmot/ gameobejct present in surrounding 4 gridco
    private bool IsConnected() 
    {
        //check surrounding from selected gridobjects
         //记录传过来指令的方块，不给他继续传送is connected指令

        //存在surrounding的selected gameobjects就继续check isconnected()

        //如果一直check到wilmot就true

        //else if 到某一时刻四周不存在未遍历过的selected方块， return false
        return false;
    }
}
