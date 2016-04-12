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
using Xml2Sql.Model;

namespace Xml2Sql
{
	[TestClass]
	public class UnitTest1
	{
		private const string CONN_STRING = @"Data Source =.\sql2012; Initial Catalog = Test; Integrated Security = True";

		[TestMethod]
		public void TestMethod1()
		{
			// scope variables for caching some values
			string s;

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
					var xFOBRelatedInstructions = xHeader.Elements(xns + "FOBRelatedInstruction");
					var xPackagings = xHeader.Elements(xns + "Packaging");
					var xCarrierInformations = xHeader.Elements(xns + "CarrierInformation");
					var xReferences = xHeader.Elements(xns + "Reference");
					var xNotes = xHeader.Elements(xns + "Notes");
					var xChargesAllowances = xHeader.Elements(xns + "ChargesAllowances");

					var xSummary = xOrder.Element(xns + "Summary");

					return new Order
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
						PaymentTerms = xPaymentTerms
							.Select(v => new PaymentTerms
							{
								TermsDescription = v.Element(xns + "TermsDescription")?.Value
							}).ToArray(),
						Dates = xDates
							.Select(v => new Date
							{
								DateTimeQualifier1 = v.Element(xns + "DateTimeQualifier1")?.Value,
								Date1 = (s = v.Element(xns + "Date1")?.Value) != null ? DateTime.Parse(s) : (DateTime?)null
							}).ToArray(),
						Contacts = xContacts
							.Select(v => new Contact
							{
								ContactTypeCode = v.Element(xns + "ContactTypeCode")?.Value,
								ContactName = v.Element(xns + "ContactName")?.Value,
								PrimaryPhone = v.Element(xns + "PrimaryPhone")?.Value,
							}).ToArray(),
						Addresses = xAddresses
							.Select(v => new Address
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
							}).ToArray(),
						FOBRelatedInstructions = xFOBRelatedInstructions
							.Select(v => new FOBRelatedInstruction
							{
								FOBPayCode = v.Element(xns + "FOBPayCode")?.Value,
								FOBLocationQualifier = v.Element(xns + "FOBLocationQualifier")?.Value,
								FOBLocationDescription = v.Element(xns + "FOBLocationDescription")?.Value,
								FOBTitlePassageCode = v.Element(xns + "FOBTitlePassageCode")?.Value
							}).ToArray(),
						Packagings = xPackagings
							.Select(v => new Packaging
							{
								UnitLoadOptionCode = v.Element(xns + "UnitLoadOptionCode")?.Value
							}).ToArray(),
						CarrierInformations = xCarrierInformations
							.Select(v =>
							{
								var xServiceLevelCodes = v.Element(xns + "ServiceLevelCodes")?.Elements(xns + "ServiceLevelCode") ?? Enumerable.Empty<XElement>();
								return new CarrierInformation
								{
									CarrierTransMethodCode = v.Element(xns + "CarrierTransMethodCode")?.Value,
									CarrierAlphaCode = v.Element(xns + "CarrierAlphaCode")?.Value,
									CarrierRouting = v.Element(xns + "CarrierRouting")?.Value,
									ServiceLevelCodes = (s = String.Join(", ", xServiceLevelCodes.Select(v2 => v2.Value))) != "" ? s : null
								};
							}).ToArray(),
						References = xReferences
							.Select(v => new Reference
							{
								ReferenceQual = v.Element(xns + "ReferenceQual")?.Value,
								Reference_ID = v.Element(xns + "ReferenceID")?.Value,
								Description = v.Element(xns + "Description")?.Value
							}).ToArray(),
						Notes = xNotes
							.Select(v => new Notes
							{
								NoteCode = v.Element(xns + "NoteCode")?.Value,
								NoteInformationField = v.Element(xns + "NoteInformationField")?.Value
							}).ToArray(),
						ChargesAllowances = xChargesAllowances
							.Select(v => new ChargesAllowances
							{
								AllowChrgIndicator = v.Element(xns + "AllowChrgIndicator")?.Value,
								AllowChrgCode = v.Element(xns + "AllowChrgCode")?.Value,
								AllowChrgAmt = (s = v.Element(xns + "AllowChrgAmt")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null,
								AllowChrgHandlingDescription = v.Element(xns + "AllowChrgHandlingDescription")?.Value
							}).ToArray(),
						Summary = xSummary != null
							? new Summary
							{
								TotalAmount = (s = xSummary.Element(xns + "TotalAmount")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null,
								TotalLineItemNumber = (s = xSummary.Element(xns + "TotalLineItemNumber")?.Value) != null ? Int32.Parse(s) : (int?)null,
								TotalQuantity = (s = xSummary.Element(xns + "TotalQuantity")?.Value) != null ? Double.Parse(s, CultureInfo.InvariantCulture) : (double?)null
							}
							: null
					};
				})
				.ToArray();

			using (MyContext ctx = new MyContext())
			{
				ctx.Configuration.AutoDetectChangesEnabled = false;
				foreach(var order in orders)
				{
					ctx.Orders.Add(order);
				}
				ctx.SaveChanges();
			}
		}
	}
}
