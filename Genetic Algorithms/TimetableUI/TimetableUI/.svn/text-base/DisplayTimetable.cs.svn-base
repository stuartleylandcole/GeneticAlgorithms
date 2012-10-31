using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace TimetableUI
{
  public partial class DisplayTimetable : Form
  {
    public DisplayTimetable()
    {
      InitializeComponent();
    }

    private void DisplayTimetable_Load(object sender, EventArgs e)
    {
      string[] days = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
      int i = 1;
      foreach (string day in days)
      {
        Label label = new Label();
        label.Text = day;
        tableLayout.Controls.Add(label, i, 0);
        i++;
      }

      int[] times = { 900, 1000, 1100, 1200, 1300, 1400, 1500, 1600 };
      i = 1;
      foreach (int time in times)
      {
        Label label = new Label();
        label.Text = Convert.ToString(time);
        tableLayout.Controls.Add(label, 0, i);
        i++;
      }


      List<TableCell> cells = new List<TableCell>();
      TableCell currentCell = new TableCell();
      TCEvents currentGene = new TCEvents();
      string currentDay = "";
      string currentTime = "";
      string currentWeek = "";
      try
      {
        XmlTextReader reader = new XmlTextReader("timetable.xml");

        while (reader.Read())
        {
          reader.MoveToContent();
          string text = reader.Name.ToString();
          if (text == "Name")
            currentDay = reader.ReadString();

          else if (text == "Time")
          {
            string tempTime = reader.ReadString();
            if (currentTime != tempTime)
            {
              if (currentTime != "")
                cells.Add(currentCell);
              currentCell = new TableCell();
              currentCell._day = currentDay;
              currentCell._time = tempTime;
            }
            currentTime = tempTime;
          }

          else if (text == "WeekName")
            currentWeek = reader.ReadString();

          else if (text == "Event")
          {
            if (currentGene._courseCode != "")
              currentCell.addGenes(currentGene);
            currentGene = new TCEvents();
            currentGene._week = currentWeek;
          }

          else if (text == "Course")
            currentGene._courseCode = reader.ReadString();

          else if (text == "Activity")
            currentGene._activity = reader.ReadString();

          else if (text == "Room")
            currentGene._room = reader.ReadString();

          else if (text == "Group")
            currentGene.addGroups(reader.ReadString());
        }

        // the last cell won't get added
        cells.Add(currentCell);
      }
      catch (Exception exp)
      {
        Console.WriteLine(exp);
      }

      foreach (TableCell tc in cells)
      {
        tableLayout.Controls.Add(populateTable(tc, tc._day, tc._time), getColNum(tc._day), getRowNum(tc._time));
      }
    }

    private TableLayoutPanel populateTable(TableCell cell, string day, string time)
    {
      List<TCEvents> genes = cell._genes;

      TableLayoutPanel tlp = new TableLayoutPanel();
      tlp.ColumnCount = 0;
      tlp.RowCount = 0;
      tlp.AutoSize = true;
      tlp.AutoScroll = false;
      tlp.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

      tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
      tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
      tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));

      int height = 12;
      int fontSize = 7;
      int i = 0;
      foreach (TCEvents g in genes)
      {
        if (g._courseCode == String.Empty)
          continue;

        tlp.RowCount += 2;

        Label week = new Label();
        week.Text = g._week;
        week.Font = new Font(week.Font.FontFamily, fontSize);
        week.Size = new System.Drawing.Size(50, height);

        Label courseCode = new Label();
        courseCode.Text = g._courseCode;
        courseCode.Font = new Font(week.Font.FontFamily, fontSize);
        courseCode.Size = new System.Drawing.Size(80, height);

        Label activity = new Label();
        activity.Text = g._activity;
        activity.Font = new Font(week.Font.FontFamily, fontSize);
        activity.Size = new System.Drawing.Size(100, height);

        Label groups = new Label();
        groups.Font = new Font(week.Font.FontFamily, fontSize);
        if (g._groups.Count > 4)
        {
          groups.Size = new System.Drawing.Size(80, height * 2);
          string text = "";
          int count = 0;
          foreach (string group in g._groups)
          {
            string[] parts = group.Split('-');
            string groupText = parts[0].Substring(1, parts[0].Length - 2);
            if (text != "")
              text += ", ";
            if ((count % 4 == 0) && (count != 0))
              text += Environment.NewLine;
            text += groupText;
            count++;
          }
          groups.Text = text;
        }
        else
        {
          groups.Size = new System.Drawing.Size(80, height);
          groups.Text = g.getGroups();
        }

        Label room = new Label();
        room.Text = g._room;
        room.Font = new Font(week.Font.FontFamily, fontSize);
        room.Size = new System.Drawing.Size(100, height);

        tlp.Controls.Add(week, 0, i);
        tlp.Controls.Add(courseCode, 1, i);
        tlp.Controls.Add(activity, 2, i);
        tlp.Controls.Add(groups, 1, i + 1);
        tlp.Controls.Add(room, 2, i + 1);

        i += 3;
      }

      for (int j = 0; j < tlp.RowCount; j++)
      {
        tlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
        tlp.RowStyles[j].SizeType = SizeType.AutoSize;
      }
      return tlp;
    } // populateTable

    private int getColNum(string day)
    {
      switch (day)
      {
        case "Monday":
          return 1;
        case "Tuesday":
          return 2;
        case "Wednesday":
          return 3;
        case "Thursday":
          return 4;
        case "Friday":
          return 5;
        default:
          return -1;
      }
    } // getColNum

    private int getRowNum(string time)
    {
      switch (time)
      {
        case "900":
          return 1;
        case "1000":
          return 2;
        case "1100":
          return 3;
        case "1200":
          return 4;
        case "1300":
          return 5;
        case "1400":
          return 6;
        case "1500":
          return 7;
        case "1600":
          return 8;
        default:
          return -1;
      }
    } // getRowNum
  }
}
