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
        private readonly IValidateModel modelValidator;

        public Work(IEmployee responsibleEmployee, TypeOfWork job, IValidateModel modelValidator)
        {
            this.responsibleEmployee = responsibleEmployee;
            this.job = job;
            this.modelValidator = modelValidator;
        }

        public IEmployee ResponsibleEmployee
        {
            get => this.responsibleEmployee;
            protected set
            {
                this.ModelValidator.CheckNullObject(value);
                this.responsibleEmployee = value;
            }
        }

        public TypeOfWork Job => this.job;

        public IValidateModel ModelValidator { get; }
    }
}
