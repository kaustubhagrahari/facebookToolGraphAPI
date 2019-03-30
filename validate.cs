using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FB_WFA
{
    class validate
    {
        public static void intvalid(TextBox tx, KeyPressEventArgs e)
        {
            char i = e.KeyChar;

            if ((char.IsDigit(i) || i == 8))
            {
                if (tx.Text.Length < 10 || i == 8)
                {
                }
                else
                {
                    MessageBox.Show("Not More than 10 Digits!!!");
                    e.Handled = true;
                }
            }
            else
            {
                MessageBox.Show("You can only enter Numbers!!!");
                e.Handled = true;
            }
        }
    }
}
