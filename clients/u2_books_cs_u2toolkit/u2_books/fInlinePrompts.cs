using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace u2_books
{
    public partial class fInlinePrompts : Form
    {
        internal List<InlinePrompt> _prompts = new List<InlinePrompt>();

        public fInlinePrompts()
        {
            InitializeComponent();
        }

        private void fInlinePrompts_Load(object sender, EventArgs e)
        {

        }

        public bool showPrompts(ref string query)
        {
            if (parse(query) == false) {
                return false;
            }
            if (_prompts.Count == 0) {
                return true;
            }
            buildDisplay();
            if (ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                updateQuery(ref query);
                return true;
            }
            return false;
        }


        protected void buildDisplay()
        {
            int top = 0;

            for (int i = 0; i < _prompts.Count; i++) {
                InlinePrompt p = _prompts[i];
                Label l = new Label();
                l.AutoSize = true;
                l.Text = p.text;
                l.Location = new Point(10, top);
                Controls.Add(l);

                top = top + l.Height + 10;
                TextBox t = new TextBox();
                t.Size = new Size(200, 24);
                t.Location = new Point(10, top);
                top = top + 50;
                Controls.Add(t);
                p.t = t;
            }
        }

        protected bool parse(string query)
        {
            int noPrompts = 0;
            int ix = 0;
            int nix = 0;
            bool done = false;

            while (done == false){
                nix = query.IndexOf("<<", ix);
                if (nix < 0) {
                    done = true;
                } else {
                    noPrompts++;
                    InlinePrompt p = new InlinePrompt();
                    _prompts.Add(p);

                    p.startPos = nix;
                    ix = nix + 2;

                    nix = query.IndexOf(">>", ix);
                    if (nix < 0) {
                        return false;
                    }
                    p.text = query.Substring(ix, (nix - ix));
                    p.Length = (4 + nix - ix);
                    ix = nix + 2;
                }
            }
            return true;
        }

        protected void updateQuery(ref string query)
        {
            for (int i = _prompts.Count; i > 0; i--) {
                InlinePrompt p = _prompts[i - 1];
                query = query.Remove(p.startPos, p.Length);
                query = query.Insert(p.startPos, p.t.Text);
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }

    internal class InlinePrompt
    {
        
        public string text = string.Empty;
        public string conv = string.Empty;
        public int startPos = 0;
        public int Length = 0;
        public TextBox t = null;
    }

}
