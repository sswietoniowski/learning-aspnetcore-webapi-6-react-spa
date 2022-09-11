public interface IUserRepository
    {
        UserEntity? GetByUsernameAndPassword(string username, string password);
    }
    public class UserRepository : IUserRepository
    {
        private List<UserEntity> users = new()
        {
            new UserEntity(1, "admin", "test", "OWY4NmQwODE4ODRjN2Q2NTlhMmZlYWEwYzU1YWQwMTVhM2JmNGYxYjJiMGI4MjJjZDE1ZDZjMTViMGYwMGEwOA==" /* test */, "admin"),
            new UserEntity(2, "user", "test", "OWY4NmQwODE4ODRjN2Q2NTlhMmZlYWEwYzU1YWQwMTVhM2JmNGYxYjJiMGI4MjJjZDE1ZDZjMTViMGYwMGEwOA==" /* test */, ""),
        };

        public UserEntity? GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password.Sha256());
            return user;
        }
    }