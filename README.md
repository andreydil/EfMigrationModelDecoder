# EfMigrationModelDecoder
This console tool decodes a database model as EDMX-file from the Model field of __MigrationHistory table in your database. 
Entity Framework Code First uses that model to generate migrations code when you do "Add-Migration" command.
It maybe useful to see what model is stored in the database to debug some difficult issues with migrations, i.e. after merge of two branches that both have new migrations.
