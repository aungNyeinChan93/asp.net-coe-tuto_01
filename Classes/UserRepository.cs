

namespace asp_tuto_01.Classes
{
    public class UserRepository
    {
        private List<User> _users =
            [
                new ("aung","aung@123",32) ,
                new ("koko","koko@123",22) ,
                new ("susu","susu@123",19) ,
            ];

        public List<User> GetAllUsers() => this._users;

        public void SetUser(User user) => this._users.Add(user);
    }
}
