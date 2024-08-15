using R3;
using UnityEngine;

namespace View
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSignalSubj;
        public void HandleGoToMainMenuButtonClick()
        {
           _exitSceneSignalSubj?.OnNext(Unit.Default);
        }
        // сюда должна передаваться View модель, пока что только subject.
        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSignalSubj = exitSceneSignalSubj;
        }
    }
}