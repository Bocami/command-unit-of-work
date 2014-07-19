using Bocami.Practices.Decorator;
using Bocami.Practices.UnitOfWork;
using System;

namespace Bocami.Practices.Command.UnitOfWork
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>, IDecorator<ICommandHandler<TCommand>>
            where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
        {
            if (commandHandler == null)
                throw new ArgumentNullException("commandHandler");

            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            this.commandHandler = commandHandler;
            this.unitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            commandHandler.Handle(command);

            unitOfWork.Commit();
        }
    }
}
