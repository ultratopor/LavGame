using System.Linq;
using LavGame.Scripts.Game.State.Entities;
using ObservableCollections;
using R3;

namespace LavGame.Scripts.Game.State.Maps
{
	public class Map
	{
		public ObservableList<Entity> Entities { get; } = new();
		public MapData Origin { get; }
		public int Id => Origin.Id;

		public Map(MapData mapData)
		{
			/* проходимся по сущностям в оригинальном состоянии (MapState), и для каждого состояния добавляем такое же в заместителе
			 * (proxy), и закидываем его  в ObservableList */
			Origin = mapData;

			mapData.Entities.ForEach(entityData => Entities.Add(EntitiesFactory.CreateEntity(entityData)));

			Entities.ObserveAdd().Subscribe(e =>       // е - это аргумент, а не объект.
			{       // подписка на добавление элемента в список.
				var addedEntity = e.Value;            // это уже объект, добавляемый ы список.

				// в оригинальное состояние в список строений добавляем новый элемент, который собираем из прокси.
				mapData.Entities.Add(addedEntity.Origin);
			});

			Entities.ObserveRemove().Subscribe(e =>
			{       // когда удаляется объект.
				var removedEntity = e.Value;
				// ищем в списке конкретный элемент.
				var removedEntityData = mapData.Entities.FirstOrDefault(b=> b.UniqueId == removedEntity.UniqueId);
				mapData.Entities.Remove(removedEntityData);      // если он существует, то удаляем его.
			});
		}
	}
}
