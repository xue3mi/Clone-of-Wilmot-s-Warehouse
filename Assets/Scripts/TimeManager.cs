using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
    public TextMeshProUGUI phaseText;
    public CameraCutsceneManager sceneCamera;
    //public Image timeBar;
    public Image timeFill;
    public Image timeBackground;
    public List<GameObject> hintBars;
    public List<GameObject> customerBubbles;


    private enum Phase { Delivery, Service, StockTake }
    private Phase currentPhase = Phase.Delivery;

    private float deliveryDuration = 180f;  // 3m 0s
    private float serviceDuration = 90f;    // 1m 30s
    private float timer;
    private float currentPhaseDuration;

    private bool triggeredDownAnimation = false;
    private bool triggeredUpAnimation = false;

    private void Start()
    {
        timer = deliveryDuration;
        currentPhaseDuration = deliveryDuration;
        UpdatePhaseText();
    }

    private void Update()
    {
        // Change timeBackground color when in service phase
        if (currentPhase == Phase.Service)
        {
            timeBackground.color = new Color(0.996f, 0.992f, 0.925f);  // Service phase: #FEFDEC
            // Service phase: black text & fillBar
            phaseText.color = Color.black;
            timeFill.color = Color.black;
        }
        else
        {
            timeBackground.color = new Color(0f, 0f, 0f, 200f / 255f);  // Default color for other phases
            // Default text color
            phaseText.color = Color.white;
            timeFill.color = new Color(0.996f, 0.980f, 0.918f); //#FEFAEA
        }

        if (currentPhase != Phase.StockTake)
        {
            timer -= Time.deltaTime;

            // update time bar fill amount
            if (timeFill != null)
            {
                //display time bar fill amount (from right to left)
                timeFill.fillAmount = Mathf.Clamp01(timer / currentPhaseDuration);
            }

            // Trigger LowerCameraAnimation at 2m 55s
            if (!triggeredDownAnimation && currentPhase == Phase.Delivery && timer <= 175f)
            {
                triggeredDownAnimation = true;
                StartCoroutine(sceneCamera.LowerCameraMove());
            }

            // Switch to next phase when time is up
            if (timer <= 0f)
            {
                SwitchToNextPhase();
            }

            UpdatePhaseText();
        }
        else
        {
            UpdatePhaseText();
        }

        // all HintBar Show/Hide
        foreach (GameObject bar in hintBars)
        {
            if (bar != null)
            {
                bar.SetActive(currentPhase == Phase.Service);
            }
        }

        // all customerBubble Show/Hide
        foreach (GameObject bubble in customerBubbles)
        {
            if (bubble != null)
            {
                bubble.SetActive(currentPhase == Phase.Service);
            }
        }

    }

    private void SwitchToNextPhase()
    {
        if (currentPhase == Phase.Delivery)
        {
            currentPhase = Phase.Service;
            timer = serviceDuration;
            currentPhaseDuration = serviceDuration;

            // trigger camera up animation at end of delivery phase
            if (!triggeredUpAnimation)
            {
                triggeredUpAnimation = true;
                StartCoroutine(sceneCamera.UpperCameraMove());
            }
        }
        else if (currentPhase == Phase.Service)
        {
            currentPhase = Phase.StockTake;
            timer = 0f;
            currentPhaseDuration = 1f; //avoid division by zero
        }
    }

    private void UpdatePhaseText()
    {
        if (currentPhase == Phase.StockTake)
        {
            phaseText.text = "STOCK TAKE";
        }
        else
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            string phaseName = currentPhase == Phase.Delivery ? "DELIVERY PHASE" : "SERVICE PHASE";
            phaseText.text = $"{phaseName}: {minutes}m {seconds}s LEFT";
        }
    }
}
