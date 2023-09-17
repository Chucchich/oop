class Person
{
    private string address;
    private double salary;
    private string name;
    public Person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }

    public double Salary
    {
        get
        {
            return salary;
        }
        set
        {
            salary = value;
        }
    }
    public static Person inputPersonInfo(string name, string address, string sSalary)
    {
        double salary;
        if (string.IsNullOrWhiteSpace(sSalary))
        {
            throw new Exception("Lương không được để rỗng!");
        }
        if (!double.TryParse(sSalary, out salary))
        {
            throw new Exception("Lương phải là một số.");
        }
        if (salary <= 0)
        {
            throw new Exception("Lương phải lớn hơn 0.");
        }
        return new Person(name, address, salary);
    }
    public static void displayPersonInfo(Person person)
    {
        Console.WriteLine("Thông tin của người:");
        Console.WriteLine("Tên: " + person.Name);
        Console.WriteLine("Địa chỉ: " + person.Address);
        Console.WriteLine("Lương: " + person.Salary);
    }
    public static Person[] sortBySalary(Person[] persons)
    {
        int n = persons.Length;
        bool swapped;

        do
        {
            swapped = false;
            for (int i = 1; i < n; i++)
            {
                if (persons[i - 1].Salary > persons[i].Salary)
                {
                    // Hoán đổi hai phần tử
                    Person temp = persons[i - 1];
                    persons[i - 1] = persons[i];
                    persons[i] = temp;
                    swapped = true;
                }
            }
            n--;
        } while (swapped);

        return persons;
    }
}
class Program
{
    static void Main()
    {
        Person[] persons = new Person[3];

        try
        {
            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Nhập thông tin người thứ " + (i + 1) + ":");
                Console.Write("Tên: ");
                string name = Console.ReadLine();
                Console.Write("Địa chỉ: ");
                string address = Console.ReadLine();
                Console.Write("Lương: ");
                string sSalary = Console.ReadLine();

                persons[i] = Person.inputPersonInfo(name, address, sSalary);
            }

            Console.WriteLine("\nDanh sách thông tin người sau khi sắp xếp theo lương:");

            persons = Person.sortBySalary(persons);

            foreach (Person person in persons)
            {
                Person.displayPersonInfo(person);
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi: " + ex.Message);
        }
    }
}
