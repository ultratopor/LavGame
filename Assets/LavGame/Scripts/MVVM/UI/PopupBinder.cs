﻿using Scripts.MVVM.UI;
using UnityEngine;
using UnityEngine.UI;

namespace LavGame.Scripts.MVVM.UI
{
	public class PopupBinder<T> : WindowBinder<T> where T: WindowViewModel
	{
		[SerializeField] private Button _btnClose;
		[SerializeField] private Button _btnCloseAlt;

		protected virtual void Start()
		{
			_btnClose?.onClick.AddListener(OnCloseButtonClick);
			_btnCloseAlt?.onClick.AddListener(OnCloseButtonClick);
		}

		protected virtual void OnDestroy()
		{
			_btnClose?.onClick.RemoveListener(OnCloseButtonClick);
			_btnCloseAlt?.onClick.RemoveListener(OnCloseButtonClick);
		}

		protected virtual void OnCloseButtonClick()
		{
			ViewModel.RequestClose();
		}
	}
}
