using ConsoleApp1;
using System.Data;
using System.Data.SqlClient;

var SelectAllCustomersProcedure = "SelectAllCustomers";
Console.WriteLine($"{SelectAllCustomersProcedure}");
Console.WriteLine(await StoredProcedures.ExecuteSelectProcedureAsync(SelectAllCustomersProcedure));
Console.ReadLine();

var SelectCustomerProcedure = "SelectCustomer";
var SelectCustomerParams = new SqlParameter("Id", 1);
Console.WriteLine($"\n{SelectCustomerProcedure}");
Console.WriteLine(await StoredProcedures.ExecuteSelectProcedureAsync(SelectCustomerProcedure, SelectCustomerParams));
Console.ReadLine();

var SelectProductsByCustomerIdProcedure = "SelectProductsByCustomerId";
var SelectProductsByCustomerIdParam = new SqlParameter("Id", 1);
Console.WriteLine($"\n{SelectProductsByCustomerIdProcedure}");
Console.WriteLine(await StoredProcedures.ExecuteSelectProcedureAsync(SelectProductsByCustomerIdProcedure, SelectProductsByCustomerIdParam));
Console.ReadLine();


var InsertProductByCustomerId = "InsertProductByCustomerId";
var InsertProductByCustomerIdCustomerIdParam = new SqlParameter("CustomerId", 1);
var InsertProductByCustomerIdPriceParam = new SqlParameter("Price", 150);
var InsertProductByCustomerIdProductNameParam = new SqlParameter("ProductName", "Microwave");
Console.WriteLine($"\n{InsertProductByCustomerId}");
await StoredProcedures.ExecuteProcedureAsync(InsertProductByCustomerId, InsertProductByCustomerIdCustomerIdParam,
    InsertProductByCustomerIdPriceParam, InsertProductByCustomerIdProductNameParam);
Console.ReadLine();

var SelectProductsByCustomerIdAndMaxPriceProcedure = "SelectProductsByCustomerIdAndMaxPrice";
var SelectProductsByCustomerIdAndMaxPriceIdParam = new SqlParameter("Id", 1);
var SelectProductsByCustomerIdAndMaxPricePriceParam = new SqlParameter("Price", 200);
Console.WriteLine($"\n{SelectProductsByCustomerIdAndMaxPriceProcedure}");
Console.WriteLine(await StoredProcedures.ExecuteSelectProcedureAsync(SelectProductsByCustomerIdAndMaxPriceProcedure, 
    SelectProductsByCustomerIdAndMaxPriceIdParam, SelectProductsByCustomerIdAndMaxPricePriceParam));
Console.ReadLine();

