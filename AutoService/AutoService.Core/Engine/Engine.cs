using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using AutoService.Core.Factory;
using AutoService.Models.Assets;
using AutoService.Models.Assets.Contracts;
using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Contracts;

namespace AutoService.Core
{
    public sealed class Engine : IEngine
    {
        //TODO CHECK WHOLE DOCUMENT FOR REPETITIVE CODE

        private readonly IList<IEmployee> employees;
        private readonly ICollection<IAsset> bankAccounts;
        private readonly ICollection<ICounterparty> clients;
        private readonly ICollection<ICounterparty> vendors;
        private readonly IDictionary<IClient, List<ISell>> notInvoicedSells;

        private DateTime lastInvoiceDate = DateTime.ParseExact("2017-01-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private int lastInvoiceNumber = 0;
        private IAutoServiceFactory factory;


        private static readonly IEngine SingleInstance = new Engine();

        private Engine()
        {
            this.factory = new AutoServiceFactory();
            this.employees = new List<IEmployee>();
            this.bankAccounts = new List<IAsset>();
            this.clients = new List<ICounterparty>();
            this.vendors = new List<ICounterparty>();
            this.notInvoicedSells = new Dictionary<IClient, List<ISell>>();
        }

        public static IEngine Instance
        {
            get
            {
                return SingleInstance;
            }
        }

      
        public void Run()
        {
            var command = ReadCommand();
            var commandParameters = new string[] { string.Empty };

            while (command != "exit")
            {
                commandParameters = ParseCommand(command);
                try
                {
                    ExecuteSingleCommand(commandParameters);
                }
                catch (NotSupportedException e)
                {
                    Console.Write(e.Message);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
                command = ReadCommand();
            }
        }

        private string ReadCommand()
        {
            return Console.ReadLine();
        }

        private string[] ParseCommand(string command)
        {
            return command.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ExecuteSingleCommand(string[] commandParameters)
        {
            var commandType = commandParameters[0];
            int employeeId;
            decimal ratePerMinute;
            IEmployee employee = null;
            string position;
            DepartmentType department;

            switch (commandType)
            {
                case "showEmployees":

                    if (this.employees.Count == 0)
                    {
                        throw new ArgumentException("There are no employees! Hire them!");
                    }
                    this.ShowEmployees();
                    break;
                case "hireEmployee":
                    if (commandParameters.Length != 7)
                    {
                        throw new NotSupportedException(
                            "Employee constructor with less or more than 6 parameters not supported.");
                    }
                    var firstName = commandParameters[1];
                    var lastName = commandParameters[2];
                    position = commandParameters[3];
                    decimal salary = decimal.TryParse(commandParameters[4], out salary)
                        ? salary
                        : throw new ArgumentException("Please provide a valid decimal value for salary!");
                    ratePerMinute = decimal.TryParse(commandParameters[5], out ratePerMinute)
                        ? ratePerMinute
                        : throw new ArgumentException("Please provide a valid decimal value for rate per minute!");
                    ;

                    if (!Enum.TryParse(commandParameters[6], out department))
                    {
                        throw new ArgumentException("Department not valid!");
                    }

                    this.AddEmployee(firstName, lastName, position, salary, ratePerMinute, department);
                    break;
                case "fireEmployee":
                    if (commandParameters.Length != 2)
                    {
                        throw new NotSupportedException(
                            "Fire employee command must be with only 2 parameters!");
                    }

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException($"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }
                    
                 employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");
                    this.FireEmployee(employee);
                    break;
                case "changeEmployeeRate":
                    if (commandParameters.Length != 3)
                    {
                        throw new NotSupportedException(
                            "Change employer rate command must be with only 3 parameters!");
                    }

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException($"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    ratePerMinute = decimal.TryParse(commandParameters[2], out ratePerMinute)
                        ? ratePerMinute
                        : throw new ArgumentException("Please provide a valid decimal value for rate per minute!");
                    
                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    this.ChangeRateOfEmployee(employee, ratePerMinute);
                    break;

                case "issueInvoices":
                    this.IssueInvoices();
                    break;

                case "showAllEmployeesAtDepartment":
                    if (commandParameters.Length != 2)
                    {
                        throw new NotSupportedException(
                            "Show Employees At Department command must be with only 2 parameters!");
                    }

                    if (!Enum.TryParse(commandParameters[1], out department))
                    {
                        throw new ArgumentException("Department not valid!");
                    }

                    this.ListEmployeesAtDepartment(department);
                    break;

                case "changeEmployeePosition":
                    if (commandParameters.Length != 3)
                    {
                        throw new NotSupportedException(
                            "Change employer position command must be with only 3 parameters!");
                    }

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException($"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    position = commandParameters[2];
                    this.ChangePositionOfEmployee(employee, position);
                    break;

                case "addEmpoloyeeResponsibility":

                    if (commandParameters.Length < 3)
                    {
                        throw new NotSupportedException(
                            "Add responsibility command must be at least 3 parameters!");
                    }

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException($"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                   var responsibilitiesToAdd = commandParameters.Skip(2).ToArray();
                    this.AddResponsibilitiesToEmployee(employee, responsibilitiesToAdd);

                    break;

                case "removeEmpoloyeeResponsibility":

                    if (commandParameters.Length < 3)
                    {
                        throw new NotSupportedException(
                            "Remove responsibility command must be at least 3 parameters!");
                    }

                    if (this.employees.Count == 0)
                    {
                        throw new InvalidOperationException("No employees currently in the service!");
                    }

                    employeeId = int.TryParse(commandParameters[1], out employeeId)
                        ? employeeId
                        : throw new ArgumentException("Please provide a valid integer value for employee Id!");

                    if (employeeId <= 0)
                    {
                        throw new ArgumentException($"Please provide a valid employee value, i.e. between 1 and {this.employees.Count}!");
                    }

                    employee = this.employees.Count >= employeeId
                        ? this.employees[employeeId - 1]
                        : throw new ArgumentException("This employee does not exist!");

                    var responsibilitiesToRemove = commandParameters.Skip(2).ToArray();
                    this.RemoveResponsibilitiesToEmployee(employee, responsibilitiesToRemove);

                    break;

                default:
                    throw new InvalidOperationException();
            }
        }

        private void RemoveResponsibilitiesToEmployee(IEmployee employee, string[] responsibilitiesToRemove)
        {
            var responsibilitesToAdd = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilitiesToRemove)
            {
                ResponsibilityType currentResponsibility;
                if (!Enum.TryParse(responsibility, out currentResponsibility))
                {
                    throw new ArgumentException($"Responsibility {responsibility} not valid!");

                }
                responsibilitesToAdd.Add(currentResponsibility);
            }
            employee.RemoveResponsibilities(responsibilitesToAdd);
        }

        private void AddResponsibilitiesToEmployee(IEmployee employee, string[] responsibilities)
        {
            var responsibilitesToAdd = new List<ResponsibilityType>();
            foreach (var responsibility in responsibilities)
            {
                ResponsibilityType currentResponsibility;
                if (!Enum.TryParse(responsibility, out currentResponsibility))
                {
                    throw new ArgumentException($"Responsibility {responsibility} not valid!");
                }
                responsibilitesToAdd.Add(currentResponsibility);
            }
            employee.AddResponsibilities(responsibilitesToAdd);
        }

        private void ChangePositionOfEmployee(IEmployee employee, string position)
        {
            employee.ChangePosition(position);

            Console.WriteLine($"Position of employee {employee.FirstName} {employee.LastName} was successfully set to {position}");
        }

        private void ListEmployeesAtDepartment(DepartmentType department)
        {
            if (this.employees.Any(x => x.Department != department))
            {
                throw new ArgumentException($"The are no employees at department: {department}!");
            }

            StringBuilder str = new StringBuilder();

            str.AppendLine($"Employees at: {department} department:");
            foreach (var employee in employees)
            {
                var counter = 1;
                if (employee.Department == department)
                {

                    str.AppendLine($"{counter}. {employee.ToString()}");
                    counter++;
                    //TODO ADD DISTINGUISHER BETWEEN LINES (#########?)
                }
            }
            Console.WriteLine(str.ToString());
        }

        private void ChangeRateOfEmployee(IEmployee employee, decimal ratePerMinute)
        {
            employee.ChangeRate(ratePerMinute);
            Console.WriteLine($"Rate per minute of employee {employee.FirstName} {employee.LastName} was successfully set to {ratePerMinute} $");
        }

        private void ShowEmployees()
        {
            int counter = 1;
            foreach (var currentEmployee in this.employees)
            {   
                Console.WriteLine(counter + ". " +  currentEmployee.ToString());
                counter++;
            }
        }

        private void IssueInvoices()
        {
            int invoiceCount = 0;
            foreach (var client in this.notInvoicedSells.OrderBy(o => o.Key.Name))
            {
                this.lastInvoiceNumber++;
                invoiceCount++;
                string invoiceNumber = this.lastInvoiceNumber.ToString().PadLeft(10, '0');
                this.lastInvoiceDate.AddDays(1);
                IInvoice invoice = new Invoice(invoiceNumber, this.lastInvoiceDate, client.Key);

                foreach (var sell in client.Value)
                {
                    invoice.InvoiceItems.Add(sell);
                }
                var clientToAddInvoice = this.clients.FirstOrDefault(f => f.UniqueNumber == client.Key.UniqueNumber);
                clientToAddInvoice.Invoices.Add(invoice);
            }

            this.notInvoicedSells.Clear();
            Console.WriteLine($"{invoiceCount} invoices were successfully issued!");
        }

        private void FireEmployee(IEmployee employee)
        {
            employee.FireEmployee();

            Console.WriteLine($"Employee {employee.FirstName} {employee.LastName} was fired!");
        }

        private void AddEmployee(string firstName, string lastName, string position, decimal salary,
            decimal ratePerMinute, DepartmentType department)
        {
            IEmployee employee =
                this.factory.CreateEmployee(firstName, lastName, position, salary, ratePerMinute, department);

            this.employees.Add(employee);
            Console.WriteLine(employee);
            Console.WriteLine($"Employee {firstName} {lastName} added successfully with Id {this.employees.Count}");
        }
    }
}