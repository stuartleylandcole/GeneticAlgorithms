using System;
using System.Collections.Generic;
using System.Text;

namespace Genetic_Algorithms
{
  class Gene
  {
    private Slot slot;
    private Event evt;
    private Room room;
    private bool cannotChange;

    public Gene(Slot slot, Event evt, Room room, bool cannotChange)
    {
      this.slot = slot;
      this.evt = evt;
      this.room = room;
      this.cannotChange = cannotChange;
    } // constructor

    public Slot getSlot()
    {
      return slot;
    } // getSlot

    public void setSlot(Slot newSlot)
    {
      slot = Program.slots[Program.slots.IndexOf(newSlot)];
    } // setSlot

    public Event getEvent()
    {
      return evt;
    } // getEvent

    public Room getRoom()
    {
      return room;
    } // getRoom

    public bool getCannotChange()
    {
      return cannotChange;
    } // getCannotChange

    public override string ToString()
    {
      return slot.ToString() + ": " + evt.ToString() + " - " + room.ToString();
    } // toString
  }
}
