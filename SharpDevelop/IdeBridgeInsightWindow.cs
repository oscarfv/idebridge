using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.TextEditor.Gui.InsightWindow;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.Drawing;

namespace IdeBridge
{
    public class IdeBridgeInsightWindow : InsightWindow
    {
        SharpDevBackend _backend = SharpDevBackend.Instance;

        public IdeBridgeInsightWindow(Form parentForm, TextEditorControl control) : base(parentForm, control)
        {
            TopMost = true;
        }

        protected override void SetLocation()
        {
            Point location = new Point(_backend.X, _backend.Y);

            Rectangle bounds = new Rectangle(location, drawingSize);

            var workingScreen = Screen.PrimaryScreen.WorkingArea;
            if (!workingScreen.Contains(bounds)) {
                if (bounds.Right > workingScreen.Right) {
                    bounds.X = workingScreen.Right - bounds.Width;
                }
                if (bounds.Left < workingScreen.Left) {
                    bounds.X = workingScreen.Left;
                }
                if (bounds.Top < workingScreen.Top) {
                    bounds.Y = workingScreen.Top;
                }
                if (bounds.Bottom > workingScreen.Bottom) {
                    bounds.Y = bounds.Y - bounds.Height - _backend.CharHeight;
                    if (bounds.Bottom > workingScreen.Bottom) {
                        bounds.Y = workingScreen.Bottom - bounds.Height;
                    }
                }
            }
            Bounds = bounds;
        }

        protected override void ShowCompletionWindow()
        {
            Enabled = true;
            this.Show();
        }

        public void Up()
        {
            ProcessTextAreaKey(Keys.Up);
        }

        public void Down()
        {
            ProcessTextAreaKey(Keys.Down);
        }

    }
}
