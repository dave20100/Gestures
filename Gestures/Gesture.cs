﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowsInput;
using WindowsInput.Native;
using System.Text.RegularExpressions;

namespace Gestures
{
    public class Gesture
    {
        enum Types
        {
            WRITE_TEXT,
            START_APP,
            SHORTCUT
        }

        public string code { get; set; }
        public int type { get; set; }
        public string command { get; set; }
        Action action;

        public Gesture(string code, int type, string command)
        {
            if (command.Equals("") || code.Equals("")){
                throw new Exception();
            }
            Regex codeRegex = new Regex(@"^(\[[0-2],[0-2]\]){2,}$");
            if(!codeRegex.IsMatch(code)){
                throw new Exception() ;
            }

            this.code = code;
            this.type = type;
            this.command = command;
            this.action = createAction(type, command);
        }

        private Action createAction(int type, string command)
        {
            InputSimulator simulator = new InputSimulator();
            switch (type)
            {
                case (int)Types.WRITE_TEXT:
                    return () => 
                    {
                        simulator.Keyboard.TextEntry(command);
                    };
                case (int)Types.START_APP:
                    return () =>
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
                case (int)Types.SHORTCUT:
                    return () =>
                    {

                        List<VirtualKeyCode> modifiers = new List<VirtualKeyCode>() { };

                        foreach (var oneKey in command.Split('+'))
                        {
                            Key key;
                            Enum.TryParse(oneKey, out key);
                            VirtualKeyCode k = (VirtualKeyCode)KeyInterop.VirtualKeyFromKey(key);
                            modifiers.Add(k);
                        }
                        simulator.Keyboard.ModifiedKeyStroke(modifiers, null);                         
                    };
                default:
                        return () => MessageBox.Show("Error");
            }
        }
        public void invoke()
        {
            this.action.Invoke();
        }
    }
}