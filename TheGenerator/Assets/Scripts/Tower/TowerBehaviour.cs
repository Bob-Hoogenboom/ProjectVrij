using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TowerBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [Header("Timers")]
    [Tooltip("The searchLight Timer is determined by a random value between these 2 values, X is lowest, Y is highest")]
    [SerializeField] private Vector2 searchLightTimerRange = new Vector2(45, 75);
    private float _searchLightTimer;
    private float _currentSearchLightTimer;
    private bool _lightIsActive = false;

    [Space]
    [Tooltip("This timer determines how loing the player has to hide when the light is shun oppon them")]
    [SerializeField]private float checkTimer = 5f;
    private float _currentCheckTimer;

    [Space]
    [SerializeField] private float endOfGameTimer = 3f;

    [Header("Detection")]
    [SerializeField] private GameObject searchLight;
    [SerializeField] private GameObject checkOrigin;
    [SerializeField] private GameObject SearchCone;
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
        if (!_lightIsActive) 
        {
            onSearchLightActivated.Invoke();
        }

        searchLight.SetActive(true);
        SearchCone.SetActive(true);
        _lightIsActive = true;

        SearchCone.transform.LookAt(player.transform);
        Vector3 playerPos = new Vector3(player.position.x, player.position.y + 5 , player.position.z);
        searchLight.transform.position = playerPos;
        
        //timer till player checked
        _currentCheckTimer -= Time.deltaTime;

        //player checked found? yes, event! : no, deactivate light;
        if (_currentCheckTimer <= 0f) 
        {
            Debug.DrawLine(checkOrigin.transform.position, player.position, Color.magenta, 10f);

            if (Physics.Raycast(checkOrigin.transform.position, (player.position - checkOrigin.transform.position).normalized, out _hit))
            {
                Debug.Log(_hit.collider.gameObject.name);
                if (_hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Found YA!");
                    onPlayerFound.Invoke();
                    StartCoroutine(PlayerFound());
                    return;
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
        SearchCone.SetActive(false);

        //reset cooldown timer
        _searchLightTimer = RandomFloat(searchLightTimerRange.x, searchLightTimerRange.y); // new random value for the 'SearchLight' Timer
        _currentSearchLightTimer = _searchLightTimer;

        _lightIsActive = false;
        //reset check timer
        _currentCheckTimer = checkTimer;
    }

    private IEnumerator PlayerFound()
    {
        yield return new WaitForSeconds(endOfGameTimer);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restart the level
    }

    private float RandomFloat(float a, float b)
    {
        float ranFloat = Random.Range(a, b);
        return ranFloat;
    }
}
