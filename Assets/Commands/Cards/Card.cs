using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Robo.Commands{
	public class Card : MonoBehaviour {

		[SerializeField] Image image;

		[SerializeField] CardConfig cardConfig;
		
		public CardConfig GetCardConfig{ get { return cardConfig; } }
		public CardConfig SetCardConfig{ set { cardConfig = value; } }
		public void SetSprite(Sprite sprite){
			image.sprite = sprite;
		}
	}
}