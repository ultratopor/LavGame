// хранит обработчики и принимает команды на выполнение
public interface ICommandProcessor
{
	// регистрация обработчика - наследника ICommand.
	void RegisterHandler<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;

	// возврат результата обработки - наследника ICommand.
	bool Process<TCommand>(TCommand command) where TCommand : ICommand;
}
