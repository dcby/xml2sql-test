using System;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;

namespace Xml2Sql
{
	[TestClass]
	public class UnitTest1
	{
		private const string CONN_STRING = @"Data Source =.\sql2012;Initial Catalog = Test; Integrated Security = True";

		[TestMethod]
		public void TestMethod1()
		{
			// scope variables for caching some values
			string s;

			// empty database
			ClearDatabase();

			// get path to xml document
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\Assets\Orders.xml");
			path = Path.GetFullPath(path);

			XNamespace xns = "http://www.spscommerce.com/RSX";

			// load document
			XDocument xdoc = XDocument.Load(path);

			// read document and build objects tree using LINQ to XML
			var orders = xdoc
				.Descendants(xns + "Order")
				.Select(xOrder =>
				{
					XElement xHeader = xOrder.Element(xns + "Header");
					XElement xOrderHeader = xHeader.Element(xns + "OrderHeader");
					var xPaymentTerms = xHeader.Elements(xns + "PaymentTerms");
					var xDates = xHeader.Elements(xns + "Date");
					var xContacts = xHeader.Elements(xns + "Contact");
					var xAddresses = xHeader.Elements(xns + "Address");
					var xFOBRelatedInstructions = xHeader.Elements(xns + "xFOBRelatedInstruction");
					var xPackagings = xHeader.Elements(xns + "Packaging");
					var xCarrierInformations = xHeader.Elements(xns + "CarrierInformation");
					var xReferences = xHeader.Elements(xns + "Reference");
					var xNotes = xHeader.Elements(xns + "Notes");
					var xChargesAllowances = xHeader.Elements(xns + "ChargesAllowances");

					var xSummary = xOrder.Element(xns + "Summary");

					return new
					{
						OrderHeader = new
						{
							TradingPartnerId = xOrderHeader.Element(xns + "TradingPartnerId").Value,
							PurchaseOrderNumber = xOrderHeader.Element(xns + "PurchaseOrderNumber").Value,
							TsetPurposeCode = xOrderHeader.Element(xns + "TsetPurposeCode")?.Value,
							PurchaseOrderTypeCode = xOrderHeader.Element(xns + "PurchaseOrderTypeCode")?.Value,
							PurchaseOrderDate = (s = xOrderHeader.Element(xns + "PurchaseOrderDate")?.Value) != null ? DateTime.Parse(s) : (DateTime?)null,
							ShipCompleteCode = xOrderHeader.Element(xns + "ShipCompleteCode")?.Value,
							SellersCurrency = xOrderHeader.Element(xns + "SellersCurrency")?.Value,
							Department = xOrderHeader.Element(xns + "Department")?.Value,
							Vendor = xOrderHeader.Element(xns + "Vendor")?.Value,
							Division = xOrderHeader.Element(xns + "Division")?.Value,
							CustomerOrderNumber = xOrderHeader.Element(xns + "CustomerOrderNumber")?.Value,
						},
						PaymentTerms = xPaymentTerms
							.Select(v => new
							{
								TermsDescription = v.Element(xns + "TermsDescription")?.Value
							}),
						Dates = xDates
							.Select(v => new
							{
								DateTimeQualifier1 = v.Element(xns + "DateTimeQualifier1")?.Value,
								Date1 = (s = v.Element(xns + "Date1")?.Value) != null ? DateTime.Parse(s) : (DateTime?)null
							}),
						Contacts = xContacts
							.Select(v => new
							{
								ContactTypeCode = v.Element(xns + "ContactTypeCode")?.Value,
								ContactName = v.Element(xns + "ContactName")?.Value,
								PrimaryPhone = v.Element(xns + "PrimaryPhone")?.Value,
							}),
						Addresses = xAddresses
							.Select(v => new
							{
								AddressTypeCode = v.Element(xns + "AddressTypeCode")?.Value,
								LocationCodeQualifier = v.Element(xns + "LocationCodeQualifier")?.Value,
								AddressLocationNumber = v.Element(xns + "AddressLocationNumber")?.Value,
								AddressName = v.Element(xns + "AddressName")?.Value,
								Address1 = v.Element(xns + "Address1")?.Value,
								Address2 = v.Element(xns + "Address2")?.Value,
								City = v.Element(xns + "City")?.Value,
								State = v.Element(xns + "State")?.Value,
								PostalCode = v.Element(xns + "PostalCode")?.Value,
								Country = v.Element(xns + "Country")?.Value
							}),
						FOBRelatedInstructions = xFOBRelatedInstructions
							.Select(v => new
							{
								FOBPayCode = v.Element(xns + "FOBPayCode")?.Value,
								FOBLocationQualifier = v.Element(xns + "FOBLocationQualifier")?.Value,
								FOBLocationDescription = v.Element(xns + "FOBLocationDescription")?.Value,
								FOBTitlePassageCode = v.Element(xns + "FOBTitlePassageCode")?.Value
							}),
						Packagings = xPackagings
							.Select(v => new
							{
								UnitLoadOptionCode = v.Element(xns + "UnitLoadOptionCode")?.Value
							}),
						CarrierInformations = xCarrierInformations
							.Select(v =>
							{
								var xServiceLevelCodes = v.Element(xns + "ServiceLevelCodes")?.Elements(xns + "ServiceLevelCode") ?? Enumerable.Empty<XElement>();
								return new
								{
									CarrierTransMethodCode = v.Element(xns + "CarrierTransMethodCode")?.Value,
									CarrierAlphaCode = v.Element(xns + "CarrierAlphaCode")?.Value,
									CarrierRouting = v.Element(xns + "CarrierRouting")?.Value,
									ServiceLevelCodes = xServiceLevelCodes.Select(v2 => v2.Value)
								};
							}),
						References = xReferences
							.Select(v => new
							{
								ReferenceQual = v.Element(xns + "ReferenceQual")?.Value,
								ReferenceID = v.Element(xns + "ReferenceID")?.Value,
								Description = v.Element(xns + "Description")?.Value
							}),
						Notes = xNotes
							.Select(v => new
							{
								NoteCode = v.Element(xns + "NoteCode")?.Value,
								NoteInformationField = v.Element(xns + "NoteInformationField")?.Value
							}),
						ChargesAllowances = xChargesAllowances
							.Select(v => new
							{
								AllowChrgIndicator = v.Element(xns + "AllowChrgIndicator")?.Value,
								AllowChrgCode = v.Element(xns + "AllowChrgCode")?.Value,
								AllowChrgAmt = (s = v.Element(xns + "AllowChrgAmt")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null,
								AllowChrgHandlingDescription = v.Element(xns + "AllowChrgHandlingDescription")?.Value
							}),
						Summary = xSummary != null
							? new
							{
								TotalAmount = (s = xSummary.Element(xns + "TotalAmount")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null,
								TotalLineItemNumber = (s = xSummary.Element(xns + "TotalLineItemNumber")?.Value) != null ? Int32.Parse(s) : (int?)null,
								TotalQuantity = (s = xSummary.Element(xns + "TotalQuantity")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null
							}
							: null
					};
				})
				.ToArray();

			// create database related objects (connection etc.)
			using (SqlConnection conn = OpenConnection())
			using (SqlTransaction tran = conn.BeginTransaction())
			using (SqlCommand insertOrderCmd = CreateInsertOrderCommand(tran))
			using (SqlCommand insertPaymentTermsCmd = CreateInsertPaymentTermsCommand(tran))
			using (SqlCommand insertDateCmd = CreateInsertDateCommand(tran))
			{
				// iterate over orders
				foreach (var o in orders)
				{
					insertOrderCmd.Parameters["@TradingPartnerId"].Value = o.OrderHeader.TradingPartnerId;
					insertOrderCmd.Parameters["@PurchaseOrderNumber"].Value = o.OrderHeader.PurchaseOrderNumber;
					insertOrderCmd.Parameters["@TsetPurposeCode"].Value = o.OrderHeader.TsetPurposeCode;
					insertOrderCmd.Parameters["@PurchaseOrderTypeCode"].Value = o.OrderHeader.PurchaseOrderTypeCode;
					insertOrderCmd.Parameters["@PurchaseOrderDate"].Value = o.OrderHeader.PurchaseOrderDate;
					insertOrderCmd.Parameters["@ShipCompleteCode"].Value = o.OrderHeader.ShipCompleteCode;
					insertOrderCmd.Parameters["@SellersCurrency"].Value = o.OrderHeader.SellersCurrency;
					insertOrderCmd.Parameters["@Department"].Value = o.OrderHeader.Department;
					insertOrderCmd.Parameters["@Vendor"].Value = o.OrderHeader.Vendor;
					insertOrderCmd.Parameters["@Division"].Value = o.OrderHeader.Division;
					insertOrderCmd.Parameters["@CustomerOrderNumber"].Value = o.OrderHeader.CustomerOrderNumber;
					int orderId = (int)insertOrderCmd.ExecuteScalar();

					// iterate over payment terms (order)
					foreach (var pt in o.PaymentTerms)
					{
						insertPaymentTermsCmd.Parameters["@OrderId"].Value = orderId;
						insertPaymentTermsCmd.Parameters["@TermsDescription"].Value = pt.TermsDescription;
						insertPaymentTermsCmd.ExecuteNonQuery();
					}

					// iterate over dates (order)
					foreach (var d in o.Dates)
					{
						insertDateCmd.Parameters["@OrderId"].Value = orderId;
						insertDateCmd.Parameters["@LineItemId"].Value = DBNull.Value;
						insertDateCmd.Parameters["@DateTimeQualifier1"].Value = d.DateTimeQualifier1;
						insertDateCmd.Parameters["@Date1"].Value = d.Date1;
						insertDateCmd.ExecuteNonQuery();
					}

					// iterate over contacts (order)
					foreach (var c in o.Contacts)
					{
					}

					// iterate over addresses (order)
					foreach (var a in o.Addresses)
					{
					}
				}

				tran.Commit();
			}
		}

		private SqlConnection OpenConnection()
		{
			SqlConnection conn = new SqlConnection(CONN_STRING);
			conn.Open();
			return conn;
		}

		private SqlCommand CreateInsertOrderCommand(SqlTransaction tran)
		{
			string sql = @"INSERT INTO Orders (TradingPartnerId, PurchaseOrderNumber, TsetPurposeCode, PurchaseOrderTypeCode, PurchaseOrderDate,
				ShipCompleteCode, SellersCurrency, Department, Vendor, Division, CustomerOrderNumber)
				OUTPUT inserted.OrderId
				VALUES (@TradingPartnerId, @PurchaseOrderNumber, @TsetPurposeCode, @PurchaseOrderTypeCode, @PurchaseOrderDate,
				@ShipCompleteCode, @SellersCurrency, @Department, @Vendor, @Division, @CustomerOrderNumber)";
			SqlCommand comm = new SqlCommand(sql, tran.Connection, tran);
			comm.Parameters.Add("@TradingPartnerId", SqlDbType.VarChar);
			comm.Parameters.Add("@PurchaseOrderNumber", SqlDbType.VarChar);
			comm.Parameters.Add("@TsetPurposeCode", SqlDbType.Char);
			comm.Parameters.Add("@PurchaseOrderTypeCode", SqlDbType.Char);
			comm.Parameters.Add("@PurchaseOrderDate", SqlDbType.Date);
			comm.Parameters.Add("@ShipCompleteCode", SqlDbType.Char);
			comm.Parameters.Add("@SellersCurrency", SqlDbType.VarChar);
			comm.Parameters.Add("@Department", SqlDbType.VarChar);
			comm.Parameters.Add("@Vendor", SqlDbType.VarChar);
			comm.Parameters.Add("@Division", SqlDbType.VarChar);
			comm.Parameters.Add("@CustomerOrderNumber", SqlDbType.VarChar);
			return comm;
		}

		private SqlCommand CreateInsertPaymentTermsCommand(SqlTransaction tran)
		{
			string sql = @"INSERT INTO PaymentTerms (OrderId, TermsDescription)
				VALUES (@OrderId, @TermsDescription)";
			SqlCommand comm = new SqlCommand(sql, tran.Connection, tran);
			comm.Parameters.Add("@OrderId", SqlDbType.Int);
			comm.Parameters.Add("@TermsDescription", SqlDbType.VarChar);
			return comm;
		}

		private SqlCommand CreateInsertDateCommand(SqlTransaction tran)
		{
			string sql = @"INSERT INTO Dates (OrderId, LineItemId, DateTimeQualifier1, Date1)
				VALUES (@OrderId, @LineItemId, @DateTimeQualifier1, @Date1)";
			SqlCommand comm = new SqlCommand(sql, tran.Connection, tran);
			comm.Parameters.Add("@OrderId", SqlDbType.Int);
			comm.Parameters.Add("@LineItemId", SqlDbType.Int);
			comm.Parameters.Add("@DateTimeQualifier1", SqlDbType.Char);
			comm.Parameters.Add("@Date1", SqlDbType.Date);
			return comm;
		}

		private void ClearDatabase()
		{
			using (SqlConnection conn = OpenConnection())
			using (SqlTransaction tran = conn.BeginTransaction())
			using (SqlCommand cleanupCmd = CreateCleanupCommand(tran))
			{
				cleanupCmd.ExecuteNonQuery();
				tran.Commit();
			}
		}

		private SqlCommand CreateCleanupCommand(SqlTransaction tran)
		{
			string sql = @"DELETE FROM Orders;
				DELETE FROM PaymentTerms;
				DELETE FROM Dates;";
			SqlCommand comm = new SqlCommand(sql, tran.Connection, tran);
			return comm;
		}
	}
}
