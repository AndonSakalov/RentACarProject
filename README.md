# RentACarProject
 Rent a car Web Application with ASP.NET Core

Overview:
The application gives functionality for
 Customers:
        To search for vehicles, filter the results, reserve and rent vehicles. 
 Staff:
        Functionalities like Create/Delete: Vehicles, Engines, Makes, Transmissions. Branch actions that include: Delete/Edit vehicle, List of vehicles due for service and option to set them as serviced once that happens, list of vehicle reservations and option to promote reservation to rental if the vehicle is not in ongoing rental already, and rental list with option to end the rental with selecting payment info and kilometers passed, freeing the vehicle. 
 Admin: 
        Can do all of the above with added option to promote regular account(Customer) to Admin account.

Every role have the common functionality of 'Account info' which is information about the account and rentals history, if any.

Implemented filtering on vehicle listing. 
Search function for accounts to be promoted to staff role. 
Paging for rentals history.
Custom 404 NotFoundPage and 500 Internal Server Error.

In configurations there are a couple of instances for every entity and 3 accounts for the 3 roles: Admin, Staff and Customer.