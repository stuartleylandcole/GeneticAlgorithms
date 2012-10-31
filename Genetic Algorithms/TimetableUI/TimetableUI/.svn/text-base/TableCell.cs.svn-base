using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimetableUI
{
  class TableCell
  {
    private string day;
    private string time;
    private List<TCEvents> genes;
    public TableCell(string day, string time, List<TCEvents> genes)
    {
      this.day = day;
      this.time = time;
      this.genes = genes;
    } // constructor

    public TableCell()
    {
      this.day = "";
      this.time = "";
      this.genes = new List<TCEvents>();
    }

    public string _day
    {
      get
      {
        return day;
      }
      set
      {
        day = value;
      }
    } // day

    public string _time
    {
      get
      {
        return time;
      }
      set
      {
        time = value;
      }
    } // time

    public List<TCEvents> _genes
    {
      get
      {
        return genes;
      }
    }

    public void addGenes(TCEvents g)
    {
      genes.Add(g);
    }

    public override string ToString()
    {
      string text = "";
      foreach (TCEvents g in genes)
      {
        if (g._courseCode != "")
        {
          if (text != "")
            text += "\n";
          text += g.ToString();
        }
      }
      return text;
    }
  }
}
