using AutoService.Models.Assets.Contracts;
using AutoService.Models.Common.Contracts;
using AutoService.Models.Vehicles.Contracts;

namespace AutoService.Core.Contracts
{
    public interface IStockManager
    {
        void AddStockToWarehouse(IStock stock/*, ICounterparty supplier*/);
        void AddStockToWarehouse(IStock stock, IEmployee employee);
        void AddStockToClient(IStock stock, IEmployee employee, IClient client, IVehicle vehicle/*, IDatabase database*/);
        void RemoveStockFromWarehouse(IStock stock, IEmployee employee);
        bool ConfirmStockExists(string stockUniqueNumber, IEmployee employee);
        string PrintAvailableStock();
    }
}