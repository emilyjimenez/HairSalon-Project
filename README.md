
# _Hair Salon_

## _Independent Project - Epicodus C# Week 3: Database Basics_

#### By _Emily Wells Jimenez_

###### _10.27.2017_

## Description

_A hair salon web app that allows the user to add a list of stylists, and add list of clients that each stylist works with._


## Specs

| Behavior  |  Input | Output  |
|---|---|---|
| Allow user to add a stylist with the following details:  |   |   |
| Allow user to add a client to a stylist with the following details:  |   |   |
| Allow user to view all stylists in a list  |   |   |
| Allow user to view all clients under each stylist, along with stylist details |   |   |
| Allow user to delete a stylist  |   |   |
| Allow user to delete a client  |   |   |
|   |   |   |


## MySQL Commands

- CREATE DATABASE emily_jimenez;
- USE emily_jimenez;
- CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255), rate INT, skills VARCHAR(255), client_id INT);
- CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), birthday INT, email VARCHAR(255));

## Setup/Installation Requirements



## Known Bugs



## Technologies Used

* Atom
* C#
* Mono
* MySQL
* .Net Core 1.1
* MAMP
* PHPmyadmin

### License

NA

Copyright (c) 2017 Emily Wells-Jimenez
