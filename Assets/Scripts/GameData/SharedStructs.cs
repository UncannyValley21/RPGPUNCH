using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public struct Gem
{
	public string name;
	public string ID;
	public int power;
	public int power2;
}

[System.Serializable]
public struct CharacterData
{
	public string name;
	public int maxHealth;
	public int currentHealth;
	public int maxSpecial;
	public int currentSpecial;
	public int attackDamage;
	public int experience;
	public int gemSlots;
	public List<Gem> equippedGems;
	public List<Gem> inventoryGem;
}
[System.Serializable]
public struct EnemyAttack
{
	public int damage;
	public float speed;
	public int attackSquares;
}