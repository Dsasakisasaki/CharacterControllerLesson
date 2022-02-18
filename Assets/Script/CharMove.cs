using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharMove : MonoBehaviour
{
	Animator animator;
	CharacterController cc;

	//ジャンプでｙ軸の概念があるのでVextor3型を用意
	Vector3 dir = Vector3.zero;
	//インスペクターから調整できるように用意↓
	public float gravity = 20.0f;
	public float speed = 4.0f;
	public float rotSpeed = 300.0f;
	public float jumpPower = 8.0f;

	void Start()
	{
		//StartではなくAwake内でやる方が正しい
		animator = GetComponent<Animator>();
		cc = GetComponent<CharacterController>();
	}


	void Update()
	{
		//前進成分を取得(0~1),今回はバックはしない
		//Input.GetAxis("Vertical") 上下キーを入力すると-1～１を返す　マスマックスで第一引数と第二引数の大きい方が返るのでこれで、下キーを無効にしてる
		float acc = Mathf.Max(Input.GetAxis("Vertical"), 0f);
		//接地していたら
		if (cc.isGrounded)//足元が地面に接地したときtrueを返す
		{
			//左右キーで回転
			float rot = Input.GetAxis("Horizontal");//-1～1の値を返す
			//前進、回転が入力されていた場合大きい方の値をspeedにセットする(転回のみをするときも動くモーションをする)
			//足が止まったまま回らないように、回転してるときも歩く動作をするロジック　事前に条件をパラメータspeedがGreaterで付けてある
			animator.SetFloat("speed", Mathf.Max(acc, Mathf.Abs(rot)));
			//回転は直接トランスフォームをいじる 一秒間に300度回転
			transform.Rotate(0, rot * rotSpeed * Time.deltaTime, 0);

			if (Input.GetButtonDown("Jump"))
			{
				//ジャンプモーション開始
				//SetTrigger() 一回trueになると一回モーションする　内部でtrueにして一回再生した後、falseにしてくれている
				//事前にjumpをtriggerで登録してある
				animator.SetTrigger("jump");
			}
		}
		//下方向の重力成分
		dir.y -= gravity * Time.deltaTime;

		//CharacterControllerはMoveでキャラを移動させる。
		//Moveにtransform.forward（ユニが向いてる方向）
		//acc 0～1　前方向の力
		//dirはｙ軸方向　今は必要ない　
		cc.Move((transform.forward * acc * speed + dir) * Time.deltaTime);
		//移動した後着していたらy成分を0にする。
		if (cc.isGrounded)
		{
			dir.y = 0;
		}

	}

}


