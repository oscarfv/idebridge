using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpDevelop.DefaultEditor.Gui.Editor;
using ICSharpCode.TextEditor.Gui.CompletionWindow;
using ICSharpCode.SharpDevelop.Gui;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Gui.InsightWindow;

namespace IdeBridge
{
    class IdeBridgeTextAreaControl : SharpDevelopTextAreaControl
    {
        SharpDevBackend _backend;

        public IdeBridgeTextAreaControl(SharpDevBackend backend)
        {
            _backend = backend;
        }

        public override void ShowCompletionWindow(ICompletionDataProvider completionDataProvider, char ch)
        {
            codeCompletionWindow = IdeBridgeCodeCompletionWindow.ShowCompletionWindow(WorkbenchSingleton.MainForm, this, _backend.FileName, completionDataProvider, ch);
            if (codeCompletionWindow != null)
            {
                codeCompletionWindow.Closed += new EventHandler(CloseCodeCompletionWindow);
            }
        }

        public override void ShowInsightWindow(IInsightDataProvider insightDataProvider)
        {
            if (insightWindow == null || insightWindow.IsDisposed) {
                insightWindow = new IdeBridgeInsightWindow(WorkbenchSingleton.MainForm, this);
                insightWindow.Closed += new EventHandler(CloseInsightWindow);
            }
            insightWindow.AddInsightDataProvider(insightDataProvider, this.FileName);
            insightWindow.ShowInsightWindow();
        }

        public bool IsInCompletion { get { return codeCompletionWindow != null && !codeCompletionWindow.IsDisposed; } }
        public bool IsInInsight { get { return insightWindow != null && !insightWindow.IsDisposed; } }

        public bool IsCompleting { get { return IsInCompletion || IsInInsight; } }

        public string DoCompletion()
        {
            if (!IsInCompletion) return "";
            return ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).DoCompletion();
        }

        public void StopCompleting()
        {
            if (codeCompletionWindow != null) codeCompletionWindow.Close();
            if (insightWindow != null) insightWindow.Close();
        }

        public void PageDown()
        {
            if (!IsInCompletion) return;
            ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).PageDown();
        }

        public void PageUp()
        {
            if (!IsInCompletion) return;
            ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).PageUp();
        }

        public void Down()
        {
            if (IsInCompletion) ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).Down();
            if (IsInInsight) ((IdeBridgeInsightWindow)insightWindow).Down();
        }

        public void Up()
        {
            if (IsInCompletion) ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).Up();
            if (IsInInsight) ((IdeBridgeInsightWindow)insightWindow).Up();
        }

        public void SetText(string text)
        {
            if (!IsInCompletion) return;
            ((IdeBridgeCodeCompletionWindow)codeCompletionWindow).SetText(text);
        }
    }
}
