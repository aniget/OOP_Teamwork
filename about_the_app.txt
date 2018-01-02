_________________________________________

The Ultimate OldFashioned* AutoService
_________________________________________
* OldFashioned, you may wander?! This is because we use a very old-fashioned console-based computer system, but it's alse very powerfull too!


_________________________________________

Development team:
_________________________________________
Alex: alexparvanov
Angel: aniget
Viacheslav: borschetsky


__________________

The main purpose:
__________________

This console app is made to help all sort of AutoServices (from small 1-2 person to big 40-50 people auto centers) do your main job, 
help their clients have a happy driving experience and leave the rest to the app.

____________________________________

Details about the application:
____________________________________

With this application we can help you with professional repairs of all sort of vehicles. Our system currently supports:
Cars, 
Small Trucks, 
Trucks
In the roadmap we have Planes, Submarines and even AlienShips

All spectrum of Stakeholders
The application provides functionality to register and modify the following stakeholders and their activities:
Employees - register, give rights and responsibilities, change their salaries and ofcourse fire them
Clients - register them and their vehicles, set discount levels and provide reports for the repairs they've made on their vehicles
Suppliers - registration and removal in case they manage poorly with stock delivery

The app can also handle payments! 
We are very strict onvalidating the input, we even validate the IBANs of our clients and suppliers
Partial invoice payments are also available in this version of the application. 

_____________________________________________

Main functions (commands) of the app are:
_____________________________________________

showEmployees
hireEmployee;<firstName>;<lastName>;<position>;<salary>;<ratePerMinute>;<department>;
showAllEmployeesAtDepartment;<department>
fireEmployee;<employeeId>
changeEmployeeRate;<employeeId>;<ratePerMinute>
changeEmployeePosition;<employeeId>;<position>
createBankAccount;<employeeId>;<assetName>;<IBAN> - check validation
addEmployeeResponsibility;<employeeId>;<responsibility>;[<responsibility>;]*
removeEmpoloyeeResponsibility;<employeeId>;<responsibility>;[<responsibility>;]*
depositCashInBank;<bankAccountId>;<depositAmount> - id or IBAN?
registerClient;<name>;<address>;<uniqueNumber>
registerSupplier;<name>;<address>;<uniqueNumber>
removeSupplier - DO WE NEED IT? Usually we don't remove suppliers in order to keep history in database
removeClient - DO WE NEED IT? Usually we don't remove clients in order to keep history in database
changeSupplierName;<uniqueNumber>;<newName>
orderStockToWarehouse - either or? check!
addVehicleToClient;<vehicleType>;<make>;<model>;<uniqueNumber>;<year>;<engineType>;<seats/weightCapacity>;<clientId> - id or name?
sellServiceToClient - two, one with vehicle, one without. Check!
sellStockToClientVehicle - two, one with vehicle, one without. Check!
issueInvoices - two, one with clientName, other without will issue all non-issued.

We hope you will find our app powerful and easy to use!
For any comments, please do not hesitate to contact us at:
Telerik Academy 
We are open 24/7

Thank you!
ASA 