using UnityEngine;

//Enum list for the detectionstates
public enum DetectionStates
{
    Idle,
    PlayerInSight,
    PlayerHidden 
}


/// <summary>
/// This is a script that displays the right UI icon for the player
/// to provide feedback if the tower is able to see the player or not
/// </summary>
public class TowerDetection : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject hiddenUI;
    [SerializeField] private GameObject visibleUI;
    [SerializeField] private TowerBehaviour towerBehaviour;

    [Header("Variables")]
    private DetectionStates states;


    //Checking if the towerbehaviour can see the player or not and switches accordingly
    private void Update()
    {
        if (!towerBehaviour.lightIsActive)
        {
            states = DetectionStates.Idle;
        }
        else if (towerBehaviour.playerInSight)
        {
            states = DetectionStates.PlayerInSight;
        }
        else
        {
            states = DetectionStates.PlayerHidden;
        }

        DisplayUI();
    }


    //Switch case for scalabilit, maybe we want to add sound or sertain events later on
    private void DisplayUI()
    {
        switch (states)
        {
            case DetectionStates.Idle:
                hiddenUI.SetActive(false);
                visibleUI.SetActive(false);
                break;
            case DetectionStates.PlayerInSight:
                hiddenUI.SetActive(false);
                visibleUI.SetActive(true);
                break;
            case DetectionStates.PlayerHidden:
                hiddenUI.SetActive(true);
                visibleUI.SetActive(false);
                break;
            default:
                break;
        }

    }
}
