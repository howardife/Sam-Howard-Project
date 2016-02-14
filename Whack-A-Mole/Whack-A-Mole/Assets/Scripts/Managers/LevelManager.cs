using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameObject[] m_HitBoxes;

    [Header("UI")]
    [SerializeField] Image m_FuelBar;
    public delegate void E_EditFuel(float amount);
    public static E_EditFuel OnEditFuel;

    [Header("Fuel Drain Stuff")]
    [SerializeField] float m_DrainAmount;

    [Header("Mole Spawn/Life Stuff")]
    [SerializeField] float m_MinTimeBetweenMoles;
    [SerializeField] float m_MaxTimeBetweenMoles;
    [SerializeField] float m_MinMoleLifeDuration;
    [SerializeField] float m_MaxMoleLifeDuration;
    private int m_AmountToSpawn = 1;
    private int m_AmountSpawned = 0;
    private bool IsMoleSpawned = false;

    public delegate void E_ResetLevel();
    public static event E_ResetLevel OnResetLevel;

    public delegate void E_InitialiseLevel();
    public static event E_InitialiseLevel OnInitialiseLevel;

    //Unity Lifecycle=================================================================
    private void Awake()
    {
        LevelManager.OnEditFuel += EditFuel;
        LevelManager.OnInitialiseLevel += InitialiseRound;
    }

    // Use this for initialization
    private void Start() {


    }

    // Update is called once per frame
    private void Update() {

        GameLoop();
    }

    private void InitialiseRound()
    {
        m_FuelBar.fillAmount = 0.5f;
    }

    //---------------------------------------------------------------------------------


    //Privates===============================================================================

    private void GameLoop()
    {
        if (GameState.GAMESTATE != GameState.State.InGame)
            return;

        DrainFuel();

        if (!IsMoleSpawned)
        {
            StartCoroutine(DelayBetweenSpawns(Random.Range(m_MinTimeBetweenMoles, m_MaxTimeBetweenMoles)));
        }
    }

    private void DrainFuel()
    {
        m_FuelBar.fillAmount -= (m_DrainAmount / 100) * Time.deltaTime;

        if (m_FuelBar.fillAmount <= 0f)
            MenuManager.InitChangeMenu("GameOver");
    }

    private IEnumerator DelayBetweenSpawns(float t)
    {
        IsMoleSpawned = true;
        yield return new WaitForSeconds(t);

        bool _notSpawned = true;

        while (_notSpawned)
        {
            GameObject go = SelectSpawn();

            if (go.GetComponent<Image>().color == Color.white)
            {
                go.GetComponent<Image>().color = Random.Range(0, 11) >= 9 ? Color.red : Color.green;
                go.GetComponent<IHitable>().Spawned(Random.Range(m_MinMoleLifeDuration, m_MaxMoleLifeDuration));
                _notSpawned = false;
                IsMoleSpawned = false;
                break;
            }
        }
    }

    private GameObject SelectSpawn()
    {
        return m_HitBoxes[Random.Range(0, m_HitBoxes.Length)];
    }

    private void EditFuel(float _amount)
    {
        m_FuelBar.fillAmount += _amount;

        if (m_FuelBar.fillAmount <= 0f)
            MenuManager.InitChangeMenu("GameOver");
    }

    private void Reset()
    {

    }

    //-----------------------------------------------------------------------------------------------------------

    public void EditorSetValues(float drainAmount, float minTimeBetweenMoles, float maxTimeBetweenMoles, float minMoleDuration, float maxMoleDuration)
    {
        m_DrainAmount = drainAmount;
        m_MinTimeBetweenMoles = minTimeBetweenMoles;
        m_MaxTimeBetweenMoles = maxTimeBetweenMoles;
        m_MaxMoleLifeDuration = maxMoleDuration;
        m_MinMoleLifeDuration = minMoleDuration;
    }

    public static void InitEditFuel(float _amount)
    {
        LevelManager.OnEditFuel(_amount);
    }

    public static void InitInitialiseRound()
    {
        LevelManager.OnInitialiseLevel();
    }


}
