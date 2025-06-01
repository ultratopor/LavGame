using BaCon;

namespace Assets.LavGame.Scripts.MVVM.UI
{
	/// <summary>
	/// Знает, как создавать ViewModel для окон
	/// </summary>
	public abstract class UIManager
	{
		protected readonly DIContainer Container; // чтобы вытаскивать барахло и собирать ViewModel окошек.

		protected UIManager(DIContainer container) => Container = container;

	}
}
