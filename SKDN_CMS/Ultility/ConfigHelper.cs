namespace DFISYS.Ultility
{
	/// <summary>
	/// Summary description for ConfigHelper.
	/// </summary>
	public class ConfigHelper
	{
		public static string GetSkinItemStyle(string _strItemName, string _strConfigName)
		{
			return Ultility.ConfigurationSetting.GetItemContent(_strItemName, _strConfigName, Definition.SKIN_FOLDER_SETTING_KEY, Definition.SKIN_PREFIX);
		}

		public static string GetTemplateContent(string _strTemplateItem)
		{
			return Ultility.ConfigurationSetting.GetItemContent(_strTemplateItem, "Template", Definition.TEMPLATE_FOLDER_SETTING_KEY, Definition.TEMPLATE_PREFIX);
		}

		public static string GetNewsletterSettings(string _strItemName)
		{
			return Ultility.ConfigurationSetting.GetItemValue(_strItemName, "Newsletter", Definition.NEWSLETTER_FOLDER_SETTING_KEY, Definition.NEWSLETTER_PREFIX);
		}

		public static void SetDailyTemplateContent(string _strTemplateRef, string _strTemplateContent)
		{
			Ultility.ConfigurationSetting.SetItemContent(_strTemplateRef, "Template", Definition.TEMPLATE_FOLDER_SETTING_KEY, Definition.TEMPLATE_PREFIX, _strTemplateContent);
		}

		public static void SetNewsletterConfiguration(string _strItemName, string _strItemValue)
		{
			Ultility.ConfigurationSetting.SetItemValue(_strItemName, "Newsletter", Definition.TEMPLATE_FOLDER_SETTING_KEY, Definition.TEMPLATE_PREFIX, _strItemValue);
		}
	}
}
