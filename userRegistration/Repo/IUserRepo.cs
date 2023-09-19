using userRegistration.Model;

namespace userRegistration.Repo
{
    public interface IUserRepo
    {
        Task<List<User>> GetAll();

        Task<User> GetUserById(int id);

        Task<string> Create(User user);

        Task<string> Update(int id, User user);

        Task<string> Remove(int id);
    }
}
