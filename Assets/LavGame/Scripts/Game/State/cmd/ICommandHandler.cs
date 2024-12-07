// универсальный (generic) интерфейс, который може быть только у наследников ICommand
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
	bool Handle(TCommand command);// метод может завершиться успешно или неуспешно. Внутри может быть любой наследник ICommand
}
