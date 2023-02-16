public interface IUserRepository
    {
        UserEntity? GetByUsernameAndPassword(string username, string password);
    }
    public class UserRepository : IUserRepository
    {
        private List<UserEntity> users = new()
        {
            new UserEntity(1, "admin", "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", "gray", "admin"),
            new UserEntity(2, "user", "BPiZbadjt6lpsQKO4wB1aerzpjVIbdqyEdUSyFud+Ps=", "black", ""),
        };

        public UserEntity? GetByUsernameAndPassword(string username, string password)
        {
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password.Sha256());
            return user;
        }
    }