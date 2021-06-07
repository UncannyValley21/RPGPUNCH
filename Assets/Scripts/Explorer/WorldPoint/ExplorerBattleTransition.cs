using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public sealed class ExplorerBattleTransition : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(Time.timeSinceLevelLoad < GameData.transitionLoadTime)
			return;

		if(other.gameObject.TryGetComponent(out ExplorerPlayer player))
		{
			GameManager.instance.LoadBattleScene(player.GetMovePoint());
		}
	}
}
