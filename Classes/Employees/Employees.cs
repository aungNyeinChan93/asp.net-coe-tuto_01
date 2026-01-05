namespace asp_tuto_01.Classes.Employees
{
    public class Employees
    {
    }

    public class Employe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Salary { get; set; }

        public string Position { get; set; }

        public Employe()
        {

        }

        public Employe(int id, string name, int salary ,string position)
        {
            this.Id = id;
            this.Name = name;
            this.Salary = salary;
            this.Position = position;
        }
    }

    static class EmployeRepository
    {
        private static List<Employe> _employees = new List<Employe>()
        {
            new (1,"John",3000,"Software Engineer"),
            new (2,"Mike",3000,"It Engineer"),
            new (3,"Nancy",2000,"Sale & marketing"),
            new (4,"Doery",4000,"Project Manager"),
        };

        public static List<Employe> GetAllEmployees() => _employees;
        public static void AddEmployee(Employe employee) => _employees.Add(employee);
    }
}
