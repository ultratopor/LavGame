using System.Linq;
using R3;
using ObservableCollections;

namespace State.Maps
{
	public class Map
	{
		public ObservableList<BuildingEntityProxy> Buildings { get; } = new();
		public MapState Origin { get; }
		public int Id => Origin.Id;

		public Map(MapState mapState)
		{
			/* проходимся по строениям в оригинальном состоянии (MapState), и для каждого состояния добавляем такое же в заместителе
			 * (proxy), и закидываем его  в ObservableList */
			Origin = mapState;

			mapState.Buildings.ForEach(buildingOrigin => Buildings.Add(new BuildingEntityProxy(buildingOrigin)));

			Buildings.ObserveAdd().Subscribe(e =>       // е - это аргумент, а не объект.
			{       // подписка на добавление элемента в список.
				var addedBuildingEntity=e.Value;            // это уже объект, добавляемый ы список.

				// в оригинальное состояние в список сторений добавляем новый элемент, который собираем из прокси.
				mapState.Buildings.Add(addedBuildingEntity.Origin);
			});

			Buildings.ObserveRemove().Subscribe(e =>
			{       // когда удаляется объект.
				var removedBuildingEntityProxy = e.Value;
				// ищем в списке конкретный элемент.
				var removedBuildingEntity = mapState.Buildings.FirstOrDefault(b=>b.Id==removedBuildingEntityProxy.Id);
				mapState.Buildings.Remove(removedBuildingEntity);      // если он существует, то удаляем его.
			});
		}
	}
}
