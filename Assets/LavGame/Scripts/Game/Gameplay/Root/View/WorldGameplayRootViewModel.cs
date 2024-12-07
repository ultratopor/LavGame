using ObservableCollections;

public class WorldGameplayRootViewModel
{
	public readonly IObservableCollection<BuildingViewModel> AllBuildings;  // реактивный список View Model.

	public WorldGameplayRootViewModel(BuildingsService buildingsService)
	{
		AllBuildings = buildingsService.AllBuildings;
	}
}
