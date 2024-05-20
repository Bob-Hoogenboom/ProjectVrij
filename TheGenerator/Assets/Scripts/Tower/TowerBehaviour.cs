using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Timer")]
    [Tooltip("The searchLight Timer is determined by a random value between these 2 values, X is lowest, Y is highest")]
    [SerializeField] private Vector2 searchLightTimerRange = new Vector2(45, 75);
    private float _searchLightTimer;
    private float _currentSearchLightTimer;
    private bool _lightIsActive = false;

    [Space]
    [Tooltip("This timer determines how loing the player has to hide when the light is shun oppon them")]
    [SerializeField]private float checkTimer = 5f;
    private float _currentCheckTimer;

    [Header("Detection")]
    [SerializeField] private GameObject searchLight;
    private RaycastHit _hit;


    private void Start()
    {
        //Set SearchLight Timer
        _searchLightTimer = RandomFloat(searchLightTimerRange.x, searchLightTimerRange.y);
        _currentSearchLightTimer = _searchLightTimer;

        //Set Check Timer
        _currentCheckTimer = checkTimer;

        //Player Reference
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            this.gameObject.SetActive(false);
            Debug.LogError(gameObject.name + " Says: Where is the player? Are you missing a player with the player tag or is it really a null reference?");
        }
    }

    private void Update()
    {
        if (!_lightIsActive)
        {
            _currentSearchLightTimer -= Time.deltaTime;
            
        }

        if (_currentSearchLightTimer <= 0f)
        {
            ActivateLight();
        }
    }

    private void ActivateLight()
    {
        searchLight.SetActive(true);
        _lightIsActive = true;
        searchLight.transform.LookAt(player);
        
        //timer till player checked
        _currentCheckTimer -= Time.deltaTime;

        //player checked found? yes, event! : no, deactivate light;
        if (_currentCheckTimer <= 0f) 
        {
            Debug.DrawLine(searchLight.transform.position, player.position, Color.magenta, 5f);

            if (Physics.Raycast(searchLight.transform.position, (player.position - searchLight.transform.position).normalized, out _hit))
            {
                Debug.Log(_hit.collider.gameObject.name);
                if (_hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Found YA!");
                    DeactivateLight(); // for debugging purpose only, should be replaced with a death or restart function;
                }
                else 
                {
                    Debug.Log("Where are youuu...");
                    DeactivateLight();
                }
            }

            //failsafe
            if (!_lightIsActive) 
            {
                DeactivateLight();
            }
            else { return; }
        }


    }

    private void DeactivateLight()
    {
        //deactivate light
        searchLight.SetActive(false);

        //reset cooldown timer
        _searchLightTimer = RandomFloat(searchLightTimerRange.x, searchLightTimerRange.y); // new random value for the 'SearchLight' Timer
        _currentSearchLightTimer = _searchLightTimer;

        _lightIsActive = false;
        //reset check timer
        _currentCheckTimer = checkTimer;
    }


    private float RandomFloat(float a, float b)
    {
        float ranFloat = Random.Range(a, b);
        return ranFloat;
    }
}
