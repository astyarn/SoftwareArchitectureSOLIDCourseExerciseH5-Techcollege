
namespace SoftwareArchitectureSOLID
{
    public abstract class Person
    {
        public string Name { get; set; }
        public string Address { get; set; }

        protected Person(string name, string address)   //only accessible from this class and derived classes
        {
            Name = name;
            Address = address;
        }

        public abstract void DisplayInfo();
    }

    public interface IStudent
    {
        void EnrollInSubject(string subject);
        void UnenrollFromSubject(string subject);
        void SetGrade(string subject, int grade);
        double CalculateAverageGrade();
    }

    public class Student : Person, IStudent
    {
        public string Subject { get; set; }
        private Dictionary<string, int> SubjectsAndGrades = new();  //Connect Subjects with grades

        public Student(string name, string address, string studentSubject) : base(name, address)
        {
            Subject = studentSubject;
        }

        public void EnrollInSubject(string subject)
        {
            if (!SubjectsAndGrades.ContainsKey(subject))    //Only add if the student doesnt already exists
                SubjectsAndGrades[subject] = 0;
        }

        public void UnenrollFromSubject(string subject)
        {
            if (SubjectsAndGrades.ContainsKey(subject))     //if exists do
                SubjectsAndGrades.Remove(subject);
        }

        public void SetGrade(string subject, int grade)
        {
            if (SubjectsAndGrades.ContainsKey(subject))     
                SubjectsAndGrades[subject] = grade;
        }

        public double CalculateAverageGrade()
        {
            if (SubjectsAndGrades.Count == 0) return 0;

            double total = 0;
            foreach (var grade in SubjectsAndGrades.Values)
            {
                total += grade;
            }
            return total / SubjectsAndGrades.Count;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Student: {Name}, Address: {Address}, Class: {Subject}, Average Grade: {this.CalculateAverageGrade()}");
        }
    }

    public interface ITeacher
    {
        void AssignSubject(string subject);
        void RemoveSubject(string subject);
    }

    public class Teacher : Person, ITeacher
    {
        public decimal MonthlySalary { get; set; }
        private List<string> Subjects = new();

        public Teacher(string name, string address, decimal salary) : base(name, address)
        {
            MonthlySalary = salary;
        }

        public void AssignSubject(string subject)
        {
            if (!Subjects.Contains(subject))
                Subjects.Add(subject);
        }

        public void RemoveSubject(string subject)
        {
            if (Subjects.Contains(subject))
                Subjects.Remove(subject);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Teacher: {Name}, Address: {Address}, Salary: {MonthlySalary:C}");
        }
    }

    public interface IAdministrativeStaff
    {
        void AssignStudent(Student student);
        void RemoveStudent(Student student);
    }

    public class AdministrativeStaff : Person, IAdministrativeStaff
    {
        public decimal MonthlySalary { get; set; }
        private List<Student> ResponsibleStudents = new();

        public AdministrativeStaff(string name, string address, decimal salary) : base(name, address)
        {
            MonthlySalary = salary;
        }

        public void AssignStudent(Student student)
        {
            if (!ResponsibleStudents.Contains(student))
                ResponsibleStudents.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (ResponsibleStudents.Contains(student))
                ResponsibleStudents.Remove(student);
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Admin Staff: {Name}, Address: {Address}, Salary: {MonthlySalary:C}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student("Anna", "Aalborgvej 3", "Grundforløb 1");
            student.EnrollInSubject("Math");
            student.SetGrade("Math", 12);
            student.DisplayInfo();

            var teacher = new Teacher("Bo", "Nørresundbyvej 4", 25000);
            teacher.AssignSubject("Math");
            teacher.DisplayInfo();

            var admin = new AdministrativeStaff("Klaus", "Odensesvej 5", 35000);
            admin.AssignStudent(student);
            admin.DisplayInfo();

            var student2 = new Student("Erik", "Aalborgvej 13", "Grundforløb 2");
            student2.EnrollInSubject("Math");
            student2.EnrollInSubject("Dansk");
            student2.SetGrade("Math", 12);
            student2.SetGrade("Dansk", 10);
            student2.DisplayInfo();
        }
    }
}
