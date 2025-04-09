using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum gameState
    { 
        
        stock_state,
        delivery_state,

    }
    public gameState current_state;
    void Start()
    {
        current_state = gameState.stock_state;
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state) 
        { 


            case gameState.stock_state:
                
                //if 点击alarm 
                current_state = gameState.delivery_state;
                break;
            case gameState.delivery_state:
                //StartDelivery();
                    //计时器开始工作
                    //Generate Request() 四个任务

                //if计时器归零 or 任务完成+点击alarm
                //EndDelivery();
                    //清除request
                    //添加分数
                current_state = gameState.stock_state;
                break;
        }
    }
}
