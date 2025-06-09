using System;
using System.Collections.Generic;
using ObservableCollections;

using R3;

using Settings.Gameplay.Buildings;

using UnityEngine;

/*// сервисы являются прослойкой между View моделью и обработчиком команд. Этот посылает сигналы в обработчик команд на
// размещение здания, перемещение и удаление.
public class BuildingsService
{
	private readonly ICommandProcessor _cmd;

	// ������ ����� Building Entity ID.
	private readonly Dictionary<int, BuildingViewModel> _buildingsMap = new();

	// ������ ������������ ������ � ��������� ��� � ��������� ��������, ������ ��� ���������� ������ ���������
	// � ������� ����� Add/Remove. ���������� ������ �����, ����� ����� ���� ������������� �� ����.
	private readonly ObservableList<BuildingViewModel> _allBuildings = new();

	// ������ ������������ ������ �������� ������ �� TypeId
	private readonly Dictionary<string, BuildingSettings> _buildingSettingsMap = new();
	public IObservableCollection<BuildingViewModel> AllBuildings => _allBuildings;

	/* buildings - ���������� ��������� ���������, cmd - ������ ����� �������� ������� ��� ��������� ���������.
	* ������ �� ������ ���������. ��� ����� ������ ������ �������. 
	* ��������� ���������� ������ ��������� ��������. �� ��������� ���� ��������� ������� View Model, ������������� 
	* ������������� �������� ��������.#1#
	public BuildingsService(IObservableCollection<BuildingEntityProxy> buildings, BuildingsSettings buildingsSettings, ICommandProcessor cmd)
	{
		this._cmd = cmd;

		foreach(var buildingSettings in buildingsSettings.AllBuildings)
		{       // �������� ��������� ������.
			_buildingSettingsMap[buildingSettings.TypeId] = buildingSettings;
		}

		// �������� View Model �� ������ ���������� ���������.
		foreach(var buildingEntity in buildings)
		{
			CreateBuildingViewModel(buildingEntity);
		}

		// �������� View Model ��� ���������� ��������.
		buildings.ObserveAdd().Subscribe(e =>
		{
			CreateBuildingViewModel(e.Value);
		});

		// �������� View Model ��� ���������� ��������.
		buildings.ObserveRemove().Subscribe(e =>
		{
			RemoveBuildingViewModel(e.Value);
		});
	}

	public bool PlaceBuilding(string buildingTypeId, Vector3Int position)
	{       // ��������� ������. bool - ����� View Models ����� ������ ��������� �����������.
			// buildingTypeId - ��� ������.
		var command = new CmdPlaceBuilding(buildingTypeId, position);
		var result = _cmd.Process(command);

		return result;
	}

	public bool MoveBuilding(int buildingEntityId, Vector3Int newPosition)
	{       // ���������� ������. buildingTypeId - ������������� ������.
		throw new NotImplementedException();
	}

	public bool DeleteBuilding(int BuildingEntityId)
	{       // ������� ������.
		throw new NotImplementedException();
	}

	private void CreateBuildingViewModel(BuildingEntityProxy buildingEntity)
	{       // ������ ���� ���������� ���������� (Proxy) � ������� ������ � ��������� ��� � ������������ ������.
		// �������� ��������� �� �����.
		var buildingSettings = _buildingSettingsMap[buildingEntity.TypeId];
		var buildingViewModel = new BuildingViewModel(buildingEntity, buildingSettings, this);

		_allBuildings.Add(buildingViewModel);
		_buildingsMap[buildingEntity.Id] = buildingViewModel;   // ���������� ������� View Model �� ����� - ID.
	}

	private void RemoveBuildingViewModel(BuildingEntityProxy buildingEntity)
	{
		if(_buildingsMap.TryGetValue(buildingEntity.Id, out var buildingViewModel))
		{
			_allBuildings.Remove(buildingViewModel);
			_buildingsMap.Remove(buildingEntity.Id);
		}
	}
}*/
