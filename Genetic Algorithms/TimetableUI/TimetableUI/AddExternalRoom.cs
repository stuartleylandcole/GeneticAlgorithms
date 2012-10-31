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
  public partial class AddExternalRoom : Form
  {
    public AddExternalRoom()
    {
      InitializeComponent();
    }

    private void btnAddEvent_Click(object sender, EventArgs e)
    {
      bool complete = true;
      if (tbRoomName.Text == String.Empty)
      {
        lblRoomName.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblRoomName.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (complete)
      {
        Main.mf.externalRooms.Add(tbRoomName.Text);
        this.Close();
        Main.mf.refreshExternalRooms();
      }
    }
  }
}
