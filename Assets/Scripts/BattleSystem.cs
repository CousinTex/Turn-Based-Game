using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOTS}
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform playerSpawn;
    public Transform enemySpawn;
    public Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    Unit playerUnit;
    Unit enemyUnit;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Angry " + enemyUnit.unitName + " appears!";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(5f);


        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
   IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
        enemyHUD.SetHP(enemyUnit.currentHp);
        dialogueText.text = "You hit for " + playerUnit.damage + " damage";
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }

        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }
    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(playerUnit.heal);
        playerHUD.SetHP(playerUnit.currentHp);
        dialogueText.text = "You gain " + playerUnit.heal + " health";
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }
    void PlayerTurn()
    {
        dialogueText.text = "Choose your action";
    }
    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " is attacking";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.SetHP(playerUnit.currentHp);
        dialogueText.text = "You take " + enemyUnit.damage + " damage";
        yield return new WaitForSeconds(2f);

        if(isDead)
        {
            state = BattleState.LOTS;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Congratulations! You've won the battle!";
        }
        else if (state == BattleState.LOTS)
        {
            dialogueText.text = "You've lost! Shame! Shame! Shame!";

        }
    }
    public void AttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
    }
    public void HealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
}
