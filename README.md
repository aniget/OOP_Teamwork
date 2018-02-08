﻿/////////////////////////////////////////////////////////////////

The Ultimate OldFashioned* AutoService ©

/////////////////////////////////////////////////////////////////

* OldFashioned, you may wander?! This is because we use a very old-fashioned console-based computer system, but it's alse very powerfull too!


_________________________________________

Development team:
_________________________________________
•	Alex: alexparvanov
•	Angel: aniget
•	Viacheslav: borschetsky


_________________________________________

The main purpose:
_________________________________________


This console app is made to help all sort of AutoServices (from small 1-2 person to big 40-50 people auto centers) do your main job, 
help their clients have a happy driving experience and leave the rest to the app.

_________________________________________

Details about the application:
_________________________________________

With this application we can help you with professional repairs of all sort of vehicles. Our system currently supports:
•	Cars, 
•	Small Trucks, 
•	Trucks
In the roadmap we have Planes, Submarines and even AlienShips


All spectrum of Stakeholders
The application provides functionality to register and modify the following stakeholders and their activities:
	Employees - register, give rights and responsibilities, change their salaries and ofcourse fire them
	Clients - register them and their vehicles, set discount levels and provide reports for the repairs they've made on their vehicles
	Suppliers - registration and removal in case they manage poorly with stock delivery

The app can also handle payments!
BitCoins are accepted β 
We are very strict onvalidating the input, we even validate the IBANs of our clients and suppliers
Partial invoice payments are also available in this version of the application. 

_____________________________________________

Main functions (commands) of the app:
_____________________________________________

1.	showEmployees
2.	hireEmployee;\<firstName>;\<lastName>;\<position>;\<salary>;\<ratePerMinute>;\<department>;
3.	showAllEmployeesAtDepartment;\<department>
4.	fireEmployee;\<employeeId>
5.	changeEmployeeRate;\<employeeId>;\<ratePerMinute>
6.	changeEmployeePosition;\<employeeId>;\<position>
7.	createBankAccount;\<employeeId>;\<assetName>;\<IBAN> - check validation
8.	addEmployeeResponsibility;\<employeeId>;\<responsibility>;[\<responsibility>;]*
9.	removeEmpoloyeeResponsibility;\<employeeId>;\<responsibility>;[\<responsibility>;]*
10.	depositCashInBank;\<bankAccountId>;\<depositAmount>
11.	registerClient;\<name>;\<address>;\<uniqueNumber>
12.	registerSupplier;\<name>;\<address>;\<uniqueNumber>
13.	removeSupplier;\< name>
14.	removeClient;\< name>
15.	changeSupplierName;\<currentName>;\<newName>
16.	orderStockToWarehouse;\<employeeFirstName>;\<supplier>;\<partName>;\<partNumber>;\<Price>;[\<employeeLastName>;\<employeeDepartment>]*
17.	addVehicleToClient;\<vehicleType>;\<make>;\<model>;\<uniqueNumber>;\<year>;\<engineType>;\<seats/weightCapacity>;\<clientUniqueName>
18.	sellServiceToClientVehicle;\<employeeFirstName>;\<supplier>;\<partName>;\<partNumber>;\<Price>;[\<employeeLastName>;\<employeeDepartment>]*
19.	sellStockToClientVehicle; ;\<employeeFirstName>;\<supplier>;\<partName>;\<partNumber>;\<Price>;[\<employeeLastName>;\<employeeDepartment>]*
20.	issueInvoices;
21.	many more to come 


We hope you will find our app powerful and easy to use!
For any comments, please do not hesitate to contact us at:
Telerik Academy 
We are open 24/7

Thank you!
ASA
