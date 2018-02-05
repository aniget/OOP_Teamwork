using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Core.Contracts
{
    public interface IStockManager
    {
        //IWarehouse Warehouse { get;  }
        void AddStockToWarehouse(IStock stock/*, IWarehouse warehouse*//*, ICounterparty supplier*/);
        void AddStockToWarehouse(IStock stock, IEmployee employee/*, IWarehouse warehouse*/);
        void AddStockToClient(IStock stock, IEmployee employee, IClient client, IVehicle vehicle/*, IDatabase database*/);
        void RemoveStockFromWarehouse(IStock stock, IEmployee employee/*, IWarehouse warehouse*/);
        bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee/*, IWarehouse warehouse*/);
    }
}