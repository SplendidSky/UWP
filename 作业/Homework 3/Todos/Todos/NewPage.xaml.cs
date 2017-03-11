using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.Storage;
using System.Collections.ObjectModel;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace Todos
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class NewPage : Page
    {
        private Models.TodoItem Item = ViewModels.TodoItemViewModel.SelectedItem;

        public NewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame.CanGoBack)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void CreateOrUpdateButton_Click(object sender, RoutedEventArgs e)
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


            else
            {
                if ((string)CreateOrUpdateButton.Content == "Create")
                {
                    ViewModels.TodoItemViewModel.AddTodoItem(Title.Text, Details.Text, Date.Date.Date);
                }
                else
                {
                    ViewModels.TodoItemViewModel.SelectedItem.title = Title.Text;
                    ViewModels.TodoItemViewModel.SelectedItem.description = Details.Text;
                    ViewModels.TodoItemViewModel.SelectedItem.date = Date.Date.Date;
                }

                Frame.Navigate(typeof(MainPage));
            }


        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if ((string)CreateOrUpdateButton.Content == "Create")
            {
                Title.Text = "";
                Details.Text = "";
                Date.Date = DateTime.Now.Date;
            }
            Frame.Navigate(typeof(MainPage));
            //Image.Source = new Windows.UI.Xaml.Media.Imaging.BitmapImage(new Uri("./Assets/background.jpg"));
        }

        private async void SelectPictureButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Storage.Pickers.FileOpenPicker filePicker = new Windows.Storage.Pickers.FileOpenPicker();
            filePicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            filePicker.FileTypeFilter.Add(".jpg");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;

            StorageFile file = await filePicker.PickSingleFileAsync();
            if (file != null)
            {
                using (Windows.Storage.Streams.IRandomAccessStream fileStream =
                     await file.OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    // Set the image source to the selected bitmap.
                    Windows.UI.Xaml.Media.Imaging.BitmapImage bitmapImage =
                        new Windows.UI.Xaml.Media.Imaging.BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    Image.Source = bitmapImage;
                }

            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModels.TodoItemViewModel.RemoveTodoItem(Item.id);
            Frame.Navigate(typeof(MainPage));
        }
    }
}
