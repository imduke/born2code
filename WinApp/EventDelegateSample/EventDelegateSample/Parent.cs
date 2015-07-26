using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventDelegateSample
{
    public class Parent
    {
        public event MyEventHandler MyEvent;
        public delegate void MyEventHandler(object obj, MyEventArgs args);
    }

    public class Child : Parent
    {

    }

    public class MyMain
    {
        
        
        public void Attach(Parent parent)
        {
            parent.MyEvent += new Parent.MyEventHandler(DoIt);

        }

        public void DoIt(object o, MyEventArgs e)
        { }
    }

    public class MyEventArgs : EventArgs
    {
        private string _param1;
        public string Param1
        {
            get { return _param1; }
            set { _param1 = value; }
        }
    }
}
