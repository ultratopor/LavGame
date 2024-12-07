using System;
using System.Collections.Generic;

using Commands;

using ObservableCollections;

using R3;

using Settings.Gameplay.Buildings;

using UnityEngine;

// сервисы являются прослойкой между View моделью и обработчиком команд. Этот посылает сигналы в обработчик команд на
// размещение здания, перемещение и удаление.
public class BuildingsService
{
	private readonly ICommandProcessor _cmd;

	// ключом будет Building Entity ID.
	private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();

	// создаём динамический массив и добавляем его в публичное свойство, потому что интерфейсы нельзя добавлять
	// в массивы через Add/Remove. Реактивный массив нужен, чтобы можно было подписываться на него.
	private readonly ObservableList<BuildingViewModel> _allBuildings = new();

	// создаём динамический массив настроек здания по TypeId
	private readonly Dictionary<string, BuildingSettings> _buildingSettingsMap = new();
	public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;

	/* buildings - реактивное состояние посредник, cmd - отсюда можно посылать команды для изменения состояния.
	* сервис не меняет состояния. Это могут делать только команды. 
	* Принимает реактивный список состояний строений. На основании него создается словарь View Model, обновляющийся 
	* автоматически согласно подписке.*/
	public BuildingsService(IObservableCollection<BuildingEntityProxy> buildings, BuildingsSettings buildingsSettings, ICommandProcessor cmd)
	{
		this._cmd = cmd;

		foreach(var buildingSettings in buildingsSettings.AllBuildings)
		{       // кэшируем настройки зданий.
			_buildingSettingsMap[buildingSettings.TypeId] = buildingSettings;
		}

		// создание View Model на каждое актуальное состояние.
		foreach(var buildingEntity in buildings)
		{
			CreateBuildingViewModel(buildingEntity);
		}

		// создание View Model при обновлении подписки.
		buildings.ObserveAdd().Subscribe(e =>
		{
			CreateBuildingViewModel(e.Value);
		});

		// удаление View Model при обновлении подписки.
		buildings.ObserveRemove().Subscribe(e =>
		{
			RemoveBuildingViewModel(e.Value);
		});
	}

	public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
	{       // размещает здание. bool - чтобы View Models могли видеть результат перемещения.
			// buildingTypeId - тип здания.
		var command = new CmdPlaceBuilding(buildingTypeId, position);
		var result = _cmd.Process(command);

		return result;
	}

	public bool MoveBuilding(int buildingEntityId, Vector3Int newPosition)
	{       // перемещает здание. buildingTypeId - идентификатор здания.
		throw new NotImplementedException();
	}

	public bool DeleteBuilding(int BuildingEntityId)
	{       // удаляет здание.
		throw new NotImplementedException();
	}

	private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
	{       // создаёт слой посредника посредника (Proxy) с данными здания и добавляет это в динамический массив.
		// кэшируем настройку по ключу.
		var buildingSettings = _buildingSettingsMap[buildingEntity.TypeId];
		var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

		_allBuildings.Add(buildingViewModel);
		_buildingsMap[buildingEntity.Id] = buildingViewModel;   // заполнение словаря View Model по ключу - ID.
	}

	private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
	{
		if(_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
		{
			_allBuildings.Remove(buildingViewModel);
			_buildingsMap.Remove(buildingEntity.Id);
		}
	}
}
