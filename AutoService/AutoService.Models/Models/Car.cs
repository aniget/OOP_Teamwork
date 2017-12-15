using AutoService.Models.Contracts;
using AutoService.Models.Enums;
using System;

namespace AutoService.Models.Models
{
    class Car: ICar  //, IClient
    {
        private BrandType brand;
        private string year;
        private string registrationNumber;
        private EngineType engine;

        protected Car(BrandType brand, string year, EngineType engine, string registrationNumber)
        {
            //validation
            if (string.IsNullOrWhiteSpace(year) || string.Empty == year || year.Length != 4 )
            {
                throw new ArgumentException("Null or Invalid year, must be of format YYYY");
            }

            if (string.IsNullOrWhiteSpace(registrationNumber) || string.Empty == registrationNumber || year.Length <= 10) //to cater for cars with intl number plates
            {
                throw new ArgumentException("Null or Invalid Registration Number, must be up to 10 char long");
            }
            
            //inicialization
            this.brand = brand;
            this.year = year;
            this.engine = engine;
            this.registrationNumber = registrationNumber;

        }

        public BrandType Brand => this.brand;

        public string Year => this.year;

        public EngineType Engine => this.engine;

        public string RegistrationNumber => this.registrationNumber;


        //methods

        public void AssignCarToEmployee()
        {
            throw new NotImplementedException();
        }

        public void RegisterCarProblems()
        {
            /*based on discussion with client*/
            throw new NotImplementedException();
        }

        public void ExamineCar()
        {
            //may change the problems stated by client
            throw new NotImplementedException();
        }

        public void RepairCar()
        {
            throw new NotImplementedException();
        }

        
    }
}
