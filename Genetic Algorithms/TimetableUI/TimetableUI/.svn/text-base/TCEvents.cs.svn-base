using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimetableUI
{
  class TCEvents
  {
    private string courseCode;
    private string activity;
    private string room;
    private List<string> groups;
    private string week;

    public TCEvents(string courseCode, string activity, string room, List<string> groups, string week)
    {
      this.courseCode = courseCode;
      this.activity = activity;
      this.room = room;
      this.groups = groups;
      this.week = week;
    } // constructor

    public TCEvents()
    {
      courseCode = "";
      activity = "";
      room = "";
      groups = new List<string>();
      week = "";
    } // constructor

    public string _courseCode
    {
      get
      {
        return courseCode;
      }
      set
      {
        courseCode = value;
      }
    }

    public string _activity
    {
      get
      {
        return activity;
      }
      set
      {
        activity = value;
      }
    }

    public string _room
    {
      get
      {
        return room;
      }
      set
      {
        room = value;
      }
    }

    public List<string> _groups
    {
      get
      {
        return groups;
      }
      set
      {
        //groups = groups;
      }
    }

    public void addGroups(string grp)
    {
      groups.Add(grp);
    }

    public string getGroups()
    {
      string groupsText = "";
      foreach (string group in groups)
      {
        if (groupsText != "")
          groupsText += ", ";

        string[] parts = group.Split('-');
        string groupName = parts[0].Substring(1, parts[0].Length - 2);
        groupsText += groupName;
      }
      return groupsText;
    }

    public string _week
    {
      get
      {
        return week;
      }
      set
      {
        week = value;
      }
    }

    public override string ToString()
    {
      string groupsText = "";
      foreach (string group in groups)
      {
        if (groupsText != "")
          groupsText += ", ";
        groupsText += group;

      }
      return week + ": " + courseCode + " (" + activity + ")" + "\n" + groupsText + "   " + room;
    }
  }
}
