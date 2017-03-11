using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Todos.Models
{
    class TodoItem : INotifyPropertyChanged
    {
        private string _id;
        public string id {
            get
            {
                return _id;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public string title { get; set; }

        public string description { get; set; }

        private bool? _completed;

        public bool? completed {
            get
            {
                return this._completed;
            }
            set
            {
                this._completed = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime date { get; set; }

        public TodoItem(string title, string description, DateTime date)
        {
            this._id = Guid.NewGuid().ToString();
            this.title = title;
            this.description = description;
            this.date = date;
            this.completed = false;
        }
    }
}
