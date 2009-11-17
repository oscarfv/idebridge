using System;
using System.Collections.Generic;
using System.Text;

namespace IdeBridge
{
    abstract public class Backend
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int CharHeight { get; set; }
        public int Offset { get; set; }
        public int Line = 0;
        public int Column = 0;
        public string Message = "";
        public string FileName = "";
        public string PreSelection { get; set; }
        public string Buffer { get; set; }
        virtual public Client Client { get; set; }
        public bool ErrorDetected = false;
        public bool OnlyContext = false;

        public static Backend Get { get; set; }

        public Backend()
        {
            Get = this;
        }

        abstract public void LoadSolution();
        abstract public void Test();

        abstract public void Build(string projectName);
        abstract public void ReBuild(string projectName);
        abstract public void BuildByFileName(string fileName);
        abstract public void ReBuildByFileName(string fileName);
        abstract public void GoToDefinition();
        abstract public void GoToNextLocation();
        abstract public void GoTo();

        abstract public void MonitorActivation();
        abstract public void Complete();
        abstract public void Insight();
        abstract public void Hide();
        abstract public void CleanUp();
        abstract public string Insert();
        abstract public void PageDown();
        abstract public void PageUp();
        abstract public void Down();
        abstract public void Up();
        abstract public void SetText(string text);
    }
}
