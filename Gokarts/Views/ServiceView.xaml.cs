using Gokarts.Controllers;
using Gokarts.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Gokarts.Views;

/// <summary>
/// Logika interakcji dla klasy ServiceView.xaml
/// </summary>
public partial class ServiceView : UserControl
{
    /// DO ZROBIENIA (MACIEK):
    /// 
    /// - Obsłużenie systemu, który ustawia nowego zawodnika z listy w turnieju, po zakończeniu wcześniejszego przejazdu
    /// - Jeśli problemy z widokiem/stylem, zgłoszenie do Igora problemów, ew. samodzielna poprawa jeśli błędy nie są rażące
    /// - Wyczyszczenie zbędnego kodu i sprawdzenie wcześniej zrobionych funkcji
    /// - Poprawienie wcześniej zrobionych funkcji (jeśli nie działały, lub przestały działać po zmianach)
    /// - Zrobienie testów dodawania uczestnika i puszczenie testowego przejazdu (za pomocą pliku .html i modułu odbierania danych)
    /// - Sprawdzenie czy wszystko śmiga - MUSTHAVE
    /// 

    DispatcherTimer _dispatcherTimer { get; set; } = new();
    ObservableCollection<string> _lapTimes { get; set; } = new();
    TimeSpan _elapsedTime { get; set; } = new();
    DispatcherTimer _lapTimer { get; set; } = new();
    public static StackPanel lapCounterStack { get; set; } = new();

    public string lapCounterV { get; set; } = string.Empty;
    public string playerNumberV { get; set; } = string.Empty;
    public string lapTimeV { get; set; } = string.Empty;
    public string player_teamV { get; set; } = string.Empty;
    public string player_bestTimeV { get; set; } = string.Empty;
    public string player_worstTimeV { get; set; } = string.Empty;
    public string player_speedV { get; set; } = string.Empty;
    public string player_numberV { get; set; } = string.Empty;
    public string player_nameV { get; set; } = string.Empty;
    public string lapTimeEnd { get; set; } = string.Empty;
    public string predictPlace { get; set; } = string.Empty;
    public int trt_id = 0;
    public int players_index = 0;
    Player? activePlayer;
    List<Player> startPlayer;


    public ServiceView()
    {
        InitializeComponent();
        startPlayer = new();
        if(IsFocused)
        {
            RunInterval();
        }
        else
        {
            StopInterval();
        }
    }

    public ServiceView(List<Player> startPlayers)
    {
        InitializeComponent();
        startPlayer = startPlayers;
        activePlayer = startPlayers[0];
        playerNumberV = "Numer zawodnika: " + activePlayer.Id;
        if (IsFocused)
        {
            RunInterval();
        }
        else
        {
            StopInterval();
        }
    }


    private void InitLaps()
    {
        _lapTimes = new ObservableCollection<string>();
        _lapTimer = new DispatcherTimer();
        _lapTimer.Interval = new TimeSpan(0, 0, 0, 0, 147);
    }

