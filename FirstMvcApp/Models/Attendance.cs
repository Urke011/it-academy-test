namespace FirstMvcApp.Models
{
    public class Attendance
    {
        private static List<string> attendants = new List<string>();

        public static void AddAttendant(Person person)
        {
            person.InsertPerson();
        }

        public static List<Person> GetAttendants()
        {
            List<Person> persons = Person.FetchAllPersons();
            return persons;
        }

    }

}
