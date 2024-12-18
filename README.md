# RebelAllianceBank

This internet bank assignement is a group project assignement in the course 'Object Oriented Programming' at System development .NET program at Campus Varberg.

## Table of Contents

1. [Overall Description](#overall-description)
2. [Code Structure](#code-structure)
3. [UML](#uml)

---

## Overall Description

The assignment was to develop a internet bank app, according to a initial backlog. 

Core functionality in the backlog stories: 
- Create accounts
- Transfer money between accounns
- Tako loans
- Financial Management

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

### Utils.cs

#### Exchange.cs
A class that handles everything related to the echange-rates stored int all the currency-objects. The central field is the _exchangeRates dictionary that gather all currency options. Key is the abbreviation of the currency and value are instances of the currency class. 

The main methods contained in this class are for…
- Printing rates and currencies (PrintAllRates(), PrintAllCurrencies())
- Populating the dictionary with default rates values (AddDefaultExchangeRates())’
- Methods used when admin is updating all the currencies from the Admin-klass. 
- Set a currency on a new account (SetAccountCurrency())
- Calculating the exchange rates from one rate to another. 
and methods that are called from the UpdateCurrency() method int the Admin-class.

#### InlaneColor.cs
Used when you want to change the color of a certain word in, for example, a `Console.WriteLine();`. This is done by converting the [ANSI escape sequence](https://en.wikipedia.org/wiki/ANSI_escape_code) into a more readable property. 

#### Markdown.cs
Used to print [markdown code](https://www.markdownguide.org) in the console. The class comes with a table, heading and a paragraph method that will print the chorus markdown code. 

#### MarkdownUtils.cs
Have generic methods with delegated functions to handle the Columns and table data to send to markdown class. Implements arrows to navigate through menus

#### Filehandler.cs
This class handles reading and writing user, bankaccount, and loan data from text files. It includes methods to load users with their associated accounts and loans, and save updates back to the files.

#### DataFiles
Is a mapp containing the .txt files for users, accounts and loan that the FileHandler.cs uses to read and write users from.

#### TaskManager.cs
Provides asynchronous task management, including a timer-based execution. A recurring task like processing queued transactions at specified intervals. It supports starting and stopping tasks, utilizing cancellation tokens to handle that.

#### SelectOneOrMore.cs
To be able to use this class, you first create an instance of it and fill the class with the column headings you want and a list of values that go into all rows under the column headings. To then be able to use it, you call the Show method.

### Enums

#### EnumsExchangeRate
Store a set number of values to be returned from a method (quit, correct, incorrecy).

#### HeaderLevels
Used to describe the levels of headings that exist. For example, the markdown header method uses it to remove unnecessary user errors when you want to use the header method.

---

## UML

A html file of the UML diagram can be found [here](). It needs to be downloaded before you open it. 

---

# Clone repository
```bash
git clone https://github.com/Darkdusk234/RebelAllianceBank.git
