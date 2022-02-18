using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform player;

	void LateUpdate()
	{
		//カメラを少し後ろからにしたい
		//                     ユニが今いる場所  ユニが向いてる方の反対方向　　上の方向に移動　　　player.forward が長さ1なので*3でオッケー
		//カメラはz軸の方向に向く
		//transform.position = player.position + (-player.forward * 3.0f) + (player.up * 1.0f);


		//ディレイ導入
		transform.position =
			Vector3.Lerp(transform.position,
			player.position + (-player.forward * 3.0f)
			+ (player.up * 1f), Time.deltaTime * 10f);

		//LookAt その方向にｚ軸を向ける
		//player.positionだけだとユニの中心（腰）に向かってカメラ（ｚ軸）を向けてしまうのでVector3.up（0,1,0）を足して上の方にしてる
		transform.LookAt(player.position + Vector3.up);
	}
}
