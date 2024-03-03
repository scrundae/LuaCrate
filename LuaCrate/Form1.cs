//Today I found out I really like spring water

using System;
using System.Deployment.Internal;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using NLua;

namespace LuaCrate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            makeNewTab();
        }

        private void makeNewTab()
        {
            TabPage newPage = new TabPage("New Tab");
            newPage.ImageIndex = 1;
            RichTextBox rtf = new RichTextBox();
            rtf.Dock = DockStyle.Fill;
            rtf.AcceptsTab = true;
            rtf.Text = "print(\"Hello, world!\")";
            Font font = new System.Drawing.Font("Lucida Console", 12, FontStyle.Regular);
            //This stupid font has been screwing me over for like an hour because it keeps resetting to Segoe UI for some stupid reason.
            rtf.TextChanged += TextChanged;
            rtf.SelectionChanged += TextChanged;
            newPage.Controls.Add(rtf);
            tabControl1.TabPages.Add(newPage);
            tabControl1.SelectedTab = newPage;
            rtf.Font = font;
        }

        private void renameActiveTab()
        {
            string newName = Interaction.InputBox("Enter new name:", "Rename File");
            tabControl1.SelectedTab.Text = newName;
        }

        private void TextChanged(object sender, EventArgs e)
        {   
            //THIS. ABSOLUTE. BANDAID. FIX. WAS THE ONLY THING THAT ACTUALLY WOULD CHANGE THE FONT TO LUCIDA CONSOLE!
            RichTextBox rtf = sender as RichTextBox;
            rtf.Font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Regular);
            rtf.Font = new System.Drawing.Font("Lucida Console", 12, FontStyle.Regular);
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            makeNewTab();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            renameActiveTab();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RichTextBox rtf = null;

            foreach (Control rtfa in tabControl1.SelectedTab.Controls)
            {
                if (rtfa is RichTextBox rtfb)
                {
                    rtf = rtfb;
                }
            }

            if (rtf != null)
            {
                toolStripStatusLabel1.Text = "Lines: " + rtf.Lines.Length.ToString();
            }
            
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in tabControl1.SelectedTab.Controls)
            {
                if (ctrl is RichTextBox rtf)
                {
                    rtf.Paste();
                }
            }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in tabControl1.SelectedTab.Controls)
            {
                if (ctrl is RichTextBox rtf)
                {
                    rtf.Paste();
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            runCode();
        }

        private void runCode()
        {
            Lua lua = new Lua();
            foreach (Control ctrl in tabControl1.SelectedTab.Controls)
            {
                if (ctrl is RichTextBox rtf)
                {
                    File.WriteAllLines("temp_run", rtf.Lines);
                    lua.DoFile("temp_run");
                }
            }
        }
    }
}