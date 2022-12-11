using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHpController : MonoBehaviour
{
    [SerializeField] private EnemyMovementController enemyMovementController;
    [SerializeField] private GameObject enemyParent;
    private Canvas enemyCanvas;
    [SerializeField] Canvas CanvasPrefab;
    private int EnemyMaxHP = 1000;
    private int HealthPoints;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;
    private SpawnBoss spawningEnemies;
    [SerializeField] private GameObject splatterAnimParent;

    private void Awake()
    {
        enemyMovementController = GetComponentInParent<EnemyMovementController>();
        enemyParent = gameObject;
        enemyCanvas = Instantiate(CanvasPrefab, enemyParent.transform);
        HealthPoints = EnemyMaxHP;
        healthSlider = enemyCanvas.GetComponentInChildren<Slider>();
        healthSlider.maxValue = EnemyMaxHP;
        healthSlider.value = EnemyMaxHP;
        spawningEnemies = enemyParent.GetComponentInParent<SpawnBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCanvas.transform.position = new Vector3(enemyParent.transform.position.x, enemyParent.transform.position.y + 1f, transform.position.z);
    }
    public void TakeDamage(int damage)
    {
        enemyMovementController.knockBack(5f);
        HealthPoints -= damage;
        updateHealth();
    }
    public void updateHealth()
    {
        healthSlider.value = HealthPoints;
        if (healthSlider.value <= 0)
        {
            var clone = Instantiate(splatterAnimParent, transform);
            clone.transform.parent = null;
            spawningEnemies.removeOneEnemyAlive();
            Destroy(gameObject);
        }
    }

}
