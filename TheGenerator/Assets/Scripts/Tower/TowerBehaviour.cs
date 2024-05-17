using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Timer")]
    [SerializeField] private Vector2 coolDownRange = new Vector2(45, 75);
    private float _coolDownTimer;
    private float _currentCoolDownTime;
    private bool _isCountingDown = true;

    [Space]
    [SerializeField]private float checkTimer = 5f;
    private float _currentCheckTimer;

    [Header("Detection")]
    [SerializeField] private GameObject searchLight;
    private RaycastHit _hit;

    private void Start()
    {
        _coolDownTimer = RandomFloat(coolDownRange.x, coolDownRange.y);
        _currentCoolDownTime = _coolDownTimer;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            this.gameObject.SetActive(false);
            Debug.LogError(gameObject.name + " Says: Where is the player? Are you missing a player with the player tag or is it really a null reference?");
        }
    }

    private void Update()
    {
        if (_isCountingDown)
        {
            _currentCoolDownTime -= Time.deltaTime;
            
        }

        if (_currentCoolDownTime <= 0f)
        {
            ActivateLight();
        }
    }

    private void ActivateLight()
    {
        _isCountingDown = false;
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
                }
                else 
                {
                    Debug.Log("Where are youuu...");
                    DeactivateLight();
                }
            }

            Debug.Log("No ObjectHit");
            DeactivateLight();
        }


    }

    private void DeactivateLight()
    {
        //deactivate light
        //reset cooldown timer
        _coolDownTimer = RandomFloat(coolDownRange.x, coolDownRange.y); // new random value
        _currentCoolDownTime = _coolDownTimer; 

        _isCountingDown = true;
        //reset check timer
        _currentCheckTimer = checkTimer;
    }


    private float RandomFloat(float a, float b)
    {
        float ranFloat = Random.Range(a, b);
        return ranFloat;
    }
}
