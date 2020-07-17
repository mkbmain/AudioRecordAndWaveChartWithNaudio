using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Forms;
using NAudio.Gui;
using NAudio.Mixer;

namespace AudioPlayAroundAppOne
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            RecordForm t = new RecordForm();
            Application.Run(t);
        }
    }
}