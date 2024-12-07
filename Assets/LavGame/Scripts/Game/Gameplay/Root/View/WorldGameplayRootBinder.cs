using System.Collections.Generic;
using ObservableCollections;
using R3;
using UnityEngine;

public class WorldGameplayRootBinder : MonoBehaviour
{
	// создание динамического массива с префабами ради быстрого поиска по ключу.
	private readonly Dictionary<int, BuildingBinder> _createdBuildingsMap = new();

			// реактивная комбинированная подписка.
	private readonly CompositeDisposable _disposables = new();

	public void Bind(WorldGameplayRootViewModel viewModel)
	{
		foreach(var buildingViewModel in viewModel.AllBuildings)
		{       // связывание префаба и View Model для каждой сущности в словаре.
			CreateBuilding(buildingViewModel);
		}

		// подписка на добавление в массив новых View Model.
		_disposables.Add(viewModel.AllBuildings.ObserveAdd().Subscribe(e =>
		{
			CreateBuilding(e.Value);
		}));

		// подписка на удаление из массива View Model.
		_disposables.Add(viewModel.AllBuildings.ObserveRemove().Subscribe(e =>
		{
			DestroyBuilding(e.Value);
		}));

	}
	private void OnDestroy()
	{		// отписка от событий при удалении.
		_disposables.Dispose();
	}

	private void CreateBuilding(BuildingViewModel buildingViewModel)
	{
			// создаём уровень.
		var buildingLevel = buildingViewModel.Level.CurrentValue;
		var buildingType = buildingViewModel.TypeId;        // кэшируем тип по ID.
			// формируем путь для префаба. Также можно вытащить из buildingViewModel.
		var prefabBuildingLevelPath = $"Prefabs/Gameplay/Buildings/Building_{buildingType}_{buildingLevel}";
		var buildingPrefab = Resources.Load<BuildingBinder>(prefabBuildingLevelPath);	// загружаем префаб в оперативную память.

		var createdBuilding = Instantiate(buildingPrefab);		// создание здания из префаба.
		createdBuilding.Bind(buildingViewModel);                            // присваивание View Model.

		_createdBuildingsMap[buildingViewModel.BuildingEntityId] = createdBuilding;	// заполнение словаря.
	}

	private void DestroyBuilding(BuildingViewModel buildingViewModel)
	{
		if(_createdBuildingsMap.TryGetValue(buildingViewModel.BuildingEntityId, out var buildingBinder))
		{	// если поиск по ключу оказался удачным.
			Destroy(buildingBinder.gameObject);		// удаление всего объекта.
			_createdBuildingsMap.Remove(buildingViewModel.BuildingEntityId);	// удаление элемента массива.
		}
	}
}
