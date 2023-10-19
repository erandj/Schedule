using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Schedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += TimerTick;
            LiveTime.Start();
        }
        
        private void UpdateDataGrid(object sender, RoutedEventArgs e)
        {
            // (ScheduledRoute)Aircraft, time, terminal, gate, status; (Route)flight, destination
            AirportEntities context = new AirportEntities();

            DbSet<ScheduledRoute> scheduledRoutes = context.ScheduledRoute;
            DbSet<Route> routes = context.Route;
            DbSet<Airport> airports = context.Airport;
            DbSet<ScheduledRouteStatus> statuses = context.ScheduledRouteStatus;

            var query =
                from ScheduledRoute in scheduledRoutes
                join Route in routes on ScheduledRoute.Route equals Route
                join Airport in airports on Route.destination_id equals Airport.id
                join Status in statuses on ScheduledRoute.ScheduledRouteStatus equals Status
                select new
                {
                    airline = Route.Airline.name,
                    time = ScheduledRoute.scheduled_departure_time,
                    flight = Route.flight_number_numeric,
                    destination = Airport.name,
                    status = Status.name,
                    terminal = ScheduledRoute.Terminal.ToList(),
                    gate = ScheduledRoute.Gate.ToList(),
                };

            /*
            foreach(var a in query)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", a.aircraft, a.time, a.status, a.flight, a.destination);
                foreach(var b in a.terminal)
                {
                    Console.WriteLine("------- {0}", b.number);
                }
            }
            */


            FlightTable.ItemsSource = query.ToList();
        }
        private void TimerTick(object sender, EventArgs e)
        {
            LiveTimeLabel.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            CreateSchedule window = new CreateSchedule();
            window.Show();
        }

        private void DownloadExcel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
