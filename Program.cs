using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace ConsoleApp1_xml
{
	class Program
	{
		static void Main()
		{
			DateTime thisDay = DateTime.Today;
			String today = thisDay.ToString("d");
			StringBuilder sb = new StringBuilder();
			String line = "";
			XmlWriterSettings xws = new XmlWriterSettings{ OmitXmlDeclaration = true };
			using (XmlWriter xw = XmlWriter.Create(sb, xws))
				{
					string[] source = File.ReadAllLines(@"C:\Export\export_purchaseorder_item.txt");
					XElement item = new XElement("XML",
					 new XElement("ORDER",
						new XElement("ORDER_HEADER",
							new XElement("ORDER_INFO",
								new XElement("ORDER_ID", "PO Number"),
								new XElement("ORDER_DATE", today),
								new XElement("PARTIES",
									new XElement("PARTY",
										new XElement("PARTY_ID", "54.068"),
										new XElement("PARTY_ROLE", "Buyer"),
										new XElement("ADDRESS",
											new XElement("NAME", "Ultra-Lite Shutters"),
											new XElement("CONTACT_DETAILS",
												new XElement("CONTACT_NAME", "Chris Nesbitt")
											),
											new XElement("STREET", "7307 40th Sreet SE"),
											new XElement("ZIP", "T2C 2K4"),
											new XElement("CITY", "Calgary"),
											new XElement("COUNTRY", "Canada"),
											new XElement("PHONE", "+1 403 280 2000"),
											new XElement("FAX", "+1 403 280 1558"),
											new XElement("EMAIL", "chris.nesbitt@ultralite.ca")
										)
									)
								)
							)
						),
						new XElement("ORDER_ITEM_LIST",
						from str in source
						let fields = str.Split(';')
							select new XElement("ORDER_ITEM",
								new XElement("ULD_ITEM_NUMBER", fields[1]),
								new XElement("DESCRIPTION", fields[2]),
								new XElement("VENDOR_ITEM_NUMBER", fields[3]),
								new XElement("UNIT_OF_MEASURE", fields[4]),
								new XElement("ORDERED", fields[5]),
								new XElement("UNIT_COST", fields[6]),
								new XElement("AMOUNT", fields[7])
							)
						)
					)
				);
				line += item.ToString() + "\n";
				item.Save(xw);
				File.WriteAllText(@"C:\Export\" + today + "_XML_Order.txt", line);
			}
		}
	}
}
