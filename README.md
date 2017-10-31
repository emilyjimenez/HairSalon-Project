
# _Hair Salon_

## _Independent Project - Epicodus C# Week 3: Database Basics_

#### By _Emily Wells Jimenez_

###### _10.27.2017_

## Description

_A hair salon web app that allows the user to add a list of stylists, and add list of clients that each stylist works with._


## Specs

| Behavior  |  Input | Output  |
|---|---|---|
| Allow user to add a stylist with the following details: Name, Rate, Skills | Edgar, 150, Cut and Color  | Name: Edgar, Rate: $150/hr, Skills: Cut and Color  |
| Allow user to add a client to a stylist with the following details: Name, Birthday, Email  | Becky, 111288, becky@me.com  | Name: Becky, Birthday: 111288, Email: becky@me.com  |
| Allow user to view all stylists in a list  | After adding stylists, user is taken back to homepage  | List of stylists are displayed  |
| Allow user to view all clients under each stylist, along with stylist details | User clicks on stylist name and is taken to stylist profile page  | Client information is displayed in a list underneath stylist details  |
| Allow user to delete a stylist  | Clicks "delete stylist" button | Stylist deleted, taken back to homepage  |
| Allow user to delete a client  | Clicks "delete client" button | Client deleted, remains on stylist profile page  |



## MySQL Commands

- CREATE DATABASE emily_jimenez;
- USE emily_jimenez;
- CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), rate INT, skills VARCHAR(255), client_id INT);
- CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), birthday INT, email VARCHAR(255));

- CREATE DATABASE emily_jimenez_test;
- USE emily_jimenez_test;
- CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), rate INT, skills VARCHAR(255), client_id INT);
- CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), birthday INT, email VARCHAR(255));

## Setup/Installation Requirements

1. Clone [HairSalon-Project](https://github.com/emilyjimenez/HairSalon-Project) from Github
2. Make sure you have MAMP installed and are using .NET Core 1.1
3. Turn servers on via MAMP, and then load emily_jimenez and emily_jimenez_test databases into the PHPmyadmin tool in MAMP
4. Run dotnet restore and build on both the HairSalon folder and HairSalon.Tests folder, then run dotnet test on the test folder
5. After restore/build/test, run dotnet run into your terminal and go to localhost5000 in your preferred web browser

## Known Bugs

* Could not get birthdates to accurately display so used int number input instead.

## Technologies Used

* Atom
* C#
* Mono
* MySQL
* .Net Core 1.1
* MAMP
* PHPmyadmin

### License

This software is licensed under the MIT license.

Copyright (c) 2017 Emily Wells-Jimenez
