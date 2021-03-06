using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyInstanceObject", order = 1)]
public class EnemyInstanceObject : ScriptableObject
{
	public Sprite idleSprite;
	public Sprite attackSprite;
	public CharacterData enemyData;
}