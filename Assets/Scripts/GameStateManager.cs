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
                
                //if ���alarm 
                current_state = gameState.delivery_state;
                break;
            case gameState.delivery_state:
                //StartDelivery();
                    //��ʱ����ʼ����
                    //Generate Request() �ĸ�����

                //if��ʱ������ or �������+���alarm
                //EndDelivery();
                    //���request
                    //��ӷ���
                current_state = gameState.stock_state;
                break;
        }
    }
}
