#if __WIN32__
using SQLitePlatformImplementation = SQLite.Net.Platform.Win32.SQLitePlatformWin32;
#elif WINDOWS_PHONE
using SQLitePlatformImplementation = SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8;
#elif __WINRT__
using SQLitePlatformImplementation = SQLite.Net.Platform.WinRT.SQLitePlatformWinRT;
#elif __IOS__
using SQLitePlatformImplementation = SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS;
#elif __ANDROID__
using SQLitePlatformImplementation = SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid;
#else
using SQLitePlatformImplementation = SQLite.Net.Platform.Generic.SQLitePlatformGeneric;
#endif

using SQLite.Net.Interop;

namespace Voxelscape.Utility.Data.SQLite.Utilities
{
	/// <summary>
	///
	/// </summary>
	internal static class SQLitePlatform
	{
		public static ISQLitePlatform New() => new SQLitePlatformImplementation();
	}
}
