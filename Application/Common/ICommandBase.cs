using ErrorOr;
using MediatR;

namespace Application.Common;

public interface ICommand : IRequest, ICommandBase 
{
    
}

public interface ICommand<TResponse> : IRequest<ErrorOr<ICommandBase>>, ICommandBase
{
    
}

public interface ICommandBase
{
    
}