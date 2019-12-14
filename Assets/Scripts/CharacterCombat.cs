using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{

    //Attack Cooldown
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    const float combatCooldown = 5;
    float lastAtkTime;

    public float attackDelay = .6f;

    public bool inCombat { get; private set; }
    public event System.Action OnAttack;

    CharacterStats myStats;

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }


    void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (Time.time - lastAtkTime > combatCooldown)
        {
            inCombat = false;
        }
    }


    public void Attack (CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));


            if (OnAttack != null)
                OnAttack();


            attackCooldown = 1f / attackSpeed;
            inCombat = true;
            lastAtkTime = Time.time;
        }
    }
    

    //makes co-routine to delay damage
    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
        if (stats.currentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