    private void RunInterval()
    {
        try
        {
            InitLaps();
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            _dispatcherTimer.Tick += Run;
            _dispatcherTimer.Start();
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"[ {DateTime.Now} ] : [ ERROR: {ex.Message} ]");
        }
    }

    private async void Run(object? sender, EventArgs e)
    {
        try
        {
            DataModel model = await DataGatewayController.GetAsync();
            if (model.Foto_1)
            {
                _elapsedTime = new TimeSpan(0, 0, 0);
                _lapTimer.Tick += LapTimer_Tick;
                _lapTimer.Start();
            }

            if (model.Foto_2)
            {
                var convertedTime = ParseTimeString(model.LapTime);
                TimeSpan pervious = new TimeSpan(0, 0, 0);
                TimeSpan totalTime = new TimeSpan(0, 0, 0);
                int lap = 1;
                if (model.LapCounter == model.FullLap)
                {
                    pervious = _elapsedTime;
                    totalTime = pervious;
                }

                if (lap < model.LapCounter)
                {
                    totalTime += _elapsedTime - pervious;
                    TimeSpan second = _elapsedTime - totalTime;

                    TimeSpan addingValue = second - pervious;
                    if (addingValue.Ticks < 0)
                    {
                        MakeLapStack(lap, $"{Math.Abs(_elapsedTime.Minutes).ToString("00")}:{Math.Abs(_elapsedTime.Seconds).ToString("00")}:{Math.Abs(_elapsedTime.Milliseconds).ToString("000")}", false, $"{Math.Abs(addingValue.Seconds).ToString("00")}:{Math.Abs(addingValue.Milliseconds).ToString("000")}");
                    }
                    else
                    {
                        MakeLapStack(lap, $"{Math.Abs(_elapsedTime.Minutes).ToString("00")}:{Math.Abs(_elapsedTime.Seconds).ToString("00")}:{Math.Abs(_elapsedTime.Milliseconds).ToString("000")}", true, $"{Math.Abs(addingValue.Seconds).ToString("00")}:{Math.Abs(addingValue.Milliseconds).ToString("000")}");
                    }
                    pervious = _elapsedTime;
                    lap++;
                }

                _lapTimes.Add(convertedTime);
            }

            if (model.Foto_3)
            {
                lapTimeEnd = $"Czas końcowy:       {ParseTimeString(model.LapTime)}";
                player_bestTimeV = $"Najlepszy Czas:      {ParseTimeString(model.LapTime)}";
                positionLabel.Content = $"Przewidywane Miejsce: #1";
                lapTimeV = $"{ParseTimeString(model.LapTime)}";
                _lapTimer.Stop();
                _lapTimer.Tick -= LapTimer_Tick;

            }
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"[ {DateTime.Now} ] : [ ERROR: {ex.Message} ]");
        }
    }

    private void StopInterval()
    {
        try
        {
            _dispatcherTimer.Stop();
            _dispatcherTimer.Tick -= Run;
        }
        catch (Exception ex)
        {
            Trace.WriteLine($"[ {DateTime.Now} ] : [ ERROR: {ex.Message} ]");
        }
    }

    private static void MakeLapStack(int actualLap, string content, bool isValuePositive, string value)
    {
        StackPanel stackPanel = new StackPanel();
        stackPanel.Orientation = Orientation.Horizontal;


        Label label1 = new Label();
        label1.Content = $"{actualLap}. Czas: {content} ";
        label1.Foreground = new SolidColorBrush(Color.FromRgb(246, 248, 251));
        label1.FontWeight = FontWeights.Bold;
        label1.FontSize = 18;
        label1.Width = 160;
        label1.HorizontalAlignment = HorizontalAlignment.Left;

        stackPanel.Children.Add(label1);

        Label label2 = new Label();
        if (isValuePositive)
        {
            label2.Content = $"+{value}";
            label2.Foreground = new SolidColorBrush(Color.FromRgb(210, 29, 29)); // red
        }
        else
        {
            label2.Content = $"-{value}";
            label2.Foreground = new SolidColorBrush(Color.FromRgb(103, 212, 80)); // green
        }
        if (actualLap == 1)
        {
            label2.Content = $"+{value}";
            label2.Foreground = new SolidColorBrush(Color.FromRgb(189, 189, 189)); // gray
        }
        label2.FontWeight = FontWeights.Bold;
        label2.FontSize = 18;
        label2.Width = 80;
        label2.HorizontalAlignment = HorizontalAlignment.Right;

        stackPanel.Children.Add(label2);

        lapCounterStack.Children.Add(stackPanel);

    }

    private static string ParseTimeString(string input)
    {
        //Delete "T#" from text
        string timeString = input.Substring(2);

        //Split times by '_'
        string[] parts = timeString.Split('_');

        int minutes = 0;
        int seconds = 0;
        int milliseconds = 0;

        //Loop to extract values and enter to specified variable
        foreach (string part in parts)
        {
            string unit = String.Empty;
            int value = 0;

            if (part.Contains("ms"))
            {
                unit = part.Substring(part.Length - 2, 2);
                value = int.Parse(part.Substring(0, part.Length - 2));
            }
            else
            {
                unit = part[part.Length - 1].ToString();
                value = int.Parse(part.Substring(0, part.Length - 1));
            }

            //checks wich variable should get the value
            switch (unit)
            {
                case "m":
                    minutes = value;
                    break;
                case "s":
                    seconds = value;
                    break;
                case "ms":
                    milliseconds = value;
                    break;
            }
        }

        //Create converted time
        string result = $"{minutes}:{seconds}:{milliseconds}";

        return result;
    }

    private void LapTimer_Tick(object? sender, EventArgs e)
    {
        _elapsedTime += TimeSpan.FromMilliseconds(147);
        lapTime.Content = $"{_elapsedTime.Minutes.ToString("00")}:{_elapsedTime.Seconds.ToString("00")}:{_elapsedTime.Milliseconds.ToString("000")}";
    }

    private void Cancel_Race_user_window_button_Click(object sender, RoutedEventArgs e)
    {
        _lapTimes = new ObservableCollection<string> { };

        player_speedV = "0";
        player_worstTimeV = "0";
        player_bestTimeV = "0";
        lapCounterV = "0/9";
        lapTimeV = "0";
        predictPlace = "Przewidywane miejsce:";
        lapTimeEnd = "0";

        StopInterval();
    }

    private void Disqualifies_user_window_button_Click(object sender, RoutedEventArgs e)
    {
        _lapTimes = new ObservableCollection<string> { };

        player_speedV = "0";
        player_worstTimeV = "0";
        player_bestTimeV = "0";
        lapCounterV = "0/9";
        lapTimeV = "0";

        StopInterval();
    }

    private void Penalty_user_window_button_Click(object sender, RoutedEventArgs e)
    {
        Penalty penalty = new Penalty(0, 1, "Test", 1);
        DataBaseController.InsertPenalty(penalty);
    }

    public void SelectNextPlayer()
    {
        if(startPlayer.Count < players_index + 1)
        {
            MessageBox.Show("Koniec przejazdów");
        }
        else
        {
            activePlayer = startPlayer[players_index + 1];
            player_nameV = activePlayer.FirstName + " " + activePlayer.LastName;
            player_teamV = DataBaseController.SelectTeams().First(x => x.Id == activePlayer.TeamId).Name;
            playerNumberV = players_index.ToString();
        }

    }
}
