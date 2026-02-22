namespace bmicalculator;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCalculateClicked(object? sender, EventArgs e)
    {
        ErrorLabel.IsVisible = false;
        ResultFrame.IsVisible = false;

        if (!double.TryParse(WeightEntry.Text, out double weight) || weight <= 0)
        {
            ShowError("Please enter a valid weight in kg.");
            return;
        }

        if (!double.TryParse(HeightEntry.Text, out double height) || height <= 0)
        {
            ShowError("Please enter a valid height in cm.");
            return;
        }

        double heightM = height / 100.0;
        double bmi = weight / (heightM * heightM);

        var (category, color, advice) = GetCategory(bmi);

        BmiValueLabel.Text = bmi.ToString("F1");
        BmiValueLabel.TextColor = color;
        CategoryLabel.Text = category;
        CategoryLabel.TextColor = color;
        AdviceLabel.Text = advice;
        ResultFrame.IsVisible = true;
    }

    private static (string category, Color color, string advice) GetCategory(double bmi) => bmi switch
    {
        < 18.5 => ("Underweight", Color.FromArgb("#2196F3"), "Consider a balanced diet to reach a healthy weight."),
        < 25.0 => ("Normal weight", Color.FromArgb("#4CAF50"), "Great! Maintain your healthy lifestyle."),
        < 30.0 => ("Overweight", Color.FromArgb("#FF9800"), "Consider regular exercise and a balanced diet."),
        _      => ("Obese", Color.FromArgb("#F44336"), "Consult a healthcare provider for guidance.")
    };

    private void ShowError(string message)
    {
        ErrorLabel.Text = message;
        ErrorLabel.IsVisible = true;
    }
}
