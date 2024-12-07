using R3;
using UnityEngine;

/* заворачиваем в прокси все оригинальные состояния, чтоб можно было подписаться и реагировать на них.
 * в конструкторе подписываемся на изменения состояния прокси, чтобы эти состояния из прокси передалось в оригинальные состояния.
 * Поэтому в BuildingEntity всегда будут актуальные данные. Не нужно будет их насильно актуализировать - всё само.
 */
/// <summary>
/// Замещающее состояние здания
/// </summary>
public class BuildingEntityProxy
{
		// это статические данные. Создаются однажды.
	public int Id { get; }
	public string TypeId { get; }
	public BuildingEntity Origin { get; }	// ссылка на оригинальное состояние.

		// это динамические данные. Можно на них подписываться.
	public ReactiveProperty<Vector3Int> Position { get; }
	public ReactiveProperty<int> Level { get; }

	public BuildingEntityProxy(BuildingEntity buildingEntity)
	{
		Origin = buildingEntity;
		Id = buildingEntity.Id;
		TypeId = buildingEntity.TypeId;
			// через реактивщину можно подписаться на изменения компонентов.
		Position = new ReactiveProperty<Vector3Int>(buildingEntity.Position);       // buildingEntity.Position - начальная точка.
		Level = new ReactiveProperty<int>(buildingEntity.Level);
			// меняем значение поля в оригинальном классе через подписку на это значение.
		Position.Skip(1).Subscribe(value => buildingEntity.Position = value);   // пропускаем первое значение при подписке, чтоб не среагировало на подписку.
		Level.Skip(1).Subscribe(value => buildingEntity.Level = value);
	}
}

