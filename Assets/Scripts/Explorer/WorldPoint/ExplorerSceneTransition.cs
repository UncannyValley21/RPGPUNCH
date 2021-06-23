using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public sealed class ExplorerSceneTransition : MonoBehaviour
{
	[SerializeField] private string targetSceneName;
	[SerializeField] private Vector2 targetSceneLocation;

	private void OnDrawGizmos()
	{
		Gizmos.DrawIcon(transform.position, "ExitGizmo.png", true);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(Time.timeSinceLevelLoad < GameData.transitionLoadTime)
			return;

		if(other.gameObject.TryGetComponent(out ExplorerPlayer player))
		{
			GameManager.instance.LoadMapScene(targetSceneName, targetSceneLocation);
		}
	}
}
