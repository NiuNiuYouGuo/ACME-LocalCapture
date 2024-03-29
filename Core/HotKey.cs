﻿using System.Runtime.InteropServices;

namespace LocalCapture.Core
{
    internal class HotKey
    {
        //“public static extern bool RegisterHotKey()”这个函数用于注册热键。由于这个函数需要引用user32.dll动态链接库后才能使用，并且
        //user32.dll是非托管代码，不能用命名空间的方式直接引用，所以需要用“DllImport”进行引入后才能使用。于是在函数前面需要加上
        //“[DllImport("user32.dll", SetLastError = true)]”这行语句。
        //“public static extern bool UnregisterHotKey()”这个函数用于注销热键，同理也需要用DllImport引用user32.dll后才能使用。
        //“public enum KeyModifiers{}”定义了一组枚举，将辅助键的数字代码直接表示为文字，以方便使用。这样在调用时我们不必记住每一个辅
        //助键的代码而只需直接选择其名称即可。
        //（2）以窗体FormA为例，介绍HotKey类的使用
        //在FormA的Activate事件中注册热键，本例中注册Ctrl+Alt+A这三个热键。这里的Id号可任意设置，但要保证不被重复。
        //=====================================================================
        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            //要定义热键的窗口的句柄
            IntPtr hWnd,
            //定义热键ID（不能与其它ID重复）
            int id,
            //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            KeyModifiers fsModifiers,
            //定义热键的内容
            Keys vk
          );
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            //要取消热键的窗口的句柄
            IntPtr hWnd,        //要取消热键的窗口的句柄
                                //要取消热键的ID
            int id
          );
        //定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8,
            CtrlAlt = 3
        }
        //重载FromA中的WndProc函数
        //监视Windows消息
        //重载WndProc方法，用于实现热键响应
        public const int WM_HOTKEY = 0x0312;
    }
}
