using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using System.Xml;

namespace Genetic_Algorithms
{
  public class Population
  {
    public List<Chromosome> population;
    private static Random random = new Random((int)DateTime.Now.Ticks);

    public double averagePopulationFitness;
    public double standardDeviation;

    /* constants */
    private const int POPSIZE = 50;
    private const int NUMGENERATIONS = 10;
    private float MUTATION = 0.1f;
    private const double ACCEPTABLE_FITNESS = 100.0;

    public Population()
    {
      population = new List<Chromosome>();
    } // constructor


    public void createInitialPopulation()
    {
      while (population.Count < POPSIZE)
      {
        Chromosome newChromosome = new Chromosome();

        /* add any fixed events first */
        foreach (Gene g in Program.fixedEvents)
          newChromosome.add(g.getSlot(), g.getEvent(), g.getRoom(), g.getCannotChange());

        newChromosome.scheduleLunch();

        /* schedule each of the events then add the chromosome to the population */
        foreach (Event e in Program.events)
          newChromosome.scheduleEvent(e);
        population.Add(newChromosome);
      }
    } // createInitialPopulation

    /* calculate the average fitness and standard deviation for the current population */
    private void setAveFitAndStdDev()
    {
      int[] fitnessValues = new int[population.Count];
      for (int i = 0; i < population.Count; i++)
      {
        fitnessValues[i] = population[i].chromosomeFitness();
        averagePopulationFitness += fitnessValues[i];
      }
      averagePopulationFitness /= POPSIZE;

      for (int i = 0; i < fitnessValues.Length; i++)
        standardDeviation += Math.Pow(fitnessValues[i] - averagePopulationFitness, 2);
      standardDeviation /= (population.Count - 1);
    } // setAverageFitnessAndStandardDeviation

    public List<Chromosome> selection()
    {
      List<Chromosome> parents = new List<Chromosome>();
      setAveFitAndStdDev();

      /* the chance the fittest chromosome of the two selected has of being
       * included in the new population*/
      double selectionLimit = 0.75;

      while (parents.Count < POPSIZE)
      {
        /* choose two random chromosomes and determine which is the fittest */
        Chromosome chromosome1 = population[random.Next(population.Count)];
        Chromosome chromosome2 = population[random.Next(population.Count)];
        Chromosome fittestChromosome = new Chromosome();
        Chromosome leastFitChromosome = new Chromosome();

        if (chromosome1.chromosomeFitness() < chromosome2.chromosomeFitness())
        {
          fittestChromosome = chromosome1;
          leastFitChromosome = chromosome2;
        }
        else
        {
          fittestChromosome = chromosome2;
          leastFitChromosome = chromosome1;
        }

        /* if a random number is less than the selectionLimit then add the fittest
         * chromosome to the population.  otherwise add the least fittest */
        if (random.NextDouble() < selectionLimit)
          parents.Add(fittestChromosome);
        else
          parents.Add(leastFitChromosome);
      }
      return parents;
    } // selection

    

    public void crossover()
    {
      int numberOfGenerations = 0;
      setAveFitAndStdDev();
      while (numberOfGenerations < NUMGENERATIONS)
      {
        List<Chromosome> parents = selection();
        List<Chromosome> newPopulation = new List<Chromosome>();
        List<Gene> parent1;
        List<Gene> parent2;        

        Console.WriteLine("Generation " + numberOfGenerations + 
                          " average fitness = " + averagePopulationFitness);
        while (newPopulation.Count < POPSIZE)
        {
          parent1 = parents[random.Next(parents.Count)].getGenes();
          parent2 = parents[random.Next(parents.Count)].getGenes();

          Chromosome child = new Chromosome();

          /* schedule any fixed events first */
          List<Gene> fixedEvents = (from g in parent1
                                    where g.getCannotChange() == true
                                    select g).ToList();

          foreach (Gene g in fixedEvents)
            child.add(g.getSlot(), g.getEvent(), g.getRoom(), g.getCannotChange());

          
          
          /* schedule any lunch events next */
          int[] days = { 1, 2, 4, 5 };
          foreach (int day in days)
          {
            List<Gene> lunchEvents = (from g in parent1
                                      where g.getEvent().getCourse() == "Lunch" && 
                                            g.getSlot().getDay() == day
                                      select g).ToList();
            lunchEvents.AddRange(from g in parent2
                                 where g.getEvent().getCourse() == "Lunch" && 
                                       g.getSlot().getDay() == day
                                 select g);

            foreach (string grp in Program.groups)
            {
              List<Gene> slotsForGroup = (from g in lunchEvents
                                          where g.getEvent().getGroups().Contains(grp)
                                          select g).ToList();

              Gene selectedLunchSlot = slotsForGroup[0];
              /* Might have to put a check in here to ensure that not all of the groups
               * (or at least a majority) have lunch at the same time */
              if (slotsForGroup.Count > 1)
              {
                // check that there aren't any clashes (this might occur for groups with external events
                bool slotFound = false;
                int i = 0;
                while ((i < slotsForGroup.Count) && (!slotFound))
                {
                  if (!child.doClashesExist(slotsForGroup[i].getSlot(), slotsForGroup[i].getEvent(), slotsForGroup[i].getRoom()))
                  {
                    slotFound = true;
                    selectedLunchSlot = slotsForGroup[i];
                  }
                  i++;
                }
              }


              if (!child.hasLunchBeenScheduled(selectedLunchSlot.getSlot(), selectedLunchSlot.getEvent()))
              {

                child.add(Program.slots[Program.slots.IndexOf(selectedLunchSlot.getSlot())],
                          selectedLunchSlot.getEvent(),
                          selectedLunchSlot.getRoom(),
                          selectedLunchSlot.getCannotChange());

                child.add(Program.slots[Program.slots.IndexOf(child.getCorrespondingSlot(selectedLunchSlot.getSlot()))],
                          selectedLunchSlot.getEvent(),
                          selectedLunchSlot.getRoom(),
                          selectedLunchSlot.getCannotChange());
              }
            }
          }

          /* choose a random point from which to start going through the timetable */
          Slot startSlot = Program.slots[random.Next(Program.slots.Count)];
          int startPoint = Program.slots.IndexOf(startSlot);

          for (int i = startPoint; i < Program.slots.Count; i++)
          {
            Slot s = Program.slots[i];
            List<Gene> p1GenesWeekly = (from g in parent1
                                        where (g.getSlot() == s)
                                        select g).ToList();
            List<Gene> p2GenesWeekly = (from g in parent2
                                        where (g.getSlot() == s)
                                        select g).ToList();
            scheduleLists(s, p1GenesWeekly, p2GenesWeekly, child);
          }
          for (int i = 0; i < startPoint; i++)
          {
            Slot s = Program.slots[i];
            List<Gene> p1GenesWeekly = (from g in parent1
                                        where (g.getSlot() == s)
                                        select g).ToList();
            List<Gene> p2GenesWeekly = (from g in parent2
                                        where (g.getSlot() == s)
                                        select g).ToList();
            scheduleLists(s, p1GenesWeekly, p2GenesWeekly, child);
          }

          if (random.NextDouble() < MUTATION)
            child.mutate();
          newPopulation.Add(child);

        }
        population = newPopulation;
        numberOfGenerations++;
      }

      sortPopulation();
      Console.WriteLine("\n\n" + population[0]);
    } // crossover

    private void scheduleLists(Slot s, List<Gene> p1, List<Gene> p2, Chromosome child)
    {
      List<Gene> combinedList = new List<Gene>();
      combinedList.AddRange(p1.Intersect(p2));

      foreach (Gene g in combinedList)
      {
        if (p1.Contains(g))
          p1.Remove(g);
        if (p2.Contains(g))
          p2.Contains(g);
      }

      combinedList.AddRange(p1);
      combinedList.AddRange(p2);
      combinedList.RemoveAll(delegate(Gene g) { return g.getCannotChange() == true || g.getEvent().getCourse() == "Lunch"; });
      combinedList.Reverse();

      for (int i = combinedList.Count; i > 0; i--)
      {
        bool success = child.scheduleEventFromList(s, combinedList[i - 1]);
        if (!success)
          child.unscheduableEvents.Add(combinedList[i - 1].getEvent());
        combinedList.Remove(combinedList[i - 1]);
      }

      child.scheduleUnscheduableEvents(s);
    } // scheduleLists

    private void sortPopulation()
    {
      population = (from c in population
                    orderby c.chromosomeFitness()
                    select c).ToList();
    } // sortPopulation

    public void createXML()
    {
      string filename = "timetable.xml";
      XmlDocument doc = new XmlDocument();
      XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
      writer.WriteStartElement("Timetable");
      writer.Close();
      doc.Load(filename);

      // get the fittest timetable's genes
      sortPopulation();
      List<Gene> selectedTimetable = population[0].getGenes();
      //selectedTimetable.sort();

      XmlNode root = doc.DocumentElement;

      List<int> days = Program.days();
      foreach (int dayNum in days)
      {
        List<Gene> genesInDay = new List<Gene>();
        foreach (Gene g in selectedTimetable)
        {
          if (g.getSlot().getDay() == dayNum)
            genesInDay.Add(g);
        }

        XmlElement day = doc.CreateElement("Day");
        XmlElement name = doc.CreateElement("Name");
        name.InnerText = genesInDay[0].getSlot().getDayName();
        day.AppendChild(name);

        // get all the slots for this day
        List<Slot> todaysSlots = (from s in Program.slots
                                  where s.getDay() == dayNum && s.getWeek() == "A"
                                  select s).ToList();

        List<Gene> eventsAlreadyScheduled = new List<Gene>();
        foreach (Slot s in todaysSlots)
        {
          XmlElement slot = doc.CreateElement("Slot");
          XmlElement time = doc.CreateElement("Time");
          time.InnerText = Convert.ToString(s.getTime());
          slot.AppendChild(time);

          // get all events for this time
          string[] weeks = { "A+B", "A", "B" };
          
          foreach (string weekText in weeks)
          {
            List<Gene> eventsThisSlot = new List<Gene>();
            
            if (weekText == "A+B")
            {
              eventsThisSlot = (from g in selectedTimetable
                                where g.getSlot().getDay() == s.getDay() &&
                                      g.getSlot().getTime() == s.getTime() &&
                                      g.getEvent().getWeekly() == true && 
                                      !eventsAlreadyScheduled.Contains(g)
                                select g).ToList();
              eventsAlreadyScheduled.AddRange(eventsThisSlot);
              
              for (int i = eventsThisSlot.Count; i > 0; i--)
              {
                if (eventsThisSlot[i-1].getSlot().getWeek() == "B")
                  eventsThisSlot.Remove(eventsThisSlot[i - 1]);
              }

              List<Gene> lunches = (from g in eventsThisSlot
                                    where g.getEvent().getCourse() == "Lunch"
                                    select g).ToList();

              for (int i = eventsThisSlot.Count; i > 0; i--)
              {
                if (eventsThisSlot[i - 1].getEvent().getCourse() == "Lunch")
                  eventsThisSlot.Remove(eventsThisSlot[i - 1]);
              }

              List<string> extraGroups = new List<string>();
              for (int i = 0; i < lunches.Count; i++)
              {
                foreach (string grp in lunches[i].getEvent().getGroups())
                extraGroups.Add(grp);
              }

              if (lunches.Count > 0)
              {
                eventsThisSlot.Add(new Gene(s,
                                            new Event(lunches[0].getEvent().getCourse(),
                                                      lunches[0].getEvent().getActivity(),
                                                      lunches[0].getEvent().getDuration(),
                                                      lunches[0].getEvent().getWeekly(),
                                                      extraGroups,
                                                      lunches[0].getEvent().getRoomSize()),
                                            lunches[0].getRoom(),
                                            lunches[0].getCannotChange()));
              }

            }
            else
            {
              eventsThisSlot = (from g in selectedTimetable
                                where g.getSlot().getDay() == s.getDay() &&
                                      g.getSlot().getTime() == s.getTime() && 
                                      g.getSlot().getWeek() == weekText &&
                                      !eventsAlreadyScheduled.Contains(g)
                                select g).ToList();
            }

            XmlElement week = doc.CreateElement("Week");
            XmlElement weekName = doc.CreateElement("WeekName");
            weekName.InnerText = weekText;
            week.AppendChild(weekName);

            foreach (Gene g in eventsThisSlot)
            {
              
              //XmlElement week = doc.CreateElement("Week");
              //XmlElement weekName = doc.CreateElement("WeekName");
              //weekName.InnerText = weekText;
              //week.AppendChild(weekName);

              XmlElement evt = doc.CreateElement("Event");
              XmlElement course = doc.CreateElement("Course");
              course.InnerText = g.getEvent().getCourse();
              evt.AppendChild(course);
              XmlElement activity = doc.CreateElement("Activity");
              activity.InnerText = g.getEvent().getActivity();
              evt.AppendChild(activity);
              XmlElement room = doc.CreateElement("Room");
              room.InnerText = g.getRoom()._name;
              evt.AppendChild(room);
              XmlElement groups = doc.CreateElement("Groups");

              foreach (string groupName in g.getEvent().getGroups())
              {
                XmlElement group = doc.CreateElement("Group");
                group.InnerText = groupName;
                groups.AppendChild(group);
              }

              evt.AppendChild(groups);
              week.AppendChild(evt);
              //slot.AppendChild(week);
            }
            slot.AppendChild(week);
          }

          day.AppendChild(slot);
        }
        root.AppendChild(day);
      }
      doc.Save(filename);
    } // createXML

    public override string ToString()
    {
      population = (from c in population
                    orderby c.chromosomeFitness()
                    select c).ToList();
      return "Fittest timetable:\n\n" + population[0].ToString() + 
             "\n\nLeast fit timetable:\n\n" + 
             population[population.Count-1].ToString();
    }
  }
}