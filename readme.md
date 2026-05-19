# Library Management System

* A 4-layer .NET Core Console Application for managing a library's daily operations.
* Built using C# and Entity Framework Core with a PostgreSQL database.
* Provides completely separate workflows and menus for Admins and Library Members.

## Folder Structure

* `LibraryManagementSystem.Core/` - Contains Data Models, Enums, and Interfaces.
* `LibraryManagementSystem.DAL/` - Data Access Layer (EF Core DbContext, Repositories, Migrations).
* `LibraryManagementSystem.BLL/` - Business Logic Layer (Services handling validation, fines, and logic).
* `LibraryManagementSystem.PL/` - Presentation Layer (Console UI, Menus, Admin/User interaction).

## Business Logic & Rules

* Users can register, but they must choose a specific Membership Type.
* Different Membership Types determine the maximum number of books allowed at once.
* Different Membership Types determine the maximum number of days a book can be kept.
* A user cannot borrow more books if they have reached their active borrowing limit.
* A user cannot borrow a book if they have an unpaid fine.
* A user cannot borrow the exact same book title twice simultaneously.
* If a book is returned after the due date, a fine of Rs.10 per day is generated.
* Returning a book in 'Lost' or 'Damaged' condition updates the specific copy's status permanently.
* Admins can view 8 distinct analytical reports, including overdue books and most borrowed titles.
* PostgreSQL functions are used natively for specific database calculations (like calculating member fines).

## Screenshots

Member Operations:
<img width="900" height="718" alt="Image" src="https://github.com/user-attachments/assets/a8b028da-b5ca-4e4e-a1d4-c774f5cceb33" />
<img width="900" height="741" alt="Image" src="https://github.com/user-attachments/assets/2e833c8d-83e8-42fa-97bc-f63e5eb8f71e" />
<img width="900" height="699" alt="Image" src="https://github.com/user-attachments/assets/73493182-0d96-4d61-b685-806efad2cf1d" />


Admin Operations: 

<img width="900" height="691" alt="Image" src="https://github.com/user-attachments/assets/b1c67cad-ea65-4dfa-957b-083e09f8fdc6" />
<img width="900" height="746" alt="Image" src="https://github.com/user-attachments/assets/93ce2e4a-9522-41d4-97ab-27edd1dfc8e0" />
<img width="900" height="652" alt="Image" src="https://github.com/user-attachments/assets/0f0fa7fc-5ab9-40b3-8b99-91f327a1975a" />
<img width="900" height="751" alt="Image" src="https://github.com/user-attachments/assets/aaee3352-7537-48d0-91f3-9a6e0130d32e" />
<img width="900" height="710" alt="Image" src="https://github.com/user-attachments/assets/55ed485c-fb69-4e99-b10f-48e785cca2a3" />
<img width="900" height="259" alt="Image" src="https://github.com/user-attachments/assets/ee32f6bc-e4a1-4b15-a67b-b2cb4b3ed2f9" />
