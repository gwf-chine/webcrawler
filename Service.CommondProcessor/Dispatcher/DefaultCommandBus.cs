﻿
using System.Collections.Generic;
using System.Web.Mvc;
using Service.CommandProcessor.Command;
using Service.Core.Common;
namespace Service.CommandProcessor.Dispatcher
{
    public class DefaultCommandBus : ICommandBus
    {
        public ICommandResult Submit<TCommand>(TCommand command) where TCommand: ICommand
        {    
            var handler = DependencyResolver.Current.GetService<ICommandHandler<TCommand>>();
            if (!((handler != null) && handler is ICommandHandler<TCommand>))
            {
                throw new CommandHandlerNotFoundException(typeof(TCommand));
            }  
            return handler.Execute(command);
 
        }
        public IEnumerable<ValidationResult> Validate<TCommand>(TCommand command) where TCommand : ICommand
        {
            var handler = DependencyResolver.Current.GetService<IValidationHandler<TCommand>>();
            if (!((handler != null) && handler is IValidationHandler<TCommand>))
            {
                throw new ValidationHandlerNotFoundException(typeof(TCommand));
            }  
            return handler.Validate(command);
        }
    }
}

