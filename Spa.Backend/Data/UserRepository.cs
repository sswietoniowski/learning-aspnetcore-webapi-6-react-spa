public interface IUserRepository
    {
        UserEntity? GetByUsernameAndPassword(string username, string password);
    }
    public class UserRepository : IUserRepository
    {
        private List<UserEntity> users = new()
        {
            new UserEntity(1, "test", "test", "gray", "admin")
        };

        public UserEntity? GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password);
            return user;
        }
    }