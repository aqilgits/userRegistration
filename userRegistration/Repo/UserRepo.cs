using userRegistration.Model;
using userRegistration.Model.Data;
using Dapper;
using System.Data;

namespace userRegistration.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly DapperDBContext context;
        public UserRepo(DapperDBContext context) 
        {
            this.context = context; 
        }

        public async Task<string> Create(User user)
        {
            string response = string.Empty;
            string query = "Insert into [users] (username, email, phone, skillsets, hobby) values(@username,@email,@phone,@skillsets,@hobby)";
            var parameter = new DynamicParameters();
            parameter.Add("username", user.username, DbType.String);
            parameter.Add("email", user.email, DbType.String);
            parameter.Add("phone", user.phone, DbType.String);
            parameter.Add("skillsets", user.skillsets, DbType.String);
            parameter.Add("hobby", user.hobby, DbType.String);
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameter);
                response = "User was created";
            }
            return response;
        }

        public async Task<List<User>> GetAll()
        {
            string query = "Select * from [users]";
            using(var connection=this.context.CreateConnection())
            {
                var userList = await connection.QueryAsync<User>(query);
                return userList.ToList();
            }
        }

        public async Task<User> GetUserById(int id)
        {
            string query = "Select * from [users] where id=@id";
            using (var connection = this.context.CreateConnection())
            {
                var userList = await connection.QueryFirstOrDefaultAsync<User>(query,new {id});
                return userList;
            }
        }

        public async Task<string> Remove(int id)
        {
            string response = string.Empty;
            string query = "delete * from [users] where id=@id";
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new {id});
                response = "User was deleted";  
            }
            return response;
        }

        public async Task<string> Update(int id, User user)
        {
            string response = string.Empty;
            string query = "Update [users] set username=@username, email=@email, phonr=@phone, skillsets=@skillsets, hobby=@hobby where userid=@userid";
            var parameter = new DynamicParameters();
            parameter.Add("userid", user.userid, DbType.Int32);
            parameter.Add("username", user.username, DbType.String);
            parameter.Add("email", user.email, DbType.String);
            parameter.Add("phone", user.phone, DbType.String);
            parameter.Add("skillsets", user.skillsets, DbType.String);
            parameter.Add("hobby", user.hobby, DbType.String);
            using (var connection = this.context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameter);
                response = "User was created";
            }
            return response;
        }
    }
}
