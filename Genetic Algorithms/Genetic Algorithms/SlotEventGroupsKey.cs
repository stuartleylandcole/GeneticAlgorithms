using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithms
{
  class SlotEventGroupsKey
  {
    public Slot slot;
    public List<string> groups;

    public SlotEventGroupsKey(Slot slot, string[] groups)
    {
      this.slot = slot;
      this.groups = groups.ToList();
    } // constructor

    /*
    public override bool Equals(Object obj)
    {
      SlotEventGroupsKey other = obj as SlotEventGroupsKey;
      if ((slot == other.slot) && (groups.Intersect(other.groups).Count() == 0))
        return true;
      else
        return false;
    }

    public override int GetHashCode()
    {
      return slot.GetHashCode() ^ groups.GetHashCode();
    }
    */
  }
}
