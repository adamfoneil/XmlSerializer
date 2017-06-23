using System;
using System.IO;
using System.Xml.Serialization;

namespace AdamOneilSoftware
{
	public abstract class UserSettings
	{
		public static string GetFilename(string companyName, string productName)
		{
			return Path.Combine(
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				companyName, productName, "Settings.xml");
		}
		
		protected string CompanyName { get; set; }
		protected string ProductName { get; set; }

		public static TSettings Load<TSettings>(string companyName, string productName) where TSettings : UserSettings, new()
		{
			TSettings result = null;
			string fileName = GetFilename(companyName, productName);

			if (File.Exists(fileName))
			{
				result = XmlSerializerHelper.Load<TSettings>(fileName);
			}
			else
			{
				result = new TSettings();
			}

			result.CompanyName = companyName;
			result.ProductName = productName;

			return result;
		}		

		public void Save()
		{
			XmlSerializerHelper.Save(this, GetFilename(CompanyName, ProductName));
		}

		~UserSettings()
		{
			Save();
		}
	}
}
