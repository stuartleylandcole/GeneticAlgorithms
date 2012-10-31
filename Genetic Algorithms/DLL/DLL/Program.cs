using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Genetic_Algorithms
{
  public class Program
  {
    public static List<Event> events;
    public static List<Slot> slots;
    public static List<string> groups;
    public static List<Room> rooms;
    public static List<Event> lunchEvents;
    public static Room lunchRoom;
    public static List<Gene> fixedEvents;

    /* get a list of the days used */
    public static List<int> days()
    {
      List<int> days = new List<int>();
      foreach (Slot s in slots)
      {
        if (!days.Contains(s.getDay()))
          days.Add(s.getDay());
      }
      return days;
    } // days

    public static void Main(string[] args)
    {
    } // main

    private static void createLunchEvents()
    {
      lunchEvents = new List<Event>();
      for (int i = 0; i < groups.Count; i++)
      {
        lunchEvents.Add(new Event("Lunch", "Lunch", 1, true, new List<string>(new string[] { groups[i] }), "Large"));
      }
    }

    public static Slot findSlot(int day, int time, string week)
    {
      List<Slot> matches = (from s in slots
                            where s.getDay() == day &&
                                  s.getTime() == time &&
                                  s.getWeek() == week
                            select s).ToList();
      return matches[0];
    }

    public static void createSlots()
    {
      // pain value array
      int[] painValues = {9, 8, 7, 3, 5, 3, 2, 4, // monday
                          7, 6, 5, 0, 3, 0, 1, 2, // tuesday
                          7, 5, 0, 0, 3, 0, 0, 3, // thursday
                          8, 7, 5, 3, 4, 5, 7, 8 // friday
                         };

      //0900-1700 4x = 8 x 4 = 32 + 4 hours on Wed = 36 hours
      slots = new List<Slot>();
      string[] weeks = { "A", "B" };
      int[] fullDaysID = { 1, 2, 4, 5 };

      foreach (string week in weeks)
      {
        int j = 0;
        foreach (int dayID in fullDaysID)
        {
          for (int time = 900; time < 1700; time += 100)
          {
            slots.Add(new Slot(dayID, time, week, painValues[j]));
            j++;
          }
        }

        int[] painValuesWed = { 7, 5, 2, 0 };
        int p = 0;
        for (int i = 900; i < 1300; i += 100)
        {
          slots.Add(new Slot(3, i, week, painValuesWed[p]));
          p++;
        }
      }


      slots = (from s in slots
               orderby s.getDay(), s.getTime()
               select s).ToList();
    }

    public static void run()
    {
      createLunchEvents();
      Population population = new Population();
      population.createInitialPopulation();
      population.crossover();
      population.createXML();
    }
  } // class
} // namespace