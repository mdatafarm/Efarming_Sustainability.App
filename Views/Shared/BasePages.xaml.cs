namespace Efarming_Sustainability.App.Views.Shared
{
    public partial class BasePages : ContentView
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(BasePages),
                string.Empty,
                BindingMode.OneWay);

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public BasePages()
        {
            InitializeComponent();
            BindingContext = this; 
        }

        

        

    }
}