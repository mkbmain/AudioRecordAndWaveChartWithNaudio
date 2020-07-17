using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioPlayAroundAppOne
{
    public class RecordForm : System.Windows.Forms.Form
    {
        private Recorder _recorder;
        List<MMDevice> _mmDevices = new List<MMDevice>();
        private Button _recordButton = new Button{Text = "Record"};
        private ListBox _listView = new ListBox();


        public RecordForm()
        {
            this.Size = new Size(640,480);
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                var test = new MMDeviceEnumerator();
                var te = test.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active)[i];
                _mmDevices.Add(te);
            }
            var names = _mmDevices.Select(f => new ListViewItem(f.FriendlyName)).ToArray();
            _listView.Items.AddRange(names);
            _recordButton.Click += ButtonOnClick;

            this._listView.Dock = DockStyle.Top;
            this.Controls.Add(_listView);
            _recordButton.Top = _listView.Bottom;
            this.Controls.Add(_recordButton);
        }

        private bool Record = false;
        private int SelectedDeviceIndex = 0;

        private void ButtonOnClick(object sender, EventArgs e)
        {
            if (!Record)
            {
                _recorder = new Recorder(_listView.Items.IndexOf(_listView.SelectedItems[0]), @"d:\test\", "output.wav");
                _recorder.StartRecording();
            }
            else
            {
                _recorder.RecordEnd();
                ReadWave();
            }

            Record = !Record;
        }

        private void ReadWave()
        {
            var floats = new List<float>();
            WaveChannel32 wave = new WaveChannel32(new WaveFileReader(_recorder.GetFile()));
            byte[] buffer = new Byte[16384];
            int read = 0;

            while (wave.Position < wave.Length)
            {
                read = wave.Read(buffer, 0, 16384);
                for (int i = 0; i < read / 4; i++)
                {
                    var item = BitConverter.ToSingle(buffer, i * 4);
                    floats.Add(item);
                }
            }

            new VisuliseForm(floats.ToArray()).Show();
        }
    }
}