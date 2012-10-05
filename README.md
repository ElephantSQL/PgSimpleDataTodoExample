PostgreSQL Simple.Data Todo Example
===

This is an example application which shows how to use [Simple.Data](https://github.com/markrendle/Simple.Data) to connect to a [PostgreSQL server](http://www.postgresql.com/), how to migrate the database with [MigratorDotNet](http://code.google.com/p/migratordotnet/), all packaged in a small CRUD application using [Asp.Net MVC](http://www.asp.net/mvc).

```ConnStr``` is a small helper class to parse the [ElephantSQL](http://www.elephantsql.com) URL and generate a .NET compatible connection string from it. 

Migrations are run at ```application_start```.

The database connection is opened in ```TodoController```. 