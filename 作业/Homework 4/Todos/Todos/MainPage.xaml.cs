using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Windows.UI.Popups;
using Windows.Storage;
using Newtonsoft.Json;
using Todos.ViewModels;


//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            var viewTitleBar = Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().TitleBar;
            viewTitleBar.BackgroundColor = Windows.UI.Colors.CornflowerBlue;
            viewTitleBar.ButtonBackgroundColor = Windows.UI.Colors.CornflowerBlue;
        }

        //protected override void OnNavigatedTo(NavigationEventArgs e)
        //{
        //    if (e.Parameter.GetType() == typeof(TodoItemViewModels.TodoItemTodoItemViewModel))
        //    {
        //        this.TodoItemViewModel = (TodoItemViewModels.TodoItemTodoItemViewModel)(e.Parameter);
        //    }
        //}

        ObservableCollection<Models.TodoItem> AllItems = TodoItemViewModel.AllItems;

        private void TodosListView_ItemClicked(object sender, ItemClickEventArgs e)
        {
            TodoItemViewModel.SelectedItem = (Models.TodoItem)(e.ClickedItem);
            Frame.Navigate(typeof(NewPage));
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            TodoItemViewModel.SelectedItem = null;
            Frame.Navigate(typeof(NewPage));
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            var i = new MessageDialog("");

            if (Title.Text == "")
            {
                i.Content = "请填写标题！";
                i.ShowAsync();
            }

            else if (Details.Text == "")
            {
                i.Content = "请填写计划细节！";
                i.ShowAsync();
            }

            else if (Date.Date < DateTime.Now.Date)
            {
                i.Content = "时间设置不正确！";
                i.ShowAsync();
            }

            else {
                TodoItemViewModel.AddTodoItem(Title.Text, Details.Text, Date.Date.Date);
                Title.Text = "";
                Details.Text = "";
                Date.Date = DateTime.Now.Date;
                TodoItemViewModel.SelectedItem = null;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "";
            Details.Text = "";
            Date.Date = DateTime.Now.Date;
        }

    }
}
