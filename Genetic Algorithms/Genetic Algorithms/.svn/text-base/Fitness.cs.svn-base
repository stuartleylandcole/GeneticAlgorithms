using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithms
{
  class Fitness// : IComparable<Fitness>
  {
    private Chromosome chromosome;
    private int fitnessValue = 0;

    // scaling factors
    private const int CLASHES_SF = 100;


    private const int GAPS_SF = 2;
    private const int CONTINUOUS_EVENTS_SF = 2;

    /* -------------------- Methods from version 1  --------------------*/
    /*
    public Fitness(Chromosome chromosome)
    {
      this.chromosome = chromosome;
    }

    //int clashes = 0;
    public void fitness()
    {
      /* go through the list and increment the value of count
       * for the slot each event is scheduled for by 1
      int currentCount = 0;
      foreach (Gene g in chromosome)
      {
        for (int i = 0; i < g.getEvent().getGroups().Length; i++)
        {
          currentCount = g.getSlot().getCount(i);
          g.getSlot().setCount(i, ++currentCount);
        }        
      }

      int[] clashes = new int[Program.groups.Length];
      int pain = 0;
      foreach (Slot s in Program.slots)
      {
        for (int i = 0; i < s.getCountLength(); i++)
        {
          if (s.getCount(i) > 0)
          {
            pain += s.getPainValue();
            if (s.getCount(i) > 1)
              clashes[i] += s.getCount(i) - 1;
          }
          s.setCount(i, 0);
        }
      }
      int totalClashs = 0;
      foreach (int clash in clashes)
        totalClashs += clash;
      fitnessValue = (totalClashs * CLASHES_SF);// +pain;
    } // fitness

    public Chromosome getChromosome()
    {
      return chromosome;
    } // getChromosome

    public int getFitnessValue()
    {
      return fitnessValue;
    } // getFitnessValue

    public int CompareTo(Fitness o)
    {
      return this.fitnessValue - o.getFitnessValue();
    } // CompareTo

    public override String ToString()
    {
      return chromosome.GetHashCode() + " -> " + fitnessValue;
    } // ToString
    */
  }
}
