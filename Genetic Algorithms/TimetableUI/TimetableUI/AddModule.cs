using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimetableUI
{
  public partial class AddModule : Form
  {
    public AddModule()
    {
      InitializeComponent();
    }

    private void btnAddEvent_Click(object sender, EventArgs e)
    {
      bool complete = true;
      if (tbCourseCode.Text == String.Empty)
      {
        complete = false;
        lblCourseCode.ForeColor = System.Drawing.Color.Red;
      }
      else
      {
        complete = true;
        lblCourseCode.ForeColor = System.Drawing.Color.Black;
      }

      if (tbCourseTitle.Text == String.Empty)
      {
        complete = false;
        lblCourseTitle.ForeColor = System.Drawing.Color.Red;
      }
      else
      {
        complete = true;
        lblCourseTitle.ForeColor = System.Drawing.Color.Black;
      }

      if (complete)
      {
        Main.courses.Add(tbCourseCode.Text + " - " + tbCourseTitle.Text);
        this.Close();
        Main.mf.refreshModules();
      }
    }
  }
}
