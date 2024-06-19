using MediatR;

namespace Application.Common;

public interface ICommand : IRequest, ICommandBase 
{
    
}

public interface ICommand<TResponse> : IRequest<ICommandBase>, ICommandBase
{
    
}

public interface ICommandBase
{
    
}