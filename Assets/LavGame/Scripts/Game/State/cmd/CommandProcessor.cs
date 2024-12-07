using System;
using System.Collections.Generic;

public class CommandProcessor : ICommandProcessor
{
	// обработчики храним в словаре как object.
	private readonly Dictionary<Type, object> _handlesMap = new();
	private readonly IGameStateProvider _gameStateProvider;		// для сохранения.
	public CommandProcessor(IGameStateProvider gameStateProvider)
	{
		_gameStateProvider = gameStateProvider;
	}

	public bool Process<TCommand>(TCommand command) where TCommand : ICommand
	{
		if(_handlesMap.TryGetValue(typeof(TCommand), out var handler))
		{       // пытаемся достать обработчик из словаря по ключу.
			// конвертируем явно обработчика из object в наследника ICommandHandler.
			var typedHandler = (ICommandHandler<TCommand>)handler;
			var result = typedHandler.Handle(command);		// заставляем обработчика обработать.

			if(result)
			{		// автоматическое сохранение игры.
				_gameStateProvider.SaveGameState();
			}

			return result;
		}

		return false;		// если не нашёлся обработчик.
	}

	public void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
	{
		_handlesMap[typeof(TCommand)] = handler;	// регистрируем обработчик, который нам прислали, в словаре.
		// если в словаре с таким ключом уже есть информация, то она перезапишется. В противном случае нужно использовать метод Add.
	}
}
