using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Program_Embedder
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void Form1_SizeChanged(object sender, EventArgs e) => Embed.ReSize(panel1);

        private void toolStripMenuItem2_Click(object sender, EventArgs e) => Embed.Close(Embed.activeProcess);

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(Embed.activeProcess).Length == 0) Embed.Open(panel1);
            else
            {
                if (MessageBox.Show("A program is already embedded. Do you want to close the current one and load another?", "Active Program", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Embed.Close(Embed.activeProcess);
                    Embed.Open(panel1);
                }
            }
        }
    }
}
