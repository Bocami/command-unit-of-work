using System;
using Bocami.Practices.AbstractFactory;
using Bocami.Practices.Decorator;
using Bocami.Practices.UnitOfWork;

namespace Bocami.Practices.Command.UnitOfWork
{
    public class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>, IDecorator<ICommandHandler<TCommand>>
            where TCommand : class, ICommand
    {
        private readonly ICommandHandler<TCommand> commandHandler;
        private readonly IAbstractFactory<IUnitOfWork> unitOfWorkFactory;

        public UnitOfWorkCommandHandlerDecorator(ICommandHandler<TCommand> commandHandler, IAbstractFactory<IUnitOfWork> unitOfWorkFactory)
        {
            if (commandHandler == null)
                throw new ArgumentNullException("commandHandler");

            if (unitOfWorkFactory == null)
                throw new ArgumentNullException("unitOfWorkFactory");

            this.commandHandler = commandHandler;
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Handle(TCommand command)
        {
            var unitOfWork = unitOfWorkFactory.Create();
            
            commandHandler.Handle(command);

            unitOfWork.Commit();
        }
    }
}
