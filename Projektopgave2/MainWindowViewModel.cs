namespace HowToBind
{
    public class MainWindowViewModel : NotifyBase
    {
        public string Noget
        {
            get { return GetField<string>(); }
            set { SetField(value); }
        }

        public DelegateCommand DontPushCommand { get; }

        public MainWindowViewModel()
        {
            Noget = "Test";
            DontPushCommand = new DelegateCommand(() => Noget = "Noget andet");
        }
    }
}