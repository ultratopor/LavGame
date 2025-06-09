using System.Linq;
using LavGame.Scripts.Game.State.GameResources;
using LavGame.Scripts.Game.State.Maps;
using ObservableCollections;
using R3;

namespace LavGame.Scripts.Game.State.Root
{
    public class GameStateProxy
    {
        private readonly GameState _gameState;
        public readonly ReactiveProperty<int> CurrentMapId = new();
        public ObservableList<Map> Maps { get; } = new(); // список, за которым можно следить.
        public ObservableList<Resource> Resources { get; } = new();

        public GameStateProxy(GameState gameState)
        {
            /* проходимся по строениям в оригинальном состоянии (GameState), и для каждого состояния добавляем такое же в заместителя,
         * и закидываем его в ObservableList */
            _gameState = gameState;

            InitMaps(gameState);
            InitResources(gameState);

            // подписка на изменения идентификатора в оригинальном состоянии.
            CurrentMapId.Subscribe(newValue => { gameState.CurrentMapId = newValue; });
        }

        public int CreateEntityId()
        {
            // если сущность сохраняется, то и счётчик увеличивается.
            return _gameState.CreateEntityId();
        }

        private void InitMaps(GameState gameState)
        {
            // проходимся по настоящему списку карт и создаём заместительный список здесь.
            gameState.Maps.ForEach(mapOrigin => Maps.Add(new Map(mapOrigin)));

            Maps.ObserveAdd().Subscribe(e => // е - это аргумент, а не объект.
            {
                // подписка на добавление элемента в список.
                var addedMap = e.Value; // это уже объект, добавляемый ы список.

                // в оригинальное состояние в список строений добавляем новый элемент, который собираем из заместителя.
                gameState.Maps.Add(addedMap.Origin);
            });

            Maps.ObserveRemove().Subscribe(e =>
            {
                // когда удаляется объект.
                var removedMap = e.Value;
                // ищем в списке конкретный элемент.
                var removedMapState = gameState.Maps.FirstOrDefault(b => b.Id == removedMap.Id);
                gameState.Maps.Remove(removedMapState); // если он существует, то удаляем его.
            });
        }

        private void InitResources(GameState gameState)
        {
            // проходимся по настоящему списку карт и создаём заместительный список здесь.
            gameState.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));

            Resources.ObserveAdd().Subscribe(e => // е - это аргумент, а не объект.
            {
                // подписка на добавление элемента в список.
                var addedResource = e.Value; // это уже объект, добавляемый ы список.

                // в оригинальное состояние в список строений добавляем новый элемент, который собираем из заместителя.
                gameState.Resources.Add(addedResource.Origin);
            });

            Resources.ObserveRemove().Subscribe(e =>
            {
                // когда удаляется объект.
                var removedResource = e.Value;
                // ищем в списке конкретный элемент.
                var removedResourceData =
                    gameState.Resources.FirstOrDefault(b => b.ResourceType == removedResource.ResourceType);
                gameState.Resources.Remove(removedResourceData); // если он существует, то удаляем его.
            });
        }
    }
}