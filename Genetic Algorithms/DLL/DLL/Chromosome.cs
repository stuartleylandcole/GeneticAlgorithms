using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace Genetic_Algorithms
{
  public class Chromosome
  {
    public List<Gene> chromosome;
    private static Random random = new Random((int)DateTime.Now.Ticks);
    public List<Event> unscheduableEvents = new List<Event>();

    public Chromosome()
    {
      chromosome = new List<Gene>();      
    } // constructor    

    
    public void scheduleLunch()
    {
      /* For every event in the lunch events list, for every day that ought
       * to have lunch scheduled, choose either 1200 or 1300 to allocate
       * lunch to for that particular group */
      Room lunchRoom = new Room("Lunch Room", "Lunch", "Large");
      foreach (Event e in Program.lunchEvents)
      {
        List<Slot> tempLunchSlots = new List<Slot>();

        int[] days = { 1, 2, 4, 5 };
        foreach (int day in days)
        {
          tempLunchSlots = (from s in Program.slots
                            where ((s.getTime() == 1200 ||
                                    s.getTime() == 1300) &&
                                    s.getDay() == day)
                            select s).ToList();
          
          Slot selectedSlot = tempLunchSlots[random.Next(tempLunchSlots.Count)];
          chromosome.Add(new Gene(Program.slots[Program.slots.IndexOf(selectedSlot)],
                                  e , lunchRoom, false));
          chromosome.Add(new Gene(Program.slots[Program.slots.IndexOf(getCorrespondingSlot(selectedSlot))],
                                  e, lunchRoom, false));
        }
      }
    } // scheduleLunch
    
    /* Checks for any clashes between groups and rooms for a particular slot */
    public bool doClashesExist(Slot s, Event e, Room r)
    {
      int indexOfSelectedSlot = Program.slots.IndexOf(s);
      bool clashes = false;
      int i = 0;

      // ensure there are no index out of bound errors
      if ((indexOfSelectedSlot + e.getDuration()) >= Program.slots.Count)
        clashes = true;

      /* Check that there are no clashes for any hours of the event */
      while ((i < e.getDuration()) && (!clashes))
      {
        List<Gene> eventsAtThisTime = new List<Gene>();

        foreach (Gene g in chromosome)
        {
          if (g.getSlot() == s)
            eventsAtThisTime.Add(g);
        }

        int j = 0;
        while ((j < eventsAtThisTime.Count) && (!clashes))
        {
          if ((eventsAtThisTime[j].getEvent().getGroups().Intersect(e.getGroups()).Count() > 0) ||
              (eventsAtThisTime[j].getRoom() == r && r != Program.lunchRoom))
            clashes = true;
          j++;
        }

        s = Program.slots[++indexOfSelectedSlot];
        i++;
      }
      return clashes;
    } // doClashesExist

    public List<Room> findAvailableRooms(string roomSize, string activityType)
    {
      List<Room> availableRooms = new List<Room>();
      foreach (Room r in Program.rooms)
      {
        if ((r._size == roomSize) && (r._type == activityType))
          availableRooms.Add(r);
      }
      return availableRooms;
    } // findAvailableRooms

    public void scheduleEvent(Event e)
    {
      /* Choose a random slot to attempt to schedule this event in */
      Slot selectedSlot = Program.slots[random.Next(Program.slots.Count)];
      if (e.getWeekly() && selectedSlot.getWeek() == "B")
        selectedSlot = getCorrespondingSlot(selectedSlot);

      List<Room> availableRooms = findAvailableRooms(e.getRoomSize(), e.getActivity());
      Room selectedRoom = null;

      bool scheduable = false;
      
      /* If there is enough time remaining in the day to schedule this event
       * at this slot, try to find a room that is available and does not cause
       * a clash */
      if (enoughTimeInDay(selectedSlot, e))
      {
        int i = 0;
        while ((i < availableRooms.Count) && (!scheduable))
        {
          selectedRoom = availableRooms[i];
          bool clashes = doClashesExist(selectedSlot, e, selectedRoom);
          if (!clashes && e.getWeekly())
            clashes = doClashesExist(getCorrespondingSlot(selectedSlot), e, selectedRoom);
          if (!clashes)
            scheduable = true;
          i++;
        }
      }
      
      /* if this random slot is not feasible then go from this slot + 1 to the end of the 
       * slots list trying to schedule this event in each slot */
      if (!scheduable)
      {
        int selectedSlotIndex = Program.slots.IndexOf(selectedSlot);
        int i = selectedSlotIndex + 2;
        while ((i < Program.slots.Count) && (!scheduable))
        {
          selectedSlot = Program.slots[i];
          if (enoughTimeInDay(selectedSlot, e))
          {
            int j = 0;
            while ((j < availableRooms.Count) && (!scheduable))
            {
              selectedRoom = availableRooms[j];
              bool clashes = doClashesExist(selectedSlot, e, selectedRoom);
              if (!clashes && e.getWeekly())
                clashes = doClashesExist(getCorrespondingSlot(selectedSlot), e, selectedRoom);
              if (!clashes)
                scheduable = true;
              j++;
            }
          }
          i += 2;
        }

        /* go from the beginning of the slots to the slot that was initially chosen - 1 
         * only if a slot hasn't been found */
        if (!scheduable)
        {
          i = 0;
          while ((i < selectedSlotIndex) && (!scheduable))
          {
            selectedSlot = Program.slots[i];
            if (enoughTimeInDay(selectedSlot, e))
            {
              int j = 0;
              while ((j < availableRooms.Count) && (!scheduable))
              {
                selectedRoom = availableRooms[j];
                bool clashes = doClashesExist(selectedSlot, e, selectedRoom);
                if (!clashes && e.getWeekly())
                  clashes = doClashesExist(getCorrespondingSlot(selectedSlot), e, selectedRoom);
                if (!clashes)
                  scheduable = true;
                j++;
              }
            }
            i += 2;
          }
        }
      }

      if (scheduable)
      {
        Gene aGene = new Gene(selectedSlot, e, selectedRoom, false);
        bookSlots(selectedSlot, aGene);
      }
      else
        unscheduableEvents.Add(e);
    } // scheduleEvent

    public bool scheduleEventFromList(Slot s, Gene g)
    {
      if (!hasEventBeenScheduled(g.getEvent()))
      {
        bool clashes = doClashesExist(s, g.getEvent(), g.getRoom());
        if (!clashes && g.getEvent().getWeekly())
          clashes = doClashesExist(getCorrespondingSlot(s), g.getEvent(), g.getRoom());
        if (!clashes && enoughTimeInDay(s, g.getEvent()))
        {
          bookSlots(s, g);
          return true;
        }
        else
          return false;
      }
      return true;
    } // scheduleEventFromList

    private void bookSlots(Slot s, Gene g)
    {
      if (g.getEvent().getWeekly() && s.getWeek() == "B")
        s = getCorrespondingSlot(s);

      for (int i = 0; i < g.getEvent().getDuration(); i++)
      {
        int slotIndex = Program.slots.IndexOf(s);
        slotIndex += i;
        s = Program.slots[slotIndex];
        chromosome.Add(new Gene(s, g.getEvent(), g.getRoom(), g.getCannotChange()));

        slotIndex = Program.slots.IndexOf(s);
        slotIndex++;
        if (slotIndex < Program.slots.Count)         
          s = Program.slots[slotIndex];

        if (g.getEvent().getWeekly())
          chromosome.Add(new Gene(s, g.getEvent(), g.getRoom(), g.getCannotChange()));
      }
    }

    public void scheduleUnscheduableEvents(Slot s)
    {
      Slot originalSlot = s;
      for (int i = unscheduableEvents.Count; i > 0; i--)
      {
        s = originalSlot;
        if (hasEventBeenScheduled(unscheduableEvents[i - 1]))
          unscheduableEvents.Remove(unscheduableEvents[i - 1]);
        else
        {
          if (enoughTimeInDay(s, unscheduableEvents[i - 1]))
          {
            List<Room> possibleRooms = findAvailableRooms(unscheduableEvents[i - 1].getRoomSize(),
                                                          unscheduableEvents[i - 1].getActivity());
            bool stop = false;
            int j = 0;
            while ((j < possibleRooms.Count) && (!stop))
            {
              bool clashes = doClashesExist(s, unscheduableEvents[i - 1], possibleRooms[j]);
              if (!clashes && unscheduableEvents[i-1].getWeekly())
                clashes = doClashesExist(getCorrespondingSlot(s), unscheduableEvents[i-1], possibleRooms[j]);
              if (!clashes)
              {
                stop = true;
                Gene aGene = new Gene(s, unscheduableEvents[i-1], possibleRooms[j], unscheduableEvents[i-1].getWeekly());
                bookSlots(s, aGene);
              }
              j++;
            }
          }
        }
      }
    } // scheduleUnscheduableEvents

    public Slot getCorrespondingSlot(Slot selectedSlot)
    {
      return Program.slots.Find(delegate(Slot compareSlot)
                                              {
                                                return (compareSlot.getDay() == selectedSlot.getDay() &&
                                                compareSlot.getTime() == selectedSlot.getTime() &&
                                                compareSlot.getWeek() != selectedSlot.getWeek());
                                              });
    } // getCorrespondingSlot

    public bool enoughTimeInDay(Slot s, Event e)
    {
      List<Slot> remainingSlotsInDay = new List<Slot>();
      for (int i = 0; i < Program.slots.Count; i++)
      {
        if ((Program.slots[i].getDay() == s.getDay()) && (Program.slots[i].getWeek() == s.getWeek()))
          remainingSlotsInDay.Add(Program.slots[i]);
      }

      if (((remainingSlotsInDay.IndexOf(s) + e.getDuration()) > remainingSlotsInDay.Count))
        return false;
      else
        return true;      
    } // enoughTimeInDay

    public bool hasEventBeenScheduled(Event e)
    {
      int numberTimesScheduled = chromosome.FindAll(delegate(Gene g) { return g.getEvent() == e; }).Count;
      int expectedNumberOfTimes = Program.events.FindAll(delegate(Event evt) { return evt == e; }).Count * e.getDuration();

      if (e.getWeekly())
        expectedNumberOfTimes *= 2;

      if (numberTimesScheduled == expectedNumberOfTimes)
        return true;
      else
        return false;
    } // hasEventBeenScheduled

    public bool hasLunchBeenScheduled(Slot s, Event e)
    {
      int numberTimesScheduled = (from g in chromosome
                                  where g.getEvent() == e && g.getSlot().getDay() == s.getDay()
                                  select g).Count();
      if (numberTimesScheduled == 1)
        return true;
      else
        return false;
    } // hasLunchBeenScheduled

    public int chromosomeFitness()
    {
      int totalPain = 0;
      foreach (Gene g in chromosome)
      {
        totalPain += g.getSlot().getPainValue();
      }
      return (unscheduableEvents.Count * 100) +totalPain;
    } // chromosomeFitness

    public void mutate()
    {
      int selectedGene = random.Next(chromosome.Count);

      /* the gene for mutation cannot be lunch or a fixed event */
      if (chromosome[selectedGene].getEvent().getCourse() != "Lunch" &&
          chromosome[selectedGene].getCannotChange() == false)
      {
        /* choose a random slot which we will try to move this event to */
        int selectedSlot = random.Next(Program.slots.Count);
        if (!doClashesExist(Program.slots[selectedSlot], 
                            chromosome[selectedGene].getEvent(), 
                            chromosome[selectedGene].getRoom()))
        {
          /* The event could span more than one hour so all genes need to be update */
          List<Gene> genesToChange = (from g in chromosome
                                      where g.getEvent() == chromosome[selectedGene].getEvent()
                                      select g).ToList();
          for (int i = 0; i < genesToChange.Count; i++)
            chromosome[chromosome.IndexOf(genesToChange[i])].setSlot(Program.slots[selectedSlot]);
        }
      }
    } // mutate

    /* this method is required as some places the chromosome is viewed
     * as a Chromosome instead of a List<Gene> */
    public void add(Slot s, Event e, Room r, bool cannotChange)
    {
      chromosome.Add(new Gene(s, e, r, cannotChange));
    } // add

    public List<Gene> getGenes()
    {
      return chromosome;
    } // getChromosome

    public void sort()
    {
      chromosome = (from g in chromosome
                    orderby g.getSlot().getDay(), g.getSlot().getTime()
                    select g).ToList();
    } // sort

    public override string ToString()
    {
      sort();
      string text = "";
      foreach (Gene g in chromosome)
        text += g + "\n";
      text += "\nFitness = " + chromosomeFitness() + "\n\nUnscheduled events:\n";
      foreach (Event e in unscheduableEvents)
        text += e + "\n";
      return text;
    } //ToString
  }
}