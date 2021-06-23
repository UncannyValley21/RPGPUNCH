using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	string lastMapScene;
	public Vector2? transitionMapPosition = null;
	static public GameManager instance;
	public CharacterData playerData;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			playerData.name = "";
			DontDestroyOnLoad(this);

			playerData.maxHealth = 100;
			playerData.currentHealth = 100;
			playerData.attackDamage = 3;
			playerData.maxSpecial = 100;
			playerData.currentSpecial = 0;	
		}
		else
		{
			Destroy(this);
		}
	}

	public void StartNewGame()
	{
		LoadMapScene(GameData.startWorldMap, null);
	}

	public void LoadBattleScene(Vector2 lastPos)
	{
		lastMapScene = SceneManager.GetActiveScene().name;
		transitionMapPosition = lastPos;
		SceneManager.LoadScene("BattleScene");
	}

	public void LoadMapScene(string levelName, Vector2? targetPosition)
	{
		SceneManager.LoadScene(levelName);
		transitionMapPosition = targetPosition;
	}

	public void ReturnToLastMapScene()
	{
		if(string.IsNullOrEmpty(lastMapScene))
		{
			lastMapScene = GameData.defaultExplorerMap;
		}
		SceneManager.LoadScene(lastMapScene);
	}

	public void GiveExperience(int experience)
	{
		playerData.experience += experience;
	}

	public void GameOver()
	{
		SceneManager.LoadScene(0);
	}
}