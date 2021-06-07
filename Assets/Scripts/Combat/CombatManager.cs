using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public sealed class CombatManager : MonoBehaviour
{
	[SerializeField] private CombatEntity enemy;
	[SerializeField] private CombatEntity player;
	[SerializeField] private Text enemyName;
	[SerializeField] private Text enemyHealthText;
	[SerializeField] private Text playerHealthText;
	[SerializeField] private Text comboText;
	[SerializeField] private SpriteRenderer[] threatSquare;

	int combo = 1;
	float enemyTurnTimer;
	LayerMask dodgeLayer = 1;

	private void Awake()
	{
		enemy.Setup(Resources.Load<EnemyInstanceObject>(GameData.enemyList[UnityEngine.Random.Range(0, GameData.enemyList.Length)]));
		enemyName.text = enemy.LocName;
	}

	private void Update()
	{	
		//Player
		playerHealthText.text = player.Health.ToString();

		//Enenmy
		enemyTurnTimer += Time.deltaTime * UnityEngine.Random.Range(1f, 10.0f);

		if(enemy.entityState == unitState.Idle && enemyTurnTimer > 10.0f)
		{
			enemyTurnTimer = 0;

			int attack = UnityEngine.Random.Range(0, 5);

			if(attack == 0)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 1));
			}
			else if(attack == 1)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 3));
			}
			else if(attack == 2)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 5));
			}
			else if(attack == 3)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 9));
			}
			else if(attack == 4)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 11));
			}
			else if(attack == 5)
			{
				StartCoroutine(EnemyAttack(5, 0.20f, 13));
			}



		}

		enemyHealthText.text = enemy.Health.ToString();

		if(enemy.Health <= 0)
		{
			GameManager.instance.ReturnToLastMapScene();
		}
		else if (player.Health <= 0)
		{
			GameManager.instance.GameOver();
		}
	}

	public void OnAttackInput(InputAction.CallbackContext context)
	{
		if(player.entityState == unitState.Idle && dodgeLayer == 1 && context.started)
		{
			StartCoroutine(Attack(2, 0.15f));	
		}
	}

	public void OnSpecialInput(InputAction.CallbackContext context)
	{
		if(player.entityState == unitState.Idle && dodgeLayer == 1 && context.started)
		{
			StartCoroutine(HeavyAttack(2, 0.3f));
		}
	}

	public void OnMovement(InputAction.CallbackContext context)
	{
		if(dodgeLayer == 1 && context.started)
		{
			Vector2 movement = context.ReadValue<Vector2>();

			if(movement.y > 0)
				return;

			player.transform.position = player.startPos + (Vector3)movement*2;
			dodgeLayer = 0;
			dodgeLayer += movement.x > 0 ? 2 : 0;
			dodgeLayer += movement.x < 0 ? 4 : 0;
			dodgeLayer += movement.y < 0 ? 8 : 0;
		}
		else if((dodgeLayer != 1 && context.started) || context.canceled)
		{
			player.transform.position = player.startPos;
			dodgeLayer = 1;
		}
	}

	private IEnumerator Attack(int damage, float actionTime)
	{
		player.transform.position = player.startPos;
		player.setColor(Color.white);
		player.entityState = unitState.Attack;
		player.transform.position += new Vector3(0f, 0.25f);
		player.ChangeFrame(1);
		float attackTimer = 0;

		while(player.entityState == unitState.Attack)
		{
			yield return new WaitForSeconds(0.01f);
			attackTimer += 0.01f;

			if(attackTimer >= actionTime)
			{
				enemy.DoDamage(damage);
				ModifyCombatMultiplier(combo +1);
				player.entityState = unitState.Idle;
			}

		}
		player.entityState = unitState.Idle;
		player.ChangeFrame(0);
		player.transform.position = player.startPos;
		
	}

	private IEnumerator HeavyAttack(int damage, float actionTime)
	{
		player.transform.position = player.startPos;
		player.setColor(Color.white);
		player.entityState = unitState.Attack;
		player.transform.position += new Vector3(0f, 0.5f);
		player.ChangeFrame(1);
		float attackTimer = 0;

		while(player.entityState == unitState.Attack)
		{
			yield return new WaitForSeconds(0.01f);
			attackTimer += 0.01f;

			if(attackTimer >= actionTime)
			{
				enemy.DoDamage(damage*combo);
				player.entityState = unitState.Idle;
			}
		}
		player.entityState = unitState.Idle;
		player.ChangeFrame(0);
		player.transform.position = player.startPos;
	}

	private IEnumerator EnemyAttack(int damage, float time, LayerMask attackArea)
	{
		enemy.entityState = unitState.Attack;
		StartCoroutine(FlashThreatSquare(0.7f, attackArea));
		yield return new WaitForSeconds(0.3f);
		enemy.transform.position += new Vector3(0, 1);
		yield return new WaitForSeconds(0.3f);
		enemy.transform.position += new Vector3(0, -2);
		if((dodgeLayer & attackArea) > 0)
		{
			player.DoDamage(damage);
			ModifyCombatMultiplier(1);
		}
		yield return new WaitForSeconds(0.1f);
		enemy.transform.position = enemy.startPos;
		
		enemy.entityState = unitState.Idle;
	}

	private void ModifyCombatMultiplier(int newCombo)
	{
		combo = newCombo;

		if(combo > 1)
		{
			comboText.text = "Combo: x " + combo.ToString();
		}
		else
		{
			comboText.text = "";
		}
	}

	private IEnumerator FlashThreatSquare(float duration, LayerMask attackArea)
	{

		Debug.Log(attackArea.value);

		if(attackArea == (attackArea | (1 << 0)))
		{
			threatSquare[0].gameObject.SetActive(true);
		}
		if(attackArea == (attackArea | (1 << 1)))
		{
			threatSquare[1].gameObject.SetActive(true);
		}
		if(attackArea == (attackArea | (1 << 2)))
		{
			threatSquare[2].gameObject.SetActive(true);
		}
		if(attackArea == (attackArea | (1 << 3)))
		{
			threatSquare[3].gameObject.SetActive(true);
		}

		yield return new WaitForSeconds(duration);
		for(int i = 0; i < 4; i++)
		{
			threatSquare[i].gameObject.SetActive(false);
		}
	}
}