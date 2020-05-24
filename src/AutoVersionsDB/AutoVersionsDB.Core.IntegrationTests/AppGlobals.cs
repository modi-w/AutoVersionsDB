using AutoVersionsDB.Core.IntegrationTests.Helpers;

namespace AutoVersionsDB.Core.IntegrationTests
{
    public static class AppGlobals
    {
        private static IntegrationTestsSetting _appSetting;
        public static IntegrationTestsSetting AppSetting
        {
            get
            {
                if (_appSetting == null)
                {
                    _appSetting = IntegrationTestsSetting.LoadSetting();
                }

                return _appSetting;
            }
        }
    }
}
