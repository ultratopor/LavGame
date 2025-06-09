﻿using Assets.LavGame.Scripts.Game.Gameplay.View.UI.ScreenGameplay;
using Assets.LavGame.Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LavGame.Scripts.Game.Gameplay.View.UI.ScreenGameplay
{
	public class ScreenGameplayBinder : WindowBinder<ScreenGameplayViewModel>
	{
		[SerializeField] private Button _btnPopupA;
		[SerializeField] private Button _btnPopupB;
		[SerializeField] private Button _btnGoToMainMenu;

		private void OnEnable()
		{
			_btnPopupA.onClick.AddListener(OnPopupAButtonClicked);
			_btnPopupB.onClick.AddListener(OnPopupBButtonClicked);
			_btnGoToMainMenu.onClick.AddListener(OnGoToMainMenuButtonClicked);
		}

		private void OnDisable()
		{
			_btnPopupA.onClick.RemoveListener(OnPopupAButtonClicked);
			_btnPopupB.onClick.RemoveListener(OnPopupBButtonClicked);
			_btnGoToMainMenu.onClick.RemoveListener(OnGoToMainMenuButtonClicked);
		}

		private void OnGoToMainMenuButtonClicked()
		{
			ViewModel.RequestGoToMainMenu();
		}

		private void OnPopupBButtonClicked()
		{
			ViewModel.RequestOpenPopupB();
		}

		private void OnPopupAButtonClicked()
		{
			ViewModel.RequestOpenPopupA();
		}
	}
}
