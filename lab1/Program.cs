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
    class Factory
    {
        class Meeting
        {
            public string date;
            public string description;
            private string url;
            public User[] users;

            public Meeting(string date, string description, string url, User[] users)
            {
                this.date = date;
                this.description = description;
                this.url = url;
                this.users = users;
            }
            public void CreateJson()
            {
                string jsonDescription = JsonSerializer.Serialize(this.description);
                string jsonUrl = JsonSerializer.Serialize(this.url);
                string jsonDate = JsonSerializer.Serialize(this.date);
                Console.WriteLine(jsonUrl);
                Console.WriteLine(jsonDescription);
                Console.WriteLine(jsonDate);
            }
            public void CreateXmlFile()
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("E:\\XMLFile.xml");
                XmlElement? rootTo = xml.DocumentElement;
                XmlElement el = xml.CreateElement("meeting");
                XmlAttribute url = xml.CreateAttribute("url");
                XmlElement date = xml.CreateElement("date");
                XmlElement description = xml.CreateElement("description");
                XmlElement user = xml.CreateElement("users");
                XmlText urlText = xml.CreateTextNode(this.url);
                XmlText dateText = xml.CreateTextNode(this.date);
                XmlText descriptionText = xml.CreateTextNode(this.description);
                url.AppendChild(urlText);
                date.AppendChild(dateText);
                description.AppendChild(descriptionText);
                el.Attributes.Append(url);
                el.AppendChild(date);
                el.AppendChild(description);
                rootTo?.AppendChild(el);
                xml.Save("E:\\XMLFile.xml");
            }
        }
        class User
        {
            public int id;
            public string name;
            public string avatar;

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
        }
        static void Main(string[] args)
        {
            User user1 = new User(1, "user1", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/800px-User_icon_2.svg.png");
            User user2 = new User(2, "user2", "https://upload.wikimedia.org/wikipedia/commons/thumb/1/12/User_icon_2.svg/800px-User_icon_2.svg.png");

            User[] users = { user1, user2 };
            Meeting meet = new Meeting("2022, 10, 16", "Meet", "url-link", users);
            meet.CreateXmlFile();
            meet.CreateJson();
        }
    }
}
