using AutoService.Models.BusinessProcess.Contracts;
using AutoService.Models.BusinessProcess.Enums;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Validator;

namespace AutoService.Models.Assets
{
    public abstract class Work : IWork
    {
        private IEmployee responsibleEmployee;
        private readonly TypeOfWork job;

        public Work(IEmployee responsibleEmployee, TypeOfWork job)
        {
            this.ResponsibleEmployee = responsibleEmployee;
            this.job = job;
        }

        public IEmployee ResponsibleEmployee
        {
            get => this.responsibleEmployee;
            protected set
            {
                Validate.CheckNullObject(value);
                this.responsibleEmployee = value;
            }
        }

        public TypeOfWork Job => this.job;

        private void ChangeResponsibleEmployee(IEmployee employee)
        {
            this.ResponsibleEmployee = employee;
        }
    }
}
