using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.SharpDevelop.Gui;
using System.Windows.Forms;
using ICSharpCode.TextEditor;
using System.Drawing;

namespace IdeBridge
{
    public class IdeBridgeCodeCompletionWindow : CodeCompletionWindow
    {
        SharpDevBackend _backend = SharpDevBackend.Instance;

        public static new IdeBridgeCodeCompletionWindow ShowCompletionWindow(Form parent, TextEditorControl control, string fileName, ICompletionDataProvider completionDataProvider, char firstChar)
        {
            return ShowCompletionWindow(parent, control, fileName, completionDataProvider, firstChar, true, true);
        }

        public static new IdeBridgeCodeCompletionWindow ShowCompletionWindow(Form parent, TextEditorControl control, string fileName, ICompletionDataProvider completionDataProvider, char firstChar, bool showDeclarationWindow, bool fixedListViewWidth)
        {
            ICompletionData[] completionData = completionDataProvider.GenerateCompletionData(fileName, control.ActiveTextAreaControl.TextArea, firstChar);
            if (completionData == null || completionData.Length == 0) {
                return null;
            }
            IdeBridgeCodeCompletionWindow codeCompletionWindow = new IdeBridgeCodeCompletionWindow(completionDataProvider, completionData, parent, control, showDeclarationWindow, fixedListViewWidth);
            codeCompletionWindow.CloseWhenCaretAtBeginning = firstChar == '\0';
            codeCompletionWindow.ShowCompletionWindow();
            return codeCompletionWindow;
        }

        protected IdeBridgeCodeCompletionWindow(ICompletionDataProvider completionDataProvider, ICompletionData[] completionData, Form parentForm, TextEditorControl control, bool showDeclarationWindow, bool fixedListViewWidth) :             base(completionDataProvider, completionData, parentForm, control, showDeclarationWindow, fixedListViewWidth)

        {
            TopMost = true;
            declarationViewWindow.Dispose();
            declarationViewWindow = new DeclarationViewWindow(null);
            declarationViewWindow.TopMost = true;
            SetDeclarationViewLocation();
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

            if (declarationViewWindow != null) {
                declarationViewWindow.Owner = null;
                SetDeclarationViewLocation();
                declarationViewWindow.TopMost = true;
            }
        }

        protected override void ShowCompletionWindow()
        {
            Enabled = true;
            this.Show();
        }

        public string DoCompletion()
        {
            InsertSelectedItem('\0');
            return ((IdeBridgeTextAreaControl)control).Text.Substring(startOffset, control.ActiveTextAreaControl.Caret.Offset - startOffset);
        }

        public void PageDown()
        {
            codeCompletionListView.PageDown();
        }

        public void PageUp()
        {
            codeCompletionListView.PageUp();
        }

        public void Down()
        {
            codeCompletionListView.SelectNextItem();
        }

        public void Up()
        {
            codeCompletionListView.SelectPrevItem();
        }

        public void SetText(string text)
        {
            codeCompletionListView.SelectItemWithStart(text);
        }
    }
}
