using ESE.Core.Messages;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace ESE.Core.Mediator.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T myEvent) where T : Event;
        Task<ValidationResult> SendCommand<T>(T command) where T : Command;
    }
}
