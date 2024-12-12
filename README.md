# RebelAllianceBank

This internet bank assignement is group project assignement in the course 'Object Oriented Programming' at System development .NET program at Campus Varberg.

## Innehållsförteckning

1. [Description](#descripton)
2. [Installation](#installation)
3. [Användning](#användning)
4. [Funktioner](#funktioner)
5. [Bidra](#bidra)
6. [Licens](#licens)
7. [Kontakt](#kontakt)

---

## Overall Description

The assignment was to develop a internet bank app, according to a initial backlog (https://qlok.notion.site/Objektorienterad-programmering-bb71eef850674b98ab1fecf2a7ac9ab2?p=114a11b2b40c8075bb14c5fa4dd87d4b&pm=s)


Ge en mer detaljerad beskrivning av projektet, dess syfte, och varför det är användbart.  
Exempel:
- **Teknologier:** Lista de viktigaste teknologierna som används.
- **Status:** Pågående, färdigt, eller i planeringsfas.
  
---

## Overall structure of the code

The code structure is centralised around a number of classes.

Except the Bank.cs it self, the most central classes for the program can can be grouped in: 
- Users (Customer.cs, Admin.cs), related through a IUser interface IUser
- Accounts (CardAccounts.cs, SavingsAccounts.cs, ISK.cs) related through a IUser interface IBankAccounts.
- Menues (CustomerMenu, AdminMenu), that inherent from the abstract class Menu.

In additon to these, there are also: 
- Loan.cs, Transactions.cs and Currency.cs of which instances are created. 
- Utils-group of classes where utility classes such as Filehandler.cs, Markdown.cs, Tashandler.cs and ExchangeRates.cs.


### Users

#### IUser.cs

#### Admin.cs
Include a method for to update all exchangerates.   

#### Customer.cs

Contain methods for: 
- Creating accounts ()
- deposit money to a chosen account (Deposit())
- printing out transactionlogs for all accounts (ShowAccountLogs)
- ....
- ....

- 
### Accounts

#### IBankAccount.cs

#### CardAccount.se, SavingsAccount.cs, ISK.se
Inlude fields such as: 
- ...
- ....
-

Include methods for: 
- accessing the _transaction log list and add a new transaction to it AddToTransactionLog(Transaction newTransaction)
- printing out the transaction log of each account (ShowTransactionLog())

### Menues

#### Menu.cs
Abstract menu class

#### AdminMenu.cs, CustomerMenu.cs
Class for running the the admin and customer menus. Inherents from Menu.cs.

### Other

#### Bank

### Currency-klassen
Instances of this class is used to store data related to the specific currencies (exchange rate, name (eg dollar) and country (e.g. US). 

### Transaction.cs
A class for storing relavant data for doing transactions. For more se RunTransactionsInQueue() under Bank.cs. 

### Utils

#### Exchange.cs
A class that handles everything related to the echange-rates stored int all the currency-objects. The central field is the _exchangeRates dictionary that gather all currency options. Key is the abbreviation of the currency and value are instances of the currency class. 

The main methods contained in this class are for…
- Printing rates and currencies (PrintAllRates(), PrintAllCurrencies())
- Populating the dictionary with default rates values (AddDefaultExchangeRates())’
- Set a currency on a new account (SetAccountCurrency())
- Calculating the exchange rates from one rate to another. 
and methods that are called from the UpdateCurrency() method int the Admin-class. 


  
---

## UML

link to UML???
  
---
## Installation      

Ta bort?????

# Klona repositoriet
```bash
git clone https://github.com/Darkdusk234/RebelAllianceBank.git
