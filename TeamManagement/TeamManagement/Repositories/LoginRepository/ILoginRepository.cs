using System.Threading.Tasks;

namespace TeamManagement.Repositories.LoginRepository
{
    public interface ILoginRepository
    {
        Task<object> LoginAsync(string email);
    }
}
