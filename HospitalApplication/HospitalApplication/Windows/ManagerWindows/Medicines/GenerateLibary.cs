using HospitalApplication.Windows.SecretaryWindows;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace HospitalApplication.Windows.ManagerWindows.Medicines
{
    class GenerateLibary : PrintDialog, IPrint
    {
        //PrintView view = new PrintView();
        Window111 ww = new Window111();
        public void PrintReport()
        {
            if (ShowDialog() == true)
            {
                PrintVisual(ww.print, "report");
            }
        }
    }
}
