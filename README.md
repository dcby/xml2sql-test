The sample uses a list of additional technologies to convert XML data and store it in SQL database.

- [LINQ (Language-Integrated Query)](https://msdn.microsoft.com/en-us/library/bb397926.aspx)
- [LINQ to XML](https://msdn.microsoft.com/en-us/library/bb387098.aspx)
- [Entity Framework Code First](https://msdn.microsoft.com/en-us/data/ee712907#codefirst)

The XML file is parsed into an object tree using *LINQ to XML*. The object tree consists of entity objects defined in `Model` namespace. The model defines a database structure as well (*Code First*). Once object tree is ready, it is added to the database context which, in a turn, will add records to underlined SQL db. The db with its' structure will be created automatically.

### Requirements
- Visual Studio 2015 (any version except Express).
- SQL Server 2012 or newer.

### Prepare
- Edit `App.config` file to provide correct connection string. Specify non-existing database for `Initial Catalog` value.
- Compile project to restore dependency packages and discover tests.

### Run
Open `MainTest.cs` file, right click inside `ConvertData()` method and choose `Run Tests` or `Debug Tests` from context menu. Or open Test Explorer window (menu:Test/Windows/Test Explorer) and run/debug test from there.

### Notes
- First run might be a bit slow due to database creation.
- It is not possible to serialize data to single db table because each order may contain multiple child elements per specific type (i.e. many dates, contacts and so on).
