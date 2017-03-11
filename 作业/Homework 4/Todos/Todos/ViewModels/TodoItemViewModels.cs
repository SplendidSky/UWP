using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;


namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        static private ObservableCollection<Models.TodoItem> allItems = new ObservableCollection<Models.TodoItem>()
        {
            //new Models.TodoItem("123", "123", DateTime.Now.Date)
        };
        static public ObservableCollection<Models.TodoItem> AllItems { get { return allItems; } }

        static public Models.TodoItem SelectedItem { get; set; }

        static public void AddTodoItem(string title, string description, DateTime date)
        {
            allItems.Add(new Models.TodoItem(title, description, date));
        }

        static public void RemoveTodoItem(string id)
        {
            // DIY
            allItems.Remove(SelectedItem);
            // set selectedItem to null after remove
            SelectedItem = null;
        }

        static public void UpdateTodoItem(string id, string title, string description, DateTime date)
        {
            SelectedItem.title = title;
            SelectedItem.description = description;
            SelectedItem.date = date;
        }
        static public Todos.Models.TodoItem getTodoItemByTitle(string title)
        {
            foreach (var item in allItems)
            {
                if (item.title == title)
                    return item;
            }
            return null;
        }

        //public static explicit operator TodoItemViewModel(object v)
        //{
        //    throw new NotImplementedException();
        //}

        static public void Save(string title, string description, DateTime date, Todos.Models.TodoItem Item)
        {
            Todos.Models.TodoItem data = new Todos.Models.TodoItem(title, description, date);
            ApplicationData.Current.RoamingSettings.Values["TheData"] = JsonConvert.SerializeObject(data);
            ApplicationData.Current.RoamingSettings.Values["title"] = Item.title;
        }

        static public void SaveAll()
        {
            ApplicationData.Current.RoamingSettings.Values["AllData"] = JsonConvert.SerializeObject(allItems);
        }

        static public Todos.Models.TodoItem Load()
        {
            if (SelectedItem == null)
            {
                Todos.Models.TodoItem Item = new Models.TodoItem();
                SelectedItem = new Todos.Models.TodoItem();
                if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("TheData"))
                {
                    Todos.Models.TodoItem data = JsonConvert.DeserializeObject<Todos.Models.TodoItem>(
                        (string)ApplicationData.Current.RoamingSettings.Values["TheData"]);
                    string title = (string)ApplicationData.Current.RoamingSettings.Values["title"];
                    SelectedItem = getTodoItemByTitle(title);
                    Item.title = SelectedItem.title;
                    Item.description = SelectedItem.description;
                    Item.date = SelectedItem.date;
                    SelectedItem.title = data.title;
                    SelectedItem.description = data.description;
                    SelectedItem.date = data.date;
                }
                return Item;
            }
            return null;
        }

        static public void LoadAll()
        {
            if (ApplicationData.Current.RoamingSettings.Values.ContainsKey("AllData"))
            {
                ObservableCollection<Models.TodoItem> data = JsonConvert.DeserializeObject<ObservableCollection<Models.TodoItem>>(
                    (string)ApplicationData.Current.RoamingSettings.Values["AllData"]);
                allItems = data;
            }
        }
    }
}
