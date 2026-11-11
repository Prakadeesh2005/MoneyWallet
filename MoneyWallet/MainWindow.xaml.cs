using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MoneyWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly JsonFileService fileService = new JsonFileService();
        private List<Expense> expenses = new List<Expense>();
        private int loggedInUserId;
        public MainWindow(int userId,string username)
        {
            InitializeComponent();
            loggedInUserId = userId;
            lblUsername.Text = "Welcome, " + username;
            dpExpenseDate.SelectedDate = DateTime.Today;
            lblYearTitle.Text = DateTime.Today.Year + " Expense";
            LoadExpenses();
        }

        // Load expenses
        private void LoadExpenses()
        {
            expenses = fileService.Read<Expense>("expenses.json");
            List<Expense> userExpenses = expenses.Where(e => e.uid == loggedInUserId).OrderByDescending(e => e.date).ToList();
            dgExpenses.ItemsSource = userExpenses;
            UpdateSummary();
        }

        // Save expense
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string reason = txtReason.Text.Trim();
            string amountText = txtAmount.Text.Trim();

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Please enter expense reason.");
                txtReason.Focus();
            }
            else if (dpExpenseDate.SelectedDate == null)
            {
                MessageBox.Show("Please select expense date.");
            }
            else if (!decimal.TryParse(amountText,NumberStyles.Number,CultureInfo.CurrentCulture,out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount greater than zero.");
                txtAmount.Focus();
            }
            else
            {
                int newId = expenses.Count == 0 ? 1 : expenses.Max(x => x.eid) + 1;
                Expense newExpense = new Expense
                {
                    eid = newId,uid = loggedInUserId,reason = reason,date = dpExpenseDate.SelectedDate.Value,amount = amount
                };
                expenses.Add(newExpense);
                fileService.Write("expenses.json", expenses);
                MessageBox.Show("Expense saved successfully!");
                txtReason.Clear();
                txtAmount.Clear();
                dpExpenseDate.SelectedDate = DateTime.Today;
                LoadExpenses();
            }
        }
        // Update expense summary
        private void UpdateSummary()
        {
            DateTime today = DateTime.Today;
            List<Expense> userExpenses = expenses.Where(e => e.uid == loggedInUserId).ToList();
            decimal monthlyTotal = userExpenses.Where(e => e.date.Year == today.Year && e.date.Month == today.Month).Sum(e => e.amount);
            DateTime weekStart = today.AddDays(-(((int)today.DayOfWeek + 6) % 7));
            DateTime nextWeekStart = weekStart.AddDays(7);
            decimal weeklyTotal = userExpenses.Where(e => e.date >= weekStart && e.date < nextWeekStart).Sum(e => e.amount);
            decimal yearlyTotal = userExpenses.Where(e => e.date.Year == today.Year).Sum(e => e.amount);
            lblMonthly.Text = "₹ " + monthlyTotal.ToString("N2");
            lblWeekly.Text = "₹ " + weeklyTotal.ToString("N2");
            lblYearly.Text = "₹ " + yearlyTotal.ToString("N2");
        }
    }
}
        
    
