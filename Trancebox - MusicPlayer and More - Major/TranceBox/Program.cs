using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace TranceBox
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main()

        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static void SetFontAndScaling(Form form)
        {
            form.SuspendLayout();
            form.Font = new Font("Tahoma", 8.25f);
            if (form.Font.Name != "Tahoma") form.Font = new Font("Arial", 8.25f);
            form.AutoScaleMode = AutoScaleMode.Font;
            form.AutoScaleDimensions = new SizeF(6f, 13f);
            form.ResumeLayout(false);
        }
    }
}
