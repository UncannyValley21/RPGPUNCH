using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEntity : MonoBehaviour
{
	[SerializeField] private Sprite[] frameArray;
	[SerializeField] private SpriteRenderer spriteRenderer;

	private int health = 100;
	private int attackDamage = 5;
	private string locName = "Player";

	public Vector3 startPos;
	public unitState entityState;
	public int Health { get { return health; } }
	public int AttackDamage { get { return attackDamage; } }
	public string LocName { get { return locName; } }


	public void Setup(EnemyInstanceObject enemyInstance)
	{
		frameArray = new Sprite[] { enemyInstance.idleSprite, enemyInstance.attackSprite };
		health = enemyInstance.health;
		locName = enemyInstance.locName;
	}

	private void Awake()
	{
		ChangeFrame(0);
		startPos = transform.position;
		entityState = unitState.Idle;
	}

	public void ChangeFrame(int index)
	{
		spriteRenderer.sprite = frameArray[index];
	}

	public void DoDamage(int dmg)
	{
		health -= dmg;
		Mathf.Clamp(Health, 0, 100);
		//StartCoroutine(Shake());
		StartCoroutine(FlashDamage());
	}

	public void setColor(Color in_color)
	{
		spriteRenderer.color = in_color;
	}

	IEnumerator Shake()
	{
		for(int i = 0; i < 15; i++)
		{
			spriteRenderer.transform.position += new Vector3(UnityEngine.Random.Range(-0.05f, 0.05f), UnityEngine.Random.Range(-0.05f, 0.05f));
			yield return new WaitForSeconds(0.02f);
		}
		spriteRenderer.transform.position = Vector3.zero;
	}

	IEnumerator FlashDamage()
	{
		setColor(Color.red);
		yield return new WaitForSeconds(0.1f);
		setColor(Color.white);
	}
}