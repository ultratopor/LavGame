﻿using UnityEngine;

namespace LavGame.Scripts.MVVM.Example
{
    // Binder - создаётся вместе с View.
    public class BuildingNameWindowBinder 
    {
        [SerializeField] private BuildingNameWindowView _view;
        
        public void Bind(BuildingNameEntity viewModel)
        {
            _view.BuildingNameText.text = viewModel.BuildingName.Value;
        }

        public void Close()
        {
            // какие-то действия при закрытии.
        }
    }
}