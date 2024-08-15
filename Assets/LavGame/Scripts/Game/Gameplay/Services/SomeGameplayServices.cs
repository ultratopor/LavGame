using System;
using UnityEngine;

namespace Services
{// в качестве примера сделано наследование от интерфейса IDisposable, который будет при закрытии отписываться
	public class SomeGameplayServices : IDisposable
	{
		private readonly SomeCommonService _someCommonService;

		public SomeGameplayServices(SomeCommonService someCommonService) 
		{ 
			_someCommonService = someCommonService;
			Debug.Log(GetType().Name+"has been created");
		}

		public void Dispose()
		{
			Debug.Log("Подчистить все подписьки");
		}
	}
}
