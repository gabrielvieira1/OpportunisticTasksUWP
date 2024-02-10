using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    public MainPage()
    {
      this.InitializeComponent();
    }

    ApplicationTrigger _AppTrigger;
    protected async override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);

      var taskRegistered = false;
      var exampleTaskName = "ExampleBackgroundTask2";

      foreach (var task in BackgroundTaskRegistration.AllTasks)
      {
        if (task.Value.Name == exampleTaskName)
        {
          taskRegistered = true;
          _AppTrigger = (task.Value as BackgroundTaskRegistration).Trigger as ApplicationTrigger;
          break;
        }
      }

      await BackgroundExecutionManager.RequestAccessAsync();

      if (taskRegistered == false)
      {
        await BackgroundExecutionManager.RequestAccessAsync();

        var Builder = new BackgroundTaskBuilder();
        Builder.Name = exampleTaskName;
        _AppTrigger = new ApplicationTrigger();
        Builder.SetTrigger(_AppTrigger);
        var task = Builder.Register();
      }
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      ApplicationTriggerResult result = await _AppTrigger.RequestAsync();
      //var result = _AppTrigger.RequestAsync();
    }
  }
}
