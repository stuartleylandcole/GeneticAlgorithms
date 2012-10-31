using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Genetic_Algorithms
{
  public class Room
  {
    private string name;
    private string type;
    private string size;

    public Room()
    {
      name = "";
      type = "";
      size = "";
    }

    public Room(string name, string type, string size)
    {
      this.name = name;
      this.type = type;
      this.size = size;
    } // constructor

    public string _name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
      }
    }

    public string _type
    {
      get
      {
        return type;
      }
      set
      {
        type = value;
      }
    }

    public string _size
    {
      get
      {
        return size;
      }
      set
      {
        size = value;
      }
    }

    public override string ToString()
    {
      return name;
    }
  }
}
