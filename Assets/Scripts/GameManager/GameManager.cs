using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : Singleton<GameManager>
{
  public bool isMobileGame;
  [SerializeField] private float spawnTimeDelay = 2f;
  [SerializeField] private Player player;
  [SerializeField] private Level[] levels;

  public Player Player { get { return player; } }
  private Wave currentWave;
  public Wave CurrentWave { get { return currentWave; } }
  private Level currentLevel;
  private WorldSegment currentLevelWorldSegment;
  private int currentWaveNumber = 0;
  private int currentLevelNumber = 0;
  private int enemiesRemainingToSpawn = 0;
  public int EnemiesRemainingToSpawn { get { return enemiesRemainingToSpawn; } }
  private int enemiesRemainingAlive = 0;
  private float nextWaveTime;
  private float spawnTime;

  private bool isWorldSegmentReady;

  private bool isPaused;
  private bool isLevelCompleted;
  private bool isEnemySpawned;
  private bool isParticlesSpawned;

  [SerializeField] private GameEventSO OnEnemyEliminated;
  [SerializeField] private GameEventSO OnLevelCompleted;

  private void OnEnable()
  {
    if (player == null) player = GetComponent<Player>();
    isPaused = true;
    ToggleTimeScale();
    SwitchFocusedPlayerControlScheme();
  }

  IEnumerator LoadWorldSegment()
  {
    GameMenu.Instance.blackScreen.gameObject.SetActive(true);
    var op = Addressables.InstantiateAsync(levels[currentLevelNumber - 1].worldSegmentReference, Vector3.zero, Quaternion.identity);
    yield return op;
    levels[currentLevelNumber - 1].worldSegment = op.Result;

    // var op = Poolable.TryGetPoolable(levels[currentLevelNumber - 1].worldSegmentPrefab);
    // yield return op;
    // levels[currentLevelNumber - 1].worldSegment = op;

    currentLevelWorldSegment = levels[currentLevelNumber - 1].worldSegment.GetComponent<WorldSegment>();

    isWorldSegmentReady = true;
    player.transform.position = currentLevelWorldSegment.playerSpawnPoint.position;
    player.transform.rotation = currentLevelWorldSegment.playerSpawnPoint.rotation;
    player.RawInputMovement = Vector3.zero;
    player.gameObject.SetActive(true);
    GameMenu.Instance.blackScreen.gameObject.SetActive(false);
  }

  // private void LoadWorldSegment(AssetReference assetReference)
  // {
  //   Addressables.InstantiateAsync(assetReference, Vector3.zero, Quaternion.identity).Completed += (op) =>
  //       {
  //         levels[currentLevelNumber - 1].worldSegment = op.Result;
  //         currentLevelWorldSegment = levels[currentLevelNumber - 1].worldSegment.GetComponent<WorldSegment>();

  //         isWorldSegmentReady = true;
  //         player.transform.position = currentLevelWorldSegment.playerSpawnPoint.position;
  //         player.transform.rotation = currentLevelWorldSegment.playerSpawnPoint.rotation;
  //       };
  // }

  public void StartGame()
  {
    StartCoroutine("LoadFirstLevel");
  }

  IEnumerator LoadFirstLevel()
  {
    if (currentLevelNumber <= 0)
    {
      currentLevelNumber++;
    }
    StartCoroutine("LoadWorldSegment");
    yield return null;
  }

  private void Update()
  {
    if (isWorldSegmentReady == true && currentLevelWorldSegment != null)
    {
      if (enemiesRemainingAlive == 0 && Time.time > nextWaveTime && isLevelCompleted == false)
      {
        NextWave();
      }

      if (isParticlesSpawned == true)
      {
        currentLevelWorldSegment.SpawnParticles();
        spawnTime = Time.time + spawnTimeDelay;
        isParticlesSpawned = false;
      }

      if (isEnemySpawned == true && Time.time > spawnTime)
      {
        currentLevelWorldSegment.SpawnEnemies();
        isEnemySpawned = false;
      }
    }
  }

  public void UpdateEnemyList()
  {
    enemiesRemainingAlive--;

    if (enemiesRemainingAlive == 0)
    {
      nextWaveTime = Time.time + currentLevel.timeBetweenWaves;
    }

    if (OnEnemyEliminated != null) OnEnemyEliminated.Invoke();
  }

  private void NextWave()
  {
    if (currentWaveNumber <= levels[currentLevelNumber - 1].waves.Length)
    {
      currentWaveNumber++;
      if (currentWaveNumber > levels[currentLevelNumber - 1].waves.Length && isLevelCompleted == false)
      {
        SetLevelCompleted();
      }
      else
      {          
        currentLevel = levels[currentLevelNumber - 1];
        currentWave = currentLevel.waves[currentWaveNumber - 1];

        enemiesRemainingToSpawn = currentWave.enemies.Length;
        enemiesRemainingAlive = enemiesRemainingToSpawn;
        isParticlesSpawned = true;
        isEnemySpawned = true;
      }
    }
  }

  private void SetLevelCompleted()
  {
    isLevelCompleted = true;
    if (OnLevelCompleted != null) OnLevelCompleted.Invoke();
    currentLevelWorldSegment.OpenGate();
  }

  public void NextLevel()
  {
    currentWaveNumber = 1;
    isLevelCompleted = false;
    
    if (currentLevelNumber == levels.Length && enemiesRemainingAlive == 0)
    {      
      GameEndMenu.Show();
      GameEndMenu.Instance.OpenWinMenu();
    }

    if (currentLevelNumber < levels.Length)
    {
      Addressables.ReleaseInstance(levels[currentLevelNumber - 1].worldSegment);
      // Poolable.TryPool(levels[currentLevelNumber - 1].worldSegment);
      isWorldSegmentReady = false;

      currentLevelNumber++;

      currentLevel = levels[currentLevelNumber - 1];
      currentWave = currentLevel.waves[0];
      isParticlesSpawned = true;
      isEnemySpawned = true;

      enemiesRemainingToSpawn = currentWave.enemies.Length;
      enemiesRemainingAlive = enemiesRemainingToSpawn;

      player.gameObject.SetActive(false);

      StartCoroutine("LoadWorldSegment");
    }
  }

  public void HandleTogglePauseState()
  {
    TogglePauseState();
    UpdateUIMenu();
  }

  public void TogglePauseState()
  {
    isPaused = !isPaused;
    ToggleTimeScale();
    SwitchFocusedPlayerControlScheme();
  }

  void ToggleTimeScale()
  {
    float newTimeScale = 0f;

    switch (isPaused)
    {
      case true:
        newTimeScale = 0f;
        break;
      case false:
        newTimeScale = 1f;
        break;
    }

    Time.timeScale = newTimeScale;
  }

  void SwitchFocusedPlayerControlScheme()
  {
    switch (isPaused)
    {
      case true:
        player.EnablePauseMenuControls();
        break;
      case false:
        player.EnableGameplayControls();
        break;
    }
  }

  void UpdateUIMenu()
  {
    MenuManager.Instance.HandleBackPressed();
  }

  [System.Serializable]
  public class Level
  {
    public AssetReference worldSegmentReference;
    // public GameObject worldSegmentPrefab;
    [HideInInspector] public GameObject worldSegment;
    public float timeBetweenWaves;
    public Wave[] waves;
  }

  [System.Serializable]
  public class Wave
  {
    public GameObject[] enemies;
  }
}
