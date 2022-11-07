using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Proj7
{
    class Program
    {
        public interface IObserver
        {
            void Update(ISubject subject);
        }
        public interface ISubject
        {
            void Attach(IObserver observer, string type);
            void Detach(IObserver observer, string type);
            void Notify(string type);
        }

        public class TextNews
        {
            public string title;
            public List<string> theme;
            public string text;
            public TextNews(string title, List<string> theme, string text)
            {
                this.title = title;
                this.theme = theme;
                this.text = text;
            }
            public override string ToString()
            {
                return $"{this.title}";
            }
        }

        public class VideoNews
        {
            public string title;
            public List<string> thene;
            public string url;
            public VideoNews(string title, List<string> theme, string url)
            {
                this.title = title;
                this.theme = theme;
                this.url = url;
            }
            public override string ToString()
            {
                return $"{this.title}";
            }
        }

        public class NewsList : ISubject
        {
            public List<TextNews> text_news = new List<TextNews> { };
            public List<VideoNews> video_news = new List<VideoNews> { };

            public List<IObserver> textObservers = new List<IObserver>();
            public List<IObserver> videoObservers = new List<IObserver>();

            public void Attach(IObserver observer, string type)
            {
                if (type == "subscribe_txt")
                {
                    Console.WriteLine($"user subscribed to text news");
                    this.textObservers.Add(observer);
                }
                if (type == "subscribe_vid")
                {
                    Console.WriteLine("user subscribet to video news");
                    this.videoObservers.Add(observer);
                }
                if (type == "subscribe")
                {
                    this.textObservers.Add(observer);
                    this.videoObservers.Add(observer);
                    Console.WriteLine("User subscribed to video and text");
                }
            }

            public void Detach(IObserver observer, string type)
            {
                if (type == "unsubscribe_txt")
                {
                    this.textObservers.Remove(observer);
                    Console.WriteLine($"user unsubscribed to text news");
                }
                if (type == "unsubscribe_vid")
                {
                    this.videoObservers.Remove(observer);
                    Console.WriteLine($"user unsubscribed to video news");
                }
                if (type == "unsubscribe")
                {
                    this.textObservers.Remove(observer);
                    this.videoObservers.Remove(observer);
                    Console.WriteLine("unsubscribed from all");                    
                }
            }

            public void Notify(string type)
            {
                if (type == "notify_txt")
                {
                    Console.WriteLine("NewsFeed: Notifying text subscribers...");

                    foreach (var observer in textObservers)
                    {
                        observer.Update(this);
                    }
                }
                if (type == "notify_vid")
                {
                    Console.WriteLine("NewsFeed: Notifying video subscribers...");

                    foreach (var observer in videoObservers)
                    {
                        observer.Update(this);
                    }
                }
                if (type == "notify")
                {
                    Console.WriteLine("NewsFeed: Notifying all subscribers...");

                    foreach (var observer in textObservers.Concat(videoObservers))
                    {
                        observer.Update(this);
                    }
                }
            }

            public void AddedTextNews(TextNews news)
            {
                this.text_news.Add(news);
                Console.WriteLine(text_news.Last());
                this.Notify("notify_txt");

            }
            public void AddedVideoNews(VideoNews news)
            {
                this.video_news.Add(news);
                Console.WriteLine(video_news.Last());
                this.Notify("notify_vid");
            }
        }


        class Reader : IObserver
        {
            public string name;
            public Reader(string name)
            {
                this.name = name;
            }
            public void Update(ISubject subject)
            {
                if ((subject as NewsList).text_news.Count != 0 || (subject as NewsList).text_news.Count != 0)
                {
                    Console.WriteLine($"{name}: Reacted to the news.");
                }
            }
        }

        static void Main(string[] args)
        {
            var list = new NewsList();
            var readerA = new Reader("Micke J.");
            var readerB = new Reader("Bingo O.");
            var readerC = new Reader("Riyaz R.");
            var videoNew = new VideoNews("Fireworks thrown at emergency service workers on Bonfire Night",
                new List<string> { "CNN", "fireworks" },
                "https://www.bbc.com/news/av/uk-63535473");
            var textNew1 = new TextNews("Apple: iPhone shipments delayed over China Covid lockdown",
                new List<string> { "Apple", "Covid" , "China"},
                "Apple has warned shoppers to expect delays in receiving its products after a strict Covid lockdown forced the world's largest iPhone factory to shut.");
            var textNew2 = new TextNews("How the beloved 73-year-old Dusty Baker became the oldest ever manager to win the World Series",
                new List<string> { "Dusty Baker", "Houston Astros" },
                "Dusty Baker was sitting in the dugout, looking down and making a note when his coaching and support staff spontaneously mobbed him");

//Test

            list.Attach(readerA, "subscribe_txt"); //reader A just subscibed on text news
            list.Attach(readerB, "subscribe_txt"); 
            list.AddedTextNews(textNew1); //added text news
            list.Detach(readerB, "subscribe_txt");
            list.AddedTextNews(textNew2);
            list.Attach(readerA, "subscribe_vid");
            list.Attach(readerB, "unsubscribe_txt");//reader B unsubscribed from news
            list.Attach(readerC, "subscribe_vid");
            list.AddedVideoNews(videoNew);//add video news
            list.Detach(readerA, "subscribe_vid");
        }
    }
}