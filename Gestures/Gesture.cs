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
        public string code;
        public int type; 
        public string command;
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

                        Key key;
                        Enum.TryParse(command, out key);
                        List<VirtualKeyCode> modifiers = new List<VirtualKeyCode>() { };
                        List<VirtualKeyCode> keys = new List<VirtualKeyCode>() { };
                        
                        VirtualKeyCode k = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(key);
                        simulator.Keyboard.ModifiedKeyStroke(modifiers, keys);
                        //simulator.Keyboard.KeyPress(k);
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
