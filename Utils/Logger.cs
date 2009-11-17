using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using EnvDTE;

namespace IdeBridge
{
    public class Logger
    {
        public static RichTextBox Output = null;
        //public static OutputWindowPane OutputPane = null;
 
        public static void Error(string message)
        {
            Info(message);
//             if (OutputPane != null) 
//             {
//                 OutputPane.OutputString(message);                
//             }
//             else
//             {

//                 MessageBox.Show(message);
//             }
        }

        public static void Debug(string message)
        {
            Info(message);
        }
        
        public static void Exception(Exception e) 
        {
            string message = "";
            if (e != null) 
            {
                message += e.Message;
                message += "\n" + e.StackTrace;
                if (e.InnerException != null) 
                {
                    message += "\n" + e.InnerException.Message;
                }
            }
            Error(message);
        }

        public static void Info(string info) 
        {
            if (Output != null) 
            {
                if (Output.InvokeRequired)
                {
                    Output.BeginInvoke((MethodInvoker)delegate()
                    {
                        Output.AppendText(info);
                        if (!info.EndsWith("\n")) Output.AppendText("\n");
                    });
                }
                else
                {
                    Output.AppendText(info);
                    if (!info.EndsWith("\n")) Output.AppendText("\n");
                }
            }
            //if (OutputPane != null) 
            //{            
            //    OutputPane.OutputString(info);
            //    if (!info.EndsWith("\n")) OutputPane.OutputString("\n");
            //}
        }
    }
}
