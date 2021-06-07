using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyInstanceObject", order = 1)]
public class EnemyInstanceObject : ScriptableObject
{
	public string locName;
	public int health;
	public Sprite idleSprite;
	public Sprite attackSprite;
}