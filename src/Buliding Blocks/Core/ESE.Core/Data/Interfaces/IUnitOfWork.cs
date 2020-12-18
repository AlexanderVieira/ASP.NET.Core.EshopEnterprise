using System.Threading.Tasks;

namespace ESE.Core.Data.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
