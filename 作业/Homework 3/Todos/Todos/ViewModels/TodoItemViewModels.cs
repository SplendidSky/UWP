using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todos.ViewModels
{
    class TodoItemViewModel
    {
        static private ObservableCollection<Models.TodoItem> allItems = new ObservableCollection<Models.TodoItem>()
        {
            new Models.TodoItem("123", "123", DateTime.Now.Date)
        };
        static public ObservableCollection<Models.TodoItem> AllItems { get { return allItems; } }

        static private Models.TodoItem selectedItem = default(Models.TodoItem);
        static public Models.TodoItem SelectedItem {
            get
            {
                if (selectedItem == null)
                    return new Models.TodoItem("", "", DateTime.Now.Date);
                return selectedItem;
            }
            set { selectedItem = value; } }

        static public void AddTodoItem(string title, string description, DateTime date)
        {
            allItems.Add(new Models.TodoItem(title, description, date));
        }

        static public void RemoveTodoItem(string id)
        {
            // DIY
            allItems.Remove(selectedItem);
            // set selectedItem to null after remove
            selectedItem = null;
        }

        public void UpdateTodoItem(string id, string title, string description, DateTime date)
        {
            // DIY
            selectedItem = allItems[int.Parse(id)];
            selectedItem.title = title;
            selectedItem.description = description;
            selectedItem.date = date;
            // set selectedItem to null after update
            selectedItem = null;
        }

        //public static explicit operator TodoItemViewModel(object v)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
