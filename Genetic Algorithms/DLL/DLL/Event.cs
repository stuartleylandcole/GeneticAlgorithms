using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithms
{
  public class Event
  {
    private string course;
    private string activity;
    private int duration;
    private bool weekly;
    private List<string> groups;
    private string roomSize;

    public Event()
    {
      this.course = "";
      this.activity = "";
      this.duration = 0;
      this.weekly = false;
      this.groups = new List<string>();
      this.roomSize = "";
    }

    public Event(string course, string activity, int duration, bool weekly, List<string> groups, string roomSize)
    {
      this.course = course;
      this.activity = activity;
      this.duration = duration;
      this.weekly = weekly;
      this.groups = groups;
      this.roomSize = roomSize;
    } // constructor
    
    public string getCourse()
    {
      return course;
    } // getCourse

    public string getActivity()
    {
      return activity;
    } // getActivity

    public int getDuration()
    {
      return duration;
    } // getDuration

    public bool getWeekly()
    {
      return weekly;
    } // getWeekly

    public List<string> getGroups()
    {
      return groups;
    } // getGroups

    public string getRoomSize()
    {
      return roomSize;
    }

    public void setCourse(string newCourse)
    {
      course = newCourse;
    }

    public void setActivity(string newActivity)
    {
      activity = newActivity;
    }

    public void setDuration(int newDuration)
    {
      duration = newDuration;
    }

    public void setWeekly(bool newWeekly)
    {
      weekly = newWeekly;
    }

    public void setGroups(List<string> newGroups)
    {
      groups = newGroups;
    }

    public void setRoomSize(string newRoomSize)
    {
      roomSize = newRoomSize;
    }

    public void addGroups(List<string> additionalGroups)
    {
      foreach (string ag in additionalGroups)
        groups.Add(ag);
    }

    public override string ToString()
    {
      string groupsText = "";
      foreach (string s in groups)
        groupsText += s + " ";
      return course + " (" + activity + ") - " + groupsText;
    } // tostring
  } // event
}
