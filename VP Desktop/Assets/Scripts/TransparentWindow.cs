using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class TransparentWindow : MonoBehaviour
{
    [DllImport("user32.dll")]
    public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int index, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dbFlags);

    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    const int GLW_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;
    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint LWA_COLORKEY = 0x00000001;
    private IntPtr hWnd;

    private void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();
        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, GLW_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        //SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#endif
    }

    private void Update()
    {
        SetClickThrough(Physics2D.OverlapPoint(Input.mousePosition) == null);
    }

    private void SetClickThrough(bool clickThrough)
    {
        if (clickThrough)
        {
            SetWindowLong(hWnd, GLW_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
            return;
        }
        SetWindowLong(hWnd, GLW_EXSTYLE, WS_EX_LAYERED);
    }
}
