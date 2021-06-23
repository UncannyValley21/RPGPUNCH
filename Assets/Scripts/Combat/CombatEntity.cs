using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatEntity : MonoBehaviour
{
	[SerializeField] private Sprite[] frameArray;
	[SerializeField] private SpriteRenderer spriteRenderer;

	CharacterData characterData;

	public Vector3 startPos;
	public unitState entityState;

	public CharacterData CharacterData { get { return characterData; } }

	public void Setup(CharacterData in_characterData, Sprite[] in_Sprites = null)
	{
		characterData = in_characterData;

		if(in_Sprites != null)
		{
			frameArray = in_Sprites;			
		}

		ChangeFrame(0);
		startPos = transform.position;
		entityState = unitState.Idle;
	}

	public void ChangeFrame(int index)
	{
		spriteRenderer.sprite = frameArray[index];
	}

	public void IncommingDamage(int in_dmg)
	{
		characterData.currentHealth -= in_dmg;
		characterData.currentHealth = Mathf.Clamp(characterData.currentHealth, 0, characterData.maxHealth);

		StartCoroutine(FlashDamage());
	}

	public void ModifySpecial(int in_value)
	{
		characterData.currentSpecial += in_value;
		characterData.currentSpecial = Mathf.Clamp(characterData.currentSpecial, 0, characterData.maxSpecial);
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