using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSPliveBot.Data;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace MSPliveBot
{
    public class Bot
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("298411969:AAG71EwpMUhMY1u1Mx-AwSnZR__BdYw97yY");

        public static void Run()
        {
            using (var db = new Context())
            {
                if (!db.Admins.Any())
                {
                    db.Admins.Add(new Admin() {UserName = "worldxaker"});
                    db.SaveChanges();
                }
            }

            var me = bot.GetMe().Result;


            bot.OnMessage += message;

            bot.StartReceiving();
            Console.Write(me.Username);

        }

        private static string state = "";

        private static void message(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            long id = message.Chat.Id;

            bool isAdmin = false;

            if (message.Text.StartsWith("/ping"))
            {
                bot.SendTextMessageAsync(id, "/pong");
            }

            using (var db = new Context())
            {
                for (int i = 0; i < db.Admins.Count();i++)
                {
                    if (db.Admins.ToArray()[i].UserName == message.Chat.Username)
                    {
                        isAdmin = true;
                    }
                }

                if (isAdmin)
                {
                    if (message.Text.StartsWith("/admins"))
                    {
                        string result="Список админов: \r\n";
                        for (int i = 0; i < db.Admins.Count();i++)
                        {
                            result += db.Admins.ToArray()[i].UserName + "\r\n";
                        }

                        bot.SendTextMessageAsync(id, result);
                    }

                    if (message.Text.StartsWith("/addAdmin"))
                    {
                        string username = message.Text.Substring(10);
                        db.Admins.Add(new Admin() {UserName = username});


                        bot.SendTextMessageAsync(id, username + " успешно добавлен");
                    }

                    if (message.Text.StartsWith("/addPost"))
                    {
                        state = "addPost";
                        bot.SendTextMessageAsync(id, "Введи пост в формате дд/мм чч:мм текс сообщения");
                    }

                    if (message.Text.StartsWith("/post"))
                    {
                        string text = message.Text.Substring(6);

                        string result = Vk.PostMessage(text);

                        bot.SendTextMessageAsync(id, text+ "\r\n" + result );
                    }

                    if (state == "addPost")
                    {
                        //string[] stringDate = message.Text.Split(new char[] {' ', '/', ':'});
                        //
                    }
                }

                if (message.Text.StartsWith("/start"))
                {

                    db.Users.Add(new User() {UserName = message.Chat.Username});

                }

                db.SaveChanges();
            }
        }
    }
}
