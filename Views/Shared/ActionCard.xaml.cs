using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Efarming_Sustainability.App.Views.Shared;

public partial class ActionCard : Frame
{
    public static readonly BindableProperty TextProperty =
       BindableProperty.Create(nameof(Text), typeof(string), typeof(ActionCard), propertyChanged: OnTextChanged);

    public static readonly BindableProperty ImageProperty =
        BindableProperty.Create(nameof(Image), typeof(string), typeof(ActionCard), propertyChanged: OnImageChanged);

    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ActionCard), propertyChanged: OnCommandChanged);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Image
    {
        get => (string)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public ActionCard()
    {
        InitializeComponent();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ActionCard)bindable).CardText.Text = newValue.ToString();
    }

    private static void OnImageChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ActionCard)bindable).CardImage.Source = newValue.ToString();
    }

    private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        ((ActionCard)bindable).CardButton.Command = (ICommand)newValue;
    }
}
