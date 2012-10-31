using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithms
{
  public class Slot
  {
    //private string day;
    private int day;
    private int time;
    private int painValue;
    private string week;

    public Slot()
    {
      this.day = 0;
      this.time = 0;
      this.painValue = 0;
      this.week = "";
    }

    public Slot(int day, int time, string week, int painValue)
    {
      this.day = day;
      this.time = time;
      this.week = week;
      this.painValue = painValue;
    } // constructor

    public string getDayName()
    {
      string dayName = "";
      switch (day)
      {
        case 1:
          dayName = "Monday";
          break;
        case 2:
          dayName = "Tuesday";
          break;
        case 3:
          dayName = "Wednesday";
          break;
        case 4:
          dayName = "Thursday";
          break;
        case 5:
          dayName = "Friday";
          break;
      }
      return dayName;
    } // getDayName

    public int getDay()
    {
      return day;
    } // getDay

    public int getTime()
    {
      return time;
    } // getTime

    public string getWeek()
    {
      return week;
    } // getWeek

    public int getPainValue()
    {
      return painValue;
    } // getPainValue

    public void setDay(int newDay)
    {
      day = newDay;
    }

    public void setTime(int newTime)
    {
      time = newTime;
    }

    public void setPainValue(int newPainValue)
    {
      painValue = newPainValue;
    }

    public void setWeek(string newWeek)
    {
      week = newWeek;
    }

    public override string ToString()
    {
      return getDayName() + ", " + week + " " + time + " (" + painValue + ")";
    } // toString
  } // slot
}
