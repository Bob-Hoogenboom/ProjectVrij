using UnityEngine;
using UnityEngine.Events;

public class TowerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    private PlayerController playerController;

    [Header("Timers")]
    [Tooltip("The searchLight Timer is determined by a random value between these 2 values, X is lowest, Y is highest")]
    [SerializeField] private Vector2 searchLightTimerRange = new Vector2(45, 75);
    private float _searchLightTimer;
    private float _currentSearchLightTimer;
    [HideInInspector] public bool lightIsActive = false;

    [Space]
    [Tooltip("This timer determines how loing the player has to hide when the light is shun oppon them")]
    [SerializeField]private float checkTimer = 5f;
    private float _currentCheckTimer;

    [Header("Detection")]
    [HideInInspector] public bool playerInSight = false;
    [SerializeField] private GameObject searchLight;
    [SerializeField] private GameObject checkOrigin;
    [SerializeField] private Transform searchLaserOrigin;
    [SerializeField] private LineRenderer searchLaser;
    private RaycastHit _hit;


    [Header("Effects")]
    public UnityEvent onSearchLightActivated;
    public UnityEvent onPlayerFound;


    private void Start()
    {
        //Set SearchLight Timer
        _searchLightTimer = RandomFloat(searchLightTimerRange.x, searchLightTimerRange.y);
        _currentSearchLightTimer = _searchLightTimer;

        //Set Check Timer
        _currentCheckTimer = checkTimer;

        //Get acces to the player

        //Player Reference
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.gameObject.GetComponent<PlayerController>();
        
        //failSave
        if (player == null)
        {
            this.gameObject.SetActive(false);
            Debug.LogError(gameObject.name + " Says: Where is the player? Are you missing a player with the player tag or is it really a null reference?");
        }
    }

    private void Update()
    {
        if (!lightIsActive)
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
        if (!lightIsActive) 
        {
            onSearchLightActivated.Invoke();
            searchLaser.SetPosition(0, searchLaserOrigin.transform.position);
        }


        searchLight.SetActive(true);
        searchLaserOrigin.gameObject.SetActive(true);
        lightIsActive = true;

        Vector3 lightPosition = new Vector3(player.position.x, player.position.y +5, player.position.z);
        searchLaser.SetPosition(1, player.position);
        searchLight.transform.position = lightPosition;

        //If the player ios crouching the timer goes down slower as the tower has to search more
        float downTimer = playerController._isChrouching ? Time.deltaTime : Time.deltaTime/2;
        _currentCheckTimer -= downTimer;

        if (Physics.Raycast(checkOrigin.transform.position, (player.position - checkOrigin.transform.position).normalized, out _hit))
        {
            Debug.Log(_hit.transform.gameObject.name);
            if (_hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.DrawLine(checkOrigin.transform.position, player.position, Color.magenta, 3f);
                playerInSight = true;
            }
            else
            {
                playerInSight = false;
            }
        }

        //player checked found? yes, event! : no, deactivate light;
        if (_currentCheckTimer <= 0f) 
        {
            if (playerInSight)
            {
                Debug.Log("Found YA!");
                onPlayerFound.Invoke();
                return;
            }
            else 
            {
                Debug.Log("Where are youuu...");
                DeactivateLight();
            }

            //failsafe
            if (!lightIsActive) 
            {
                DeactivateLight();
            }
            else { return; }
        }
    }

    public void DeactivateLight()
    {
        //deactivate light
        searchLight.SetActive(false);
        searchLaserOrigin.gameObject.SetActive(false);

        //reset cooldown timer
        _searchLightTimer = RandomFloat(searchLightTimerRange.x, searchLightTimerRange.y); // new random value for the 'SearchLight' Timer
        _currentSearchLightTimer = _searchLightTimer;

        lightIsActive = false;
        //reset check timer
        _currentCheckTimer = checkTimer;
    }

    private float RandomFloat(float a, float b)
    {
        float ranFloat = Random.Range(a, b);
        return ranFloat;
    }
}
