# EfMigrationModelDecoder
###A tool to extract EDMX schema for a specfic migration from __MigrationHistory.

This console tool decodes a database model as EDMX-file from the Model field of __MigrationHistory table in your MSSQL database.  
Entity Framework Code First uses that model to generate migrations code when you do "Add-Migration" command.
It maybe useful to see what model is stored in the database to debug some difficult issues with migrations, i.e. after merge of two branches that both have new migrations.  
EDMX file can be opened with Visual Studio.

###Usage
#####You must specify connection string to your MSSQL database.  
#####Migrations can be specified either by ID or by number. Numbers are zero-based. Negative values are supported to count from the end. 
For example:
* **/migration:AddUsersTable**  - for the migration with id 'AddUsersTable'
* **/migration:0**  - for the first migration
* **/migration:7**  - for migration #7 (zero-based)
* **/migration:-1**  - for the last migration

###Usage examples:
Get EDMX for a migration by its name:
```bat
>EfMigrationModelDecoder.Cli.exe "Data Source=.\SQL2012;Initial Catalog=TestDb;User ID=sa;Password=******;MultipleActiveResultSets=True" /migration:Init
```

Get EDMX for the first migration:
```bat
>EfMigrationModelDecoder.Cli.exe "Data Source=.\SQL2012;Initial Catalog=TestDb;User ID=sa;Password=******;MultipleActiveResultSets=True" /migration:0 /outFile:MyModel.edmx
```

Get EDMX for the second to last migration:
```bat
>EfMigrationModelDecoder.Cli.exe "Data Source=.\SQL2012;Initial Catalog=TestDb;User ID=sa;Password=******;MultipleActiveResultSets=True" /migration:-2 /outFile:MyModel.edmx
```
