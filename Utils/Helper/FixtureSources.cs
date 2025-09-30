using dev_repmove_autotest.Utils.Enum;
using System.Collections;

namespace dev_repmove_autotest.Utils.Helper
{
    public static class FixtureSources
    {
        public static IEnumerable BrowserTypes => EBrowserType.GetValues(typeof(EBrowserType)).Cast<EBrowserType>();
    }
}
