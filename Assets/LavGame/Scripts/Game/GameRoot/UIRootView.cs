using UnityEngine;

namespace LavGame.Scripts
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;     // временный экран загрузки.
        [SerializeField] private Transform _uiSceneContainer;   // ссылка на объект в префабе UIRoot.

        private void Awake()
        {
            HideLoadingScreen(); 
        }

        public void ShowLoadingScreen()                 // показывать экран загрузки.
        {
            _loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen()                 // скрыть экран загрузки.
        {
            _loadingScreen.SetActive(false);
        }
        
        public void AttachSceneUI(GameObject sceneUI)       // добавляет новый объект sceneUI в префаб UIRoot.
        {
            ClearSceneUI();

            sceneUI.transform.SetParent(_uiSceneContainer, false);  // устанавливает трансформ родительского префаба
        }

        private void ClearSceneUI()                         // удаляет всех детей данного трансформа.
        {
            var childCount = _uiSceneContainer.childCount;
            for(var i = 0; i < childCount; i++)
            {
                Destroy( _uiSceneContainer.GetChild(i).gameObject );
            }
        }
    }
}