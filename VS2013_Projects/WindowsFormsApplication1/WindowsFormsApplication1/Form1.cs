using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RunAsync();
        }

        private void RunAsync()
        {
            button1.Enabled = false;
            var task = Task.Run(()=>
            {
                System.Threading.Thread.Sleep(5000);
                
            });

           task.ContinueWith((t) => 
           {
               if (button1.InvokeRequired)
               {
                   button1.Invoke(new MethodInvoker (delegate{ button1.Enabled = true; }));
               }
           });
           MessageBox.Show("ffffff");
        }
    }
}
