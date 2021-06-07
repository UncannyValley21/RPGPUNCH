using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	string lastMapScene;
	public Vector2? transitionMapPosition;

	static public GameManager instance;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
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

	public void GameOver()
	{
		SceneManager.LoadScene(0);
	}

}