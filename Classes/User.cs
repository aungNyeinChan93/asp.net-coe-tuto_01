namespace asp_tuto_01.Classes
{
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public User(string name ,string email, int age) 
        {
            this.Name = name;
            this.Email = email;
            this.Age = age;
        }
    }
}
