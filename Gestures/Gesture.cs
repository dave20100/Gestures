using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowsInput;
using WindowsInput.Native;

namespace Gestures
{
    class Gesture
    {
        public string code { get; set; }
        public int type { get; set; }
        public string command { get; set; }
        Action action;

        public Gesture(string code, int type, string command)
        {
            this.code = code;
            this.type = type;
            this.command = command;

            InputSimulator simulator = new InputSimulator();
            switch (type)
            {
                case 1:
                    action = () =>
                    {
                        simulator.Keyboard.TextEntry(command);
                    };
                    break;
                case 2:
                    action = () =>
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(command);
                        }
                        catch
                        {
                            MessageBox.Show("The file does not exist", "Error");
                        }
                    };
                    break;
                case 3:
                    action = () =>
                    {
                        
                        List<VirtualKeyCode> modifiers = new List<VirtualKeyCode>() { };
                        List<VirtualKeyCode> keys = new List<VirtualKeyCode>() { };
                        
                        foreach(var oneKey in command.Split(','))
                        {
                            Key key;
                            Enum.TryParse(oneKey, out key);
                            VirtualKeyCode k = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(key);
                            modifiers.Add(k);
                        }
                        simulator.Keyboard.ModifiedKeyStroke(modifiers, keys);
                    };
                    break;
            }
        }
        public void invoke()
        {
            this.action.Invoke();
        }
    }
}