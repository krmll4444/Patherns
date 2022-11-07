using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1
{
    public class Meeting
    {
        public string date { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string file { get; set; }
        public Meeting(string date, string description, string url, string file)
        {
            this.date = date;
            this.description = description;
            this.url = url;
            this.file = file;
        }

        public Meeting()
        {

        }

        public override string ToString()
        {
            return $"{date, description, url, nameOfUsersFile}";
        }
    }
    public class UserToFile
    {
        public List<User> user = new List<User>();
        public User(){}
        public void Add(int id, string name, string img)
        {
            user.Add(new User(id, name, img));
        }

        public void CreateJson(string filename)
        {
            string json = JsonSerializer.Serialize(user);

            File.WriteAllText(filename, json);
        }
        public void CreateXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                serializer.Serialize(fs, this);
            }
        }
    }
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public string avatar { get; set; }

        public User(int id, string name, string avatar)
        {
            this.id = id;
            this.name = name;
            this.avatar = avatar;
        }
        public override string ToString()
        {
            return $"{this.id}";
        }
        public User()
        {

        }
    }
    public class MeetingToFile
    {
        public List<Meeting> meet = new List<Meeting>();

        public MeetingToFile()
        {

        }

        public void Add(string date, string description, string url , string file)
        {
            meet.Add(new Meeting(date, description, url, file));
        }

        public void CreateJson(string filename)
        {
            string json = JsonSerializer.Serialize(onlinemeeting);

            File.WriteAllText(filename, json);
        }
        public void CreateXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Meeting));
            FileStream fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (fs)
            {
                serializer.Serialize(fs, this);
            }
        }
    }
    internal class Factory
    { 
        static void Main(string[] args)
        {
            UserToFile newUser =  new UserToFile();
            newUser.Add(1, "Nastya", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/800px-User_icon_2.svg.png");
            newUser.Add(2, "Mark", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/800px-User_icon_2.svg.png");
            Console.WriteLine("type 'json' for .json file");
            Console.WriteLine("type 'xml' for .xml file");
            string input = Console.ReadLine();
            if (input == "json")
            {
                MeetingToFile meetfile = new MeetingToFile();
                meetfile.Add("02.10.2022", "some meet", "sk-sdds-dsds", "E:\\lab1\\json1.json");
                meetfile.Add("02.10.2022", "some meet2", "sk-sdds-dsds", "E:\\lab1\\json1.json");
                string file1 = @"E:\lab1\json1.json";
                string file2 = @"E:\lab1\json1.json";
                newUser.CreateJson(file2);
                meetfile.CreateJson(file1);
            }
            if(input == "xml")
            {
                MeetingToFile meetfile = new MeetingToFile();
                meetfile.Add("02.10.2022", "some meet", "sk-sdds-dsds", "E:\\lab1\\XMLFile1.xml");
                meetfile.Add("02.10.2022", "some meet2", "sk-sdds-dsds", "E:\\lab1\\XMLFile1.xml");
                string file1 = @"E:\lab1\XMLFile1.xml";
                string file2 = @"E:\lab1\XMLFile2.xml";
                newUser.CreateXML(file2);
                meetfile.CreateXML(file1);
            }
        }
    }
}