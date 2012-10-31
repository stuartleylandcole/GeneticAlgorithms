using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Genetic_Algorithms;
using System.Xml;
using System.IO;

namespace TimetableUI
{
  public partial class Main : Form
  {
    public static Main mf;
    List<string> groups = new List<string>();
    public static List<string> courses = new List<string>();
    List<Event> events = new List<Event>();
    List<Gene> fixedEvents = new List<Gene>();
    List<Room> rooms = new List<Room>();
    public List<string> externalRooms = new List<string>();
    bool startAgain = false;

    public Main()
    {
      InitializeComponent();
      mf = this;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      bool complete = true;
      if (cbAddGroupYear.SelectedIndex == -1)
      {
        lblAddGroupYear.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddGroupYear.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (tbAddGroupName.Text == String.Empty)
      {
        lblAddGroupName.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddGroupName.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (complete)
        addGroup();
    }

    private void addGroup()
    {
      groups.Add(cbAddGroupYear.SelectedItem + tbAddGroupName.Text + " - " + tbAddGroupDescription.Text);
      refreshGroups();
    }

    private void refreshGroups()
    {
      lbYear1Groups.Items.Clear();
      lbYear2Groups.Items.Clear();
      lbYear3Groups.Items.Clear();
      lbAddEventGroups.Items.Clear();
      lbExtEventGroups.Items.Clear();

      foreach (string group in groups)
      {
        if (group.Substring(0, 1) == "1")
          lbYear1Groups.Items.Add(group);
        else if (group.Substring(0, 1) == "2")
          lbYear2Groups.Items.Add(group);
        else if (group.Substring(0, 1) == "3")
          lbYear3Groups.Items.Add(group);

        lbAddEventGroups.Items.Add(group);
        lbExtEventGroups.Items.Add(group);
      }
    }

    private void btnDeleteGroupYear1_Click(object sender, EventArgs e)
    {
      if (lbYear1Groups.SelectedIndex != -1)
        deleteGroup(lbYear1Groups.Items[lbYear1Groups.SelectedIndex].ToString());
    }

    private void btnDeleteGroupYear2_Click(object sender, EventArgs e)
    {
      if (lbYear2Groups.SelectedIndex != -1)
        deleteGroup(lbYear2Groups.Items[lbYear2Groups.SelectedIndex].ToString());
    }

    private void btnDeleteGroupYear3_Click(object sender, EventArgs e)
    {
      if (lbYear3Groups.SelectedIndex != -1)
        deleteGroup(lbYear3Groups.Items[lbYear3Groups.SelectedIndex].ToString());
    }

    private void deleteGroup(string groupToDelete)
    {
      groups = (from grp in groups
                where grp != groupToDelete
                select grp).ToList();
      refreshGroups();
    }

    private void btnEventsAddModule_Click(object sender, EventArgs e)
    {
      AddModule formAddMod = new AddModule();
      formAddMod.Show();
      refreshModules();
    }

    public void refreshModules()
    {
      cbModule.Items.Clear();
      cbCurrentEventModule.Items.Clear();
      cbExtEventModule.Items.Clear();
      foreach (string module in courses)
      {
        cbModule.Items.Add(module);
        cbCurrentEventModule.Items.Add(module);
        cbExtEventModule.Items.Add(module);
      }
    }

    private void btnAddEvent_Click(object sender, EventArgs e)
    {
      bool complete = true;

      if (cbModule.SelectedIndex == -1)
      {
        lblAddEventModule.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddEventModule.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbAddEventRoomSize.SelectedIndex == -1)
      {
        lblAddEventRoomSize.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddEventRoomSize.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbAddEventEventType.SelectedIndex == -1)
      {
        lblAddEventEventType.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddEventEventType.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (!(nmAddEventDuration.Value > 0 && nmAddEventDuration.Value < 5))
      {
        lblAddEventDuration.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddEventDuration.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (lbAddEventGroups.SelectedIndex == -1)
      {
        lblAddEventGroups.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblAddEventGroups.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (complete)
      {
        string[] parts = cbModule.Items[cbModule.SelectedIndex].ToString().Split('-');
        string courseCode = parts[0].Substring(0, parts[0].Length - 1);

        List<string> groupsForEvent = new List<string>();
        foreach (int index in lbAddEventGroups.SelectedIndices)
          groupsForEvent.Add(lbAddEventGroups.Items[index].ToString());


        events.Add(new Event(courseCode,
                             cbAddEventEventType.Items[cbAddEventEventType.SelectedIndex].ToString(),
                             (int)nmAddEventDuration.Value,
                             !chkAddEventFortnightly.Checked,
                             groupsForEvent,
                             cbAddEventRoomSize.Items[cbAddEventRoomSize.SelectedIndex].ToString()));

        refreshCurrentEvents("", "");
      }
    }

    private void refreshCurrentEvents(string year, string module)
    {
      lbEvents.Items.Clear();
      List<Event> filteredEvents = new List<Event>();
      if (year == String.Empty && module == String.Empty)
      {
        filteredEvents = (from e in events
                          select e).ToList();
      }

      foreach (Event e in filteredEvents)
        lbEvents.Items.Add(e);
    }

    private void btnAddEventClearFields_Click(object sender, EventArgs e)
    {
      cbModule.SelectedIndex = -1;
      cbAddEventRoomSize.SelectedIndex = -1;
      cbAddEventEventType.SelectedIndex = -1;
      nmAddEventDuration.Value = 1;
      chkAddEventFortnightly.Checked = false;
      lbAddEventGroups.SelectedIndex = -1;
    }

    private void btnAddExtEvent_Click(object sender, EventArgs e)
    {
      bool complete = true;
      if (cbDay.SelectedIndex == -1)
      {
        lblDay.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblDay.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbTime.SelectedIndex == -1)
      {
        lblTime.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblTime.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (!(nmExtEventDuration.Value > 0 && nmExtEventDuration.Value < 5))
      {
        lblDuration.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblDuration.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbRoom.SelectedIndex == -1)
      {
        lblRoom.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblRoom.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbRoom.SelectedIndex == -1)
      {
        lblRoom.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblRoom.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbExtEventModule.SelectedIndex == -1)
      {
        lblModule.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblModule.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbExtEventEventType.SelectedIndex == -1)
      {
        lblEventType.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblEventType.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (lbExternalEventsWeeks.SelectedIndex == -1)
      {
        lblWeeks.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblWeeks.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (lbExtEventGroups.SelectedIndex == -1)
      {
        lblExtEventGroups.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblExtEventGroups.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (complete)
      {
        string [] parts = cbExtEventModule.Items[cbExtEventModule.SelectedIndex].ToString().Split('-');
        string courseCode = parts[0].Substring(0, parts[0].Length-2);

        bool weekly = false;
        if (lbExternalEventsWeeks.SelectedIndices.Count > 0)
          weekly = true;

        List<string> groupsForThisEvent = new List<string>();
        foreach (int index in lbExtEventGroups.SelectedIndices)
          groupsForThisEvent.Add(lbExtEventGroups.Items[index].ToString());

        foreach (int index in lbExternalEventsWeeks.SelectedIndices)
        {
          fixedEvents.Add(new Gene(Genetic_Algorithms.Program.findSlot(getDayNum(cbDay.Items[cbDay.SelectedIndex].ToString()),
                                                                       Int32.Parse(cbTime.Items[cbTime.SelectedIndex].ToString()),
                                                                       lbExternalEventsWeeks.Items[index].ToString()),
                                   new Event(courseCode,
                                             cbExtEventEventType.Items[cbExtEventEventType.SelectedIndex].ToString(),
                                             (int)nmExtEventDuration.Value,
                                             weekly,
                                             groupsForThisEvent,
                                             "Large"),
                                   new Room(cbRoom.Items[cbRoom.SelectedIndex].ToString(),
                                            cbExtEventEventType.Items[cbExtEventEventType.SelectedIndex].ToString(),
                                            "Large"), true));


        }
        refreshCurrentExternalEvents();
      }
    }

    private void refreshCurrentExternalEvents()
    {
      lbCurrentExtEvents.Items.Clear();
      foreach (Gene g in fixedEvents)
        lbCurrentExtEvents.Items.Add(g);
    }

    private int getDayNum(string day)
    {
      switch (day)
      {
        case "Monday":
          return 1;
        case "Tuesday":
          return 2;
        case "Wednesday":
          return 3;
        case "Thursday":
          return 4;
        case "Friday":
          return 5;
        default:
          return -1;
      }
    }

    private void btnExtEventAddModule_Click(object sender, EventArgs e)
    {
      AddModule formAddMod = new AddModule();
      formAddMod.Show();
      refreshModules();
    }

    public void refreshExternalRooms()
    {
      cbRoom.Items.Clear();
      foreach (string rm in externalRooms)
        cbRoom.Items.Add(rm);
    }

    private void button8_Click(object sender, EventArgs e)
    {
      AddExternalRoom addExtRoom = new AddExternalRoom();
      addExtRoom.Show();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      cbDay.SelectedIndex = -1;
      cbTime.SelectedIndex = -1;
      nmExtEventDuration.Value = 1;
      cbRoom.SelectedIndex = -1;
      cbExtEventModule.SelectedIndex = -1;
      cbExtEventEventType.SelectedIndex = -1;
      lbExternalEventsWeeks.SelectedIndex = -1;
      lbExtEventGroups.SelectedIndex = -1;
    }

    private void btnAddRoom_Click(object sender, EventArgs e)
    {
      bool complete = true;

      if (tbAddRoomName.Text == String.Empty)
      {
        lblRoomName.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblRoomName.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (cbAddRoomSize.SelectedIndex == -1)
      {
        lblRoomSize.ForeColor = System.Drawing.Color.Red;
        complete = false;
      }
      else
      {
        lblRoomSize.ForeColor = System.Drawing.Color.Black;
        complete = true;
      }

      if (complete)
      {
        foreach (int index in lbAddRoomTypes.SelectedIndices)
        {
          rooms.Add(new Room(tbAddRoomName.Text,
                             lbAddRoomTypes.Items[index].ToString(),
                             cbAddRoomSize.Items[cbAddRoomSize.SelectedIndex].ToString()));
        }
        refreshRooms();
      }
    }
    private void refreshRooms()
    {
      lbRooms.Items.Clear();
      foreach (Room r in rooms)
        lbRooms.Items.Add(r);
    }

    private void btnFinish_Click(object sender, EventArgs e)
    {
      //if (startAgain)
        createXML();

      Genetic_Algorithms.Program.groups = this.groups;
      Genetic_Algorithms.Program.fixedEvents = this.fixedEvents;
      Genetic_Algorithms.Program.events = this.events;
      Genetic_Algorithms.Program.rooms = this.rooms;
      Genetic_Algorithms.Program.run();
      DisplayTimetable dt = new DisplayTimetable();
      dt.Show();
    }

    private void createXML()
    {
      File.Delete("data.xml");
      string filename = "data.xml";
      XmlDocument doc = new XmlDocument();
      XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.UTF8);
      writer.Formatting = Formatting.Indented;
      writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
      writer.WriteStartElement("Data");
      writer.Close();
      doc.Load(filename);

      XmlNode root = doc.DocumentElement;
      XmlElement groupsElement = doc.CreateElement("Groups");

      // add the groups
      foreach (string grp in groups)
      {
        string [] parts = grp.Split('-');
        string groupYear = parts[0].Substring(0, 1);
        string groupName = parts[0].Substring(1, parts[0].Length-2);
        string description = parts[1].Substring(1, parts[1].Length-1);
        
        XmlElement groupElement = doc.CreateElement("Group");

        XmlElement groupYearElement = doc.CreateElement("GroupYear");
        groupYearElement.InnerText = groupYear;
        groupElement.AppendChild(groupYearElement);

        XmlElement groupNameElement = doc.CreateElement("GroupName");
        groupNameElement.InnerText = groupName;
        groupElement.AppendChild(groupNameElement);

        XmlElement groupDescriptionElement = doc.CreateElement("GroupDescription");
        groupDescriptionElement.InnerText = description;
        groupElement.AppendChild(groupDescriptionElement);

        groupsElement.AppendChild(groupElement);
      }
      root.AppendChild(groupsElement);

      // add the events
      XmlElement eventsElement = doc.CreateElement("Events");

      foreach (Event e in events)
      {
        XmlElement eventElement = doc.CreateElement("Event");

        XmlElement courseCodeElement = doc.CreateElement("CourseCode");
        courseCodeElement.InnerText = e.getCourse();
        eventElement.AppendChild(courseCodeElement);

        XmlElement activityElement = doc.CreateElement("Activity");
        activityElement.InnerText = e.getActivity();
        eventElement.AppendChild(activityElement);

        XmlElement durationElement = doc.CreateElement("Duration");
        durationElement.InnerText = Convert.ToString(e.getDuration());
        eventElement.AppendChild(durationElement);

        XmlElement weeklyElement = doc.CreateElement("Weekly");
        weeklyElement.InnerText = Convert.ToString(e.getWeekly());
        eventElement.AppendChild(weeklyElement);

        XmlElement eventGroupsElement = doc.CreateElement("EventGroups");
        foreach (string grp in e.getGroups())
        {
          XmlElement eventGroupElement = doc.CreateElement("EventGroup");
          eventGroupElement.InnerText = grp;
          eventGroupsElement.AppendChild(eventGroupElement);
        }
        eventElement.AppendChild(eventGroupsElement);

        XmlElement roomSizeElement = doc.CreateElement("EventRoomSize");
        roomSizeElement.InnerText = e.getRoomSize();
        eventElement.AppendChild(roomSizeElement);

        eventsElement.AppendChild(eventElement);
      }

      root.AppendChild(eventsElement);

      // add fixed events
      XmlElement fixedEventsElement = doc.CreateElement("FixedEvents");

      foreach (Gene g in fixedEvents)
      {
        XmlElement fixedEventElement = doc.CreateElement("FixedEvent");

        XmlElement dayElement = doc.CreateElement("FEDay");
        dayElement.InnerText = g.getSlot().getDayName();
        fixedEventElement.AppendChild(dayElement);

        XmlElement timeElement = doc.CreateElement("FETime");
        timeElement.InnerText = Convert.ToString(g.getSlot().getTime());
        fixedEventElement.AppendChild(timeElement);

        XmlElement weekElement = doc.CreateElement("FEWeek");
        weekElement.InnerText = g.getSlot().getWeek();
        fixedEventElement.AppendChild(weekElement);

        XmlElement durationElement = doc.CreateElement("FEDuration");
        durationElement.InnerText = Convert.ToString(g.getEvent().getDuration());
        fixedEventElement.AppendChild(durationElement);

        XmlElement courseCodeElement = doc.CreateElement("FECourseCode");
        courseCodeElement.InnerText = g.getEvent().getCourse();
        fixedEventElement.AppendChild(courseCodeElement);

        XmlElement activityElement = doc.CreateElement("FEActivity");
        activityElement.InnerText = g.getEvent().getActivity();
        fixedEventElement.AppendChild(activityElement);

        XmlElement feGroupsElement = doc.CreateElement("FEGroups");
        foreach (string grp in g.getEvent().getGroups())
        {
          XmlElement groupElement = doc.CreateElement("FEGroup");
          groupElement.InnerText = grp;
          feGroupsElement.AppendChild(groupElement);
        }
        fixedEventElement.AppendChild(feGroupsElement);

        XmlElement roomElement = doc.CreateElement("FERoom");
        roomElement.InnerText = g.getRoom()._name;
        fixedEventElement.AppendChild(roomElement);

        fixedEventsElement.AppendChild(fixedEventElement);
      }
      root.AppendChild(fixedEventsElement);

      // add rooms
      XmlElement roomsElement = doc.CreateElement("Rooms");

      foreach (Room r in rooms)
      {
        XmlElement roomElement = doc.CreateElement("Room");

        XmlElement roomNameElement = doc.CreateElement("RoomName");
        roomNameElement.InnerText = r._name;
        roomElement.AppendChild(roomNameElement);

        XmlElement roomSizeElement = doc.CreateElement("RoomSize");
        roomSizeElement.InnerText = r._size;
        roomElement.AppendChild(roomSizeElement);

        XmlElement roomTypeElement = doc.CreateElement("RoomType");
        roomTypeElement.InnerText = r._type;
        roomElement.AppendChild(roomTypeElement);

        roomsElement.AppendChild(roomElement);
      }
      root.AppendChild(roomsElement);

      // add the list of courses
      XmlElement coursesElement = doc.CreateElement("Courses");
      foreach (string course in courses)
      {
        XmlElement courseNameElement = doc.CreateElement("CourseName");
        courseNameElement.InnerText = course;
        coursesElement.AppendChild(courseNameElement);
      }
      root.AppendChild(coursesElement);

      // add the list of external rooms
      XmlElement externalRoomsElement = doc.CreateElement("ExternalRooms");
      foreach (string extRoom in externalRooms)
      {
        XmlElement extRoomName = doc.CreateElement("ExternalRoomName");
        extRoomName.InnerText = extRoom;
        externalRoomsElement.AppendChild(extRoomName);
      }
      root.AppendChild(externalRoomsElement);

      doc.Save(filename);
    }

    private void loadXML()
    {
      try
      {
        XmlTextReader reader = new XmlTextReader("data.xml");

        string groupRecord = "";
        Event eventToAdd = new Event();
        List<string> eventGroups = new List<string>();
        Gene fixedEventGene = new Gene();
        Room roomToAdd = new Room();
        string feDay = "";
        string feTime = "";
        while (reader.Read())
        {
          reader.MoveToContent();

          // groups
          string text = reader.Name.ToString();
          if (text == "GroupYear")
            groupRecord += reader.ReadString();
          else if (text == "GroupName")
            groupRecord += reader.ReadString();
          else if (text == "GroupDescription")
          {
            groupRecord += " - " + reader.ReadString();
            groups.Add(groupRecord);
            groupRecord = "";
          }

          // events
          else if (text == "CourseCode")
          {
            eventToAdd = new Event();
            eventToAdd.setCourse(reader.ReadString());
          }
          else if (text == "Activity")
            eventToAdd.setActivity(reader.ReadString());
          else if (text == "Duration")
            eventToAdd.setDuration(Int32.Parse(reader.ReadString()));
          else if (text == "Weekly")
          {
            eventToAdd.setWeekly(Boolean.Parse(reader.ReadString()));
            eventGroups = new List<string>();
          }
          else if (text == "EventGroup")
            eventGroups.Add(reader.ReadString());
          else if (text == "EventRoomSize")
          {
            eventToAdd.setGroups(eventGroups);
            eventToAdd.setRoomSize(reader.ReadString());
            events.Add(eventToAdd);
          }

          // fixed events
          else if (text == "FEDay")
          {
            fixedEventGene = new Gene();
            feDay = reader.ReadString();
            fixedEventGene.setCannotChange(true);
          }
          else if (text == "FETime")
            feTime = reader.ReadString();
          else if (text == "FEWeek")
          {
            fixedEventGene.setSlot(Genetic_Algorithms.Program.findSlot(getDayNum(feDay),
                                                                       Int32.Parse(feTime),
                                                                       reader.ReadString()));
          }
          else if (text == "FEDuration")
            fixedEventGene.getEvent().setDuration(Int32.Parse(reader.ReadString()));

          else if (text == "FECourseCode")
            fixedEventGene.getEvent().setCourse(reader.ReadString());
          else if (text == "FEActivity")
          {
            fixedEventGene.getEvent().setActivity(reader.ReadString());
            eventGroups = new List<string>();
          }
          else if (text == "FEGroup")
            eventGroups.Add(reader.ReadString());
          else if (text == "FERoom")
          {
            fixedEventGene.getEvent().setGroups(eventGroups);

            fixedEventGene.getRoom()._name = reader.ReadString();
            fixedEventGene.getRoom()._size = "";
            fixedEventGene.getRoom()._type = "";

            fixedEvents.Add(fixedEventGene);
          }

          // rooms
          else if (text == "RoomName")
          {
            roomToAdd = new Room();
            roomToAdd._name = reader.ReadString();
          }
          else if (text == "RoomSize")
            roomToAdd._size = reader.ReadString();
          else if (text == "RoomType")
          {
            roomToAdd._type = reader.ReadString();
            rooms.Add(roomToAdd);
          }

          // courses
          else if (text == "CourseName")
            courses.Add(reader.ReadString());

          // external rooms
          else if (text == "ExternalRoomName")
            externalRooms.Add(reader.ReadString());
        }
      }
      catch (Exception exp)
      {
        Console.WriteLine(exp);
      }
    }

    private void Main_Load(object sender, EventArgs e)
    {
      Genetic_Algorithms.Program.createSlots();

      loadXML();

      if (startAgain)
      {
        groups.Add("1W - Single Honours");
        groups.Add("1X - Single Honours");
        groups.Add("1Y - Single Honours");
        groups.Add("1Z - Single Honours");
        groups.Add("1M - Joint Honours with Maths");
        groups.Add("1A - Joint Honours with Business Management");
        string[] allGroups = new string[] { groups[0], groups[1], groups[2], groups[3], groups[4] };

        rooms = new List<Room>();
        rooms.Add(new Room("1.1", "Lecture", "Large"));
        rooms.Add(new Room("1.3", "Lecture", "Medium"));
        rooms.Add(new Room("1.4", "Lecture", "Medium"));
        rooms.Add(new Room("1.5", "Lecture", "Medium"));
        rooms.Add(new Room("3rd Year Lab", "Lab", "Large"));
        rooms.Add(new Room("Unix", "Lab", "Large"));
        rooms.Add(new Room("Eng", "Lab", "Large"));
        rooms.Add(new Room("Tutorial Room", "Tutorial", "Large"));
        rooms.Add(new Room("LF15", "Examples Class", "Large"));
        rooms.Add(new Room("1.5", "Workshop", "Large"));

        events.Add(new Event("COMP10412", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4] }), "Large"));
        events.Add(new Event("COMP10092", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10020", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10052", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10052", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10092", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10020", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10042", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4] }), "Large"));
        events.Add(new Event("COMP10900", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10092", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4], groups[5] }), "Large"));
        events.Add(new Event("COMP10042", "Lecture", 1, true, new List<string>(new string[] { groups[0], groups[1], groups[2], groups[3], groups[4] }), "Large"));

        /* ------------------------ Group W events --------------------------- */
        events.Add(new Event("COMP10052", "Lab", 2, false, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10412", "Lab", 2, false, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, false, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10900", "Lab", 1, true, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, true, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10052", "Examples Class", 1, false, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10020", "Examples Class", 1, true, new List<string>(new string[] { groups[0] }), "Large"));
        events.Add(new Event("COMP10042", "Examples Class", 1, true, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("COMP10412", "Examples Class", 1, true, new List<string>(new string[] { groups[0], groups[4] }), "Large"));
        events.Add(new Event("Tutorial", "Tutorial", 1, true, new List<string>(new string[] { groups[0] }), "Large"));

        /* ------------------------ Group X events --------------------------- */

        events.Add(new Event("COMP10412", "Lab", 2, false, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, false, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10900", "Lab", 1, true, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10052", "Lab", 2, false, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, true, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10042", "Examples Class", 1, true, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10020", "Examples Class", 1, true, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10412", "Examples Class", 1, true, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("COMP10052", "Examples Class", 1, false, new List<string>(new string[] { groups[1] }), "Large"));
        events.Add(new Event("Tutorial", "Tutorial", 1, true, new List<string>(new string[] { groups[1] }), "Large"));


        /* ------------------------ Group Y events --------------------------- */

        events.Add(new Event("COMP10092", "Lab", 2, false, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10412", "Lab", 2, false, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10052", "Lab", 2, false, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, true, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10900", "Lab", 1, true, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10020", "Examples Class", 1, true, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10052", "Examples Class", 1, false, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10042", "Examples Class", 1, true, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("COMP10412", "Examples Class", 1, true, new List<string>(new string[] { groups[2] }), "Large"));
        events.Add(new Event("Tutorial", "Tutorial", 1, true, new List<string>(new string[] { groups[2] }), "Large"));


        /* ------------------------ Group Z events --------------------------- */
        events.Add(new Event("COMP10052", "Lab", 2, false, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10412", "Lab", 2, false, new List<string>(new string[] { groups[3] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, false, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10900", "Lab", 1, true, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10092", "Lab", 2, true, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10042", "Examples Class", 1, true, new List<string>(new string[] { groups[3] }), "Large"));
        events.Add(new Event("COMP10052", "Examples Class", 1, false, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("COMP10412", "Examples Class", 1, true, new List<string>(new string[] { groups[3] }), "Large"));
        events.Add(new Event("COMP10020", "Examples Class", 1, true, new List<string>(new string[] { groups[3], groups[5] }), "Large"));
        events.Add(new Event("Tutorial", "Tutorial", 1, true, new List<string>(new string[] { groups[3] }), "Large"));

        /* ------------------------ Group A events --------------------------- */
        events.Add(new Event("Tutorial", "Tutorial", 1, true, new List<string>(new string[] { groups[5] }), "Large"));

        List<Slot> allSlotsForEvent = new List<Slot>();
        fixedEvents = new List<Gene>();

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 1 && s.getTime() == 1200; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10232", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("SCH RUTH", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 1 && s.getTime() == 1600; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10212", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("CHEM G.51", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 2 && s.getTime() == 900; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10232", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("SCH RUTH", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 3 && s.getTime() == 1200; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10212", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("SCH RUTH", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 5 && s.getTime() == 900; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10212", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("ALEX TH", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 5 && s.getTime() == 1300; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("MATH10232", "Lecture", 1, true, new List<string>(new string[] { groups[4] }), "Large"),
                                   new Room("SCH RUTH", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 2 && s.getTime() == 1000; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10252", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("Ell Wilk A3.7", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 2 && s.getTime() == 1200; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10552", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("CHAP", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 4 && s.getTime() == 1000; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10252", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("MBS EAST F20", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 4 && s.getTime() == 1200; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10552", "Workshop", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("1.5", "Workshop", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 4 && s.getTime() == 1400; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10652", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("1.1", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 4 && s.getTime() == 1500; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10252", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("MBS EAST F20", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 4 && s.getTime() == 1600; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10552", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("CHAP", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 5 && s.getTime() == 1100; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10612", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("CRAW TH.2", "Lecture", "Large"),
                                   true));
        }

        allSlotsForEvent = Genetic_Algorithms.Program.slots.FindAll(delegate(Slot s) { return s.getDay() == 5 && s.getTime() == 1200; });
        foreach (Slot s in allSlotsForEvent)
        {
          fixedEvents.Add(new Gene(s,
                                   new Event("BMAN10612", "Lecture", 1, true, new List<string>(new string[] { groups[5] }), "Large"),
                                   new Room("CRAW TH.2", "Lecture", "Large"),
                                   true));
        }

        foreach (Event evt in events)
        {
          if (!courses.Contains(evt.getCourse()))
            courses.Add(evt.getCourse());
        }
        foreach (Gene g in fixedEvents)
        {
          if (!courses.Contains(g.getEvent().getCourse()))
            courses.Add(g.getEvent().getCourse());

          if (!externalRooms.Contains(g.getRoom()._name))
            externalRooms.Add(g.getRoom()._name);
        }
      }


      refreshCurrentEvents("", "");
      refreshCurrentExternalEvents();
      refreshExternalRooms();
      refreshGroups();
      refreshModules();
      refreshRooms();
    }
  }
}
