using UnityEngine;

namespace LavGame.Scripts.MVVM.Example
{
    public class BuildingNameWindowManager
    {
        public void OpenBuildingNameWindow(BuildingNameEntityData data)
        {
            var viewModel = new BuildingNameEntity(data);
            var window = Object.Instantiate("SomeWindowPrefab");
            var binder = window.GetComponent<BuildingNameWindowBinder>();
            binder.Bind(viewModel);
        }
        // ... ( other methods)
    }
}