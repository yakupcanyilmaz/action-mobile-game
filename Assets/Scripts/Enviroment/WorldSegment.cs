using System.Collections;
using UnityEngine;

public class WorldSegment : MonoBehaviour
{
  [SerializeField] private GameObject shield;
  public ParticleSystem spawnParticles;
  public Transform playerSpawnPoint;
  public Transform[] enemySpawnPoints;
  private int enemyArrayNummber;

  private void OnEnable()
  {
    shield.SetActive(true);
  }

  public void OpenGate()
  {
    StartCoroutine("RemoveShield");
  }

  private IEnumerator RemoveShield()
  {
    yield return new WaitForSeconds(1.5f);
    AudioManager.Instance.PlaySound("ShieldBreak");
    shield.SetActive(false);
  }

  public void SpawnParticles()
  {
    if (enemySpawnPoints.Length == 0) return;
    for (int i = 0; i < enemySpawnPoints.Length; i++)
    {
      SpawnParticles(i);
    } 
  }

  public void SpawnEnemies()
  {
    if (enemySpawnPoints.Length == 0) return;
    for (int i = 0; i < enemySpawnPoints.Length; i++)
    {
      SpawnEnemies(i);
    } 
  }

  private void SpawnParticles(int i)
  {
    var pfx = Poolable.TryGetPoolable<ParticleSystem>(spawnParticles.gameObject);
    pfx.transform.position = new Vector3(enemySpawnPoints[i].position.x, enemySpawnPoints[i].position.y + 0.01f, enemySpawnPoints[i].position.z);
    pfx.Play();
  }

  private void SpawnEnemies(int i)
  {
    Agent enemy = Poolable.TryGetPoolable<Agent>(GameManager.Instance.CurrentWave.enemies[i].gameObject);
    enemy.transform.position = enemySpawnPoints[i].position;
    enemy.transform.rotation = enemySpawnPoints[i].rotation;
    enemy.OnAISpawned.Invoke();
  }
}
