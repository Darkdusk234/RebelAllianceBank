# RebelAllianceBank

This internet bank assignement is a group project assignement in the course 'Object Oriented Programming' at System development .NET program at Campus Varberg.

## Innehållsförteckning

1. [Overall Description](#descripton)
2. [Installation](#installation)
3. [Användning](#användning)
4. [Funktioner](#funktioner)
5. [Bidra](#bidra)
6. [Licens](#licens)
7. [Kontakt](#kontakt)

---

## Overall Description

The assignment was to develop a internet bank app, according to a initial backlog. 


Ge en mer detaljerad beskrivning av projektet, dess syfte, och varför det är användbart.  
Exempel:
- **Teknologier:** Lista de viktigaste teknologierna som används.
- **Status:** Pågående, färdigt, eller i planeringsfas.
  
---

## Code structure

The code structure is centralised around a number of classes.

Except the Bank.cs it self, the most central classes for the program can can be grouped in: 
- Users (Customer.cs, Admin.cs), related through the interface IUser.
- Accounts (CardAccounts.cs, SavingsAccounts.cs, ISK.cs) related through a IUser interface IBankAccounts.
- Menues (CustomerMenu, AdminMenu), that inherent from the abstract class Menu.

In additon to these, there are also: 
- Loan.cs, Transactions.cs and Currency.cs of which instances are created. 
- Utils-group of classes where utility classes such as Filehandler.cs, Markdown.cs, Tashandler.cs and ExchangeRates.cs.


### Users

#### IUser.cs
Interface for the user-classes of the bank. 

#### Admin.cs
The administrator class 
Include the methods for: 
- updating all exchangerates.
- creting users
- unlocking user accounts.  

#### Customer.cs
The Customer class gather properties and methods related to the customers. 

Contain methods for: 
- creating and show bankaccounts in a neat markdown table. 
- deposit and transfer money (both between own accounts and to other customers). 
- printing out transaction logs for all accounts.
- applying for loans etc.

### Accounts

#### IBankAccount.cs
Interface for the different account types. 

#### CardAccount.se, SavingsAccount.cs, ISK.se
Manage the data of the respective bankaccount. 

Include methods for: 
- handling and printing out the transaction log of the specifik account.
  
### Menues

#### Menu.cs
Abstract overall menu class

#### AdminMenu.cs, CustomerMenu.cs
Classes for running the admin and customer menus. Inherents from Menu.cs.

The customer menu has three submenus:  
- Account menu
- Transaction
- Loan menu

### Other

#### Bank.cs
Main class that runs the banc system. 

#### Currency.cs
Instances of this class is used to store data related to the specific currencies (exchange rate, name (eg dollar) and country (e.g. US). 

#### Transaction.cs
A class for storing relavant data for doing transactions. 

### Utils

#### Exchange.cs
A class that handles everything related to the echange-rates stored int all the currency-objects. The central field is the _exchangeRates dictionary that gather all currency options. Key is the abbreviation of the currency and value are instances of the currency class. 

The main methods contained in this class are for…
- Printing rates and currencies (PrintAllRates(), PrintAllCurrencies())
- Populating the dictionary with default rates values (AddDefaultExchangeRates())’
- Methods used when admin is updating all the currencies from the Admin-klass. 
- Set a currency on a new account (SetAccountCurrency())
- Calculating the exchange rates from one rate to another. 
and methods that are called from the UpdateCurrency() method int the Admin-class.


```c#
Console.WriteLine("Hej!");
```

### Enums

  
---
## UML

link to UML???

## Function
Details about core functionality of the application
- Create accounts
- Transfer money between accounns
- Tako loans
- Financial Management
---
## Installation      

Ta bort?????

# Klona repositoriet
```bash
git clone https://github.com/Darkdusk234/RebelAllianceBank.git
