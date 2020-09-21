# betOnTheLine_api
> Simple API to manage user login, user tips, and football matches. This project is connected with <a href="https://github.com/joaopinheiro10/BetOnTheLine">this</a>.

> Objectives

The main goal of this project is to learn new technologies such as C# and mySql while creating an API RESTFULL to serve our front end.

## Installation

### Clone
- Clone this repo to your local machine using `https://github.com/nelsonsantos22/betOnTheLine_api.git`

### Setup
```shell
$ cd api
$ dotnet add package MySqlConnector
```
Change "password" field on "appsettings.json" file to your mySql password
```shell
"DefaultConnection": "server=127.0.0.1;user id=root;password=CHANGE_HERE;port=3306;database=betontheline;"
```

### Create a MySql Database
```shell
CREATE DATABASE betontheline;
USE betontheline;

CREATE TABLE IF NOT EXISTS person (
  Id INT NOT NULL AUTO_INCREMENT,
  firstName LONGTEXT CHARSET utf8mb4,
  lastName LONGTEXT CHARSET utf8mb4,
  username LONGTEXT CHARSET utf8mb4,
  email LONGTEXT CHARSET utf8mb4,
  password LONGTEXT CHARSET utf8mb4,
  PRIMARY KEY (Id)
) ENGINE=InnoDB;    

CREATE TABLE IF NOT EXISTS football_match(
  Id INT NOT NULL AUTO_INCREMENT,
  home_team LONGTEXT CHARSET utf8mb4,
  away_team LONGTEXT CHARSET utf8mb4,
  PRIMARY KEY (Id)
) ENGINE=InnoDB; 

CREATE TABLE IF NOT EXISTS tip(
  Id INT NOT NULL AUTO_INCREMENT,
  tip INT NOT NULL,
  message LONGTEXT CHARSET utf8mb4,
  gameId INT,
  userId INT,
  PRIMARY KEY (Id),
  FOREIGN KEY (gameId) REFERENCES football_match(Id),
  FOREIGN KEY (userId) REFERENCES person(Id)
) ENGINE=InnoDB;
```

### Run
```shell
$ dotnet run
```

You can try the CRUD methods on Postman.

## Team

<a href="https://github.com/joaopedro1986">![FVCproductions](https://avatars2.githubusercontent.com/u/70176397?s=150)</a>   <a href="https://github.com/joaopinheiro10">![FVCproductions](https://avatars2.githubusercontent.com/u/69478805?s=150)</a>   <a href="https://github.com/Luis-Trigueiro">![FVCproductions](https://avatars2.githubusercontent.com/u/57730922?s=150)</a>   <a href="https://github.com/Tiago-Patricio">![FVCproductions](https://avatars2.githubusercontent.com/u/69849680?s=150)</a> <a href="https://github.com/nelsonsantos22">![FVCproductions](https://avatars3.githubusercontent.com/u/44930623?s=150)</a>
