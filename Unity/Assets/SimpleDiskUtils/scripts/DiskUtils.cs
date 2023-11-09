/*

Class: DiskUtils.cs
==============================================
Last update: 2016-05-12  (by Dikra)
==============================================

Copyright (c) 2016  M Dikra Prasetya

 * MIT LICENSE
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*/

using System;

namespace SimpleDiskUtils
{
	public class DiskUtils
	{
		public static int CheckAvailableSpace(string drive = null)
		{
			var instance = GetInstance();
			return instance.CheckAvailableSpace(drive);
		}

		public static int CheckTotalSpace(string drive = null)
		{
			var instance = GetInstance();
			return instance.CheckTotalSpace(drive);
		}

		public static int CheckBusySpace(string drive = null)
		{
			var instance = GetInstance();
			return instance.CheckBusySpace(drive);
		}


		private static INativeDiskUtils GetInstance()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			return new DiskUtilsAndroid();
#elif UNITY_IOS && !UNITY_EDITOR
			return new DiskUtilsIOS();
#elif UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
			return new DiskUtilsOSX();
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			return new DiskUtilsWindows();
#else
			#error Unsupported platform
#endif
		}
	}
}