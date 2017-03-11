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

        private string _title;
        public string title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        private string _description;
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged();
            }
        }

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

        private DateTime _date;
        public DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                NotifyPropertyChanged();
            }
        }

        public TodoItem(string title, string description, DateTime date)
        {
            this._id = Guid.NewGuid().ToString();
            this._title = title;
            this._description = description;
            this._date = date;
            this._completed = false;
        }

        public TodoItem()
        {
            this._id = Guid.NewGuid().ToString();
            this._title = "";
            this._description = "";
            this._date = DateTime.Today;
            this._completed = false;
        }
    }
}
