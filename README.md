### Requirements
- Visual Studio 2015 (any version except Express).
- SQL Server 2012 or newer.

### Prepare
- Edit `App.config` file to provide correct connection string. Specify non-existing database for `Initial Catalog` value.
- Compile project to restore dependency packages and discover tests.

### Run
Open `MainTest.cs` file, right click inside `ConvertData()` method and choose `Run Tests` or `Debug Tests` from context menu. Or open Test Explorer window (menu:Test/Windows/Test Explorer) and run/debug test from there.

### Notes
It is not possible to serialize data to single db table because each order may contain multiple child elements per specific type (i.e. many dates, contacts and so on).
