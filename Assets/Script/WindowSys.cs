using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowSys : SystemBase
{
    // ==========================================
    // 引入 Windows API
    // ==========================================
    [StructLayout(LayoutKind.Sequential)]
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [DllImport("dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref RECT pvParam, uint fWinIni);

    const int GWL_STYLE = -16;
    const int GWL_EXSTYLE = -20;

    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
    const uint SWP_FRAMECHANGED = 0x0020;
    const uint SWP_SHOWWINDOW = 0x0040;
    const uint SPI_GETWORKAREA = 0x0030; // 获取工作区常数

    private IntPtr hWnd;
    private bool m_IsClickthrough = false;

    public override void Init(GameLuncher gameLuncher)
    {
        base.Init(gameLuncher);
        Application.runInBackground = true;

#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
        hWnd = GetActiveWindow();

        SetWindowLong(hWnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);

        uint currentExStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        SetWindowLong(hWnd, GWL_EXSTYLE, currentExStyle | WS_EX_LAYERED);

        // ==========================================
        // 【核心修改】：获取真实工作区，不再强行全屏
        // ==========================================
        RECT workArea = new RECT();
        SystemParametersInfo(SPI_GETWORKAREA, 0, ref workArea, 0);

        int windowWidth = workArea.right - workArea.left;
        int windowHeight = workArea.bottom - workArea.top;

        // 设置窗口位置：起点是工作区的左上角，长宽是工作区的长宽
        SetWindowPos(hWnd, HWND_TOPMOST, workArea.left, workArea.top, windowWidth, windowHeight, SWP_FRAMECHANGED | SWP_SHOWWINDOW);

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);
#endif
    }

    public override void Update()
    {
        base.Update();

#if !UNITY_EDITOR && UNITY_STANDALONE_WIN
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider != null)
        {
            SetClickthrough(false);
        }
        else
        {
            SetClickthrough(true);
        }
#endif
    }

    private void SetClickthrough(bool clickthrough)
    {
        if (m_IsClickthrough == clickthrough) return;

        uint currentExStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
        if (clickthrough)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, currentExStyle | WS_EX_TRANSPARENT);
        }
        else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, currentExStyle & ~WS_EX_TRANSPARENT);
        }

        m_IsClickthrough = clickthrough;
    }
}