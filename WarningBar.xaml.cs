using Autodesk.Windows;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace PaperMEP.UI_Templates
{
    public partial class WarningBar : Window, INotifyPropertyChanged, IDisposable
    {
        private string _displayText;
        public string DisplayText
        {
            get => _displayText;
            set { _displayText = value; OnPropertyChanged(); }
        }

        private readonly IntPtr _revitHandle;
        private readonly DispatcherTimer _timer;

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowPos(
            IntPtr hWnd, IntPtr hWndInsertAfter,
            int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_SHOWWINDOW = 0x0040;

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left; public int Top; public int Right; public int Bottom; }

        public WarningBar(string text)
        {


            Utility.Utility.LoadXaml(this);


            //InitializeComponent();

            DataContext = this;
            DisplayText = text;
            _revitHandle = ComponentManager.ApplicationWindow;

            var helper = new WindowInteropHelper(this) { Owner = _revitHandle };
            this.SourceInitialized += (s, e) =>
            {
                IntPtr hwnd = helper.Handle;
                SetWindowPos(hwnd, HWND_TOPMOST, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
            };

            UpdatePosition();

            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += (s, e) => UpdatePosition();
            _timer.Start();
        }

        private void UpdatePosition()
        {
            if (_revitHandle == IntPtr.Zero) return;
            if (GetWindowRect(_revitHandle, out RECT rect))
            {
                // Giả sử title‑bar Windows ~30px, ribbon Revit ~50px => tổng khoảng 80
                int windowHeight = rect.Bottom - rect.Top;
                double ratio = 0.00; // 0% of window height
                int offset = (int)(windowHeight * ratio);

                Left = rect.Left;
                Top = rect.Top + offset;
                Width = rect.Right - rect.Left;

                // Đảm bảo window luôn topmost
                var helper = new WindowInteropHelper(this);
                SetWindowPos(helper.Handle, HWND_TOPMOST,
                    0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOACTIVATE | SWP_SHOWWINDOW);
            }
        }

        public void UpdateText(string newText) => DisplayText = newText;

        protected override void OnClosed(EventArgs e)
        {
            _timer.Stop();
            base.OnClosed(e);
        }

        public void Dispose()
        {
            if (_timer.IsEnabled) _timer.Stop();
            if (Dispatcher.CheckAccess()) Close(); else Dispatcher.Invoke(Close);
            GC.SuppressFinalize(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

