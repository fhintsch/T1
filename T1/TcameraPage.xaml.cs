using Microsoft.Maui;

namespace T1;

public partial class TcameraPage : ContentPage
{
    private bool lastStep;
    private View holdView;

    public int Step { get; private set; }

    public TcameraPage()
    {
        InitializeComponent();
        Step = 1;
        SetContent();
    }

    void BackButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Step--;
        SetContent();
    }

    void NextButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Step++;
        SetContent();
    }

    private void SetContent()
    {
        DeActivateView();
        View currentView = null;
        lastStep = false;
        BackButton.IsVisible = true;
        NextButton.IsVisible = true;
        switch (Step)
        {
            case 1:
                BackButton.IsVisible = false;
                Title = $"Camera Assistant View ({Step})";
                Subtitle.Text = "This is a simple Entry";
                Help.Text = "Type in some Data";
                currentView = ContentView1();
                break;
            case 2:
                Title = $"Camera Assistant View ({Step})";
                Subtitle.Text = "This is a more complex view";
                Help.Text = "Show an image or take a photo";
                currentView = ContentView2();
                break;
            case 3:
                Title = $"Camera Assistant View ({Step})";
                Subtitle.Text = "Another view with 2 Entries inside a Stack";
                Help.Text = "Type in your name for example";
                currentView = ContentView3();
                break;
            case 4:
                NextButton.IsVisible = false;
                Title = $"Camera Assistant View ({Step})";
                Subtitle.Text = "End reached";
                Help.Text = "You are at the end";
                currentView = ContentView4();
                lastStep = true;
                break;
            default:
                throw new NotSupportedException($"step {Step} not implemented");
        }
        Activate(currentView);
    }

    private View ContentView1()
    {
        var entry = new Entry
        {
            Placeholder = "Name oder Id",
            Keyboard = Keyboard.Text,
        };

        return entry;
    }

    private View ContentView2()
    {
        var view = new CameraAssistantView();
        return view;
    }

    private View ContentView3()
    {
        var stack = new VerticalStackLayout();

        var label = new Label
        {
            Text = "Enter some data",
            //TextColor = Colors.AliceBlue
        };
        var entry = new Entry
        {
            WidthRequest = 100,
            Placeholder = "not important"
        };
        stack.Children.Add(label);
        stack.Children.Add(entry);
        return stack;
    }

    private View ContentView4()
    {
        var label = new Label
        {
            Text = "Done",
            FontSize = 20,
            FontAttributes = FontAttributes.Bold
        };
        var frame = new Frame
        {
            WidthRequest = 300,
            HeightRequest = 200,
            Content = label
        };
        return frame;
    }

    private void Activate(View view)
    {
        Console.WriteLine($"ENTER {view.GetType()}");
        ContentView contentView = this.FindByName<ContentView>("Contents");
        if (view != null)
        {
            if (view.BindingContext == null)
            {
                view.BindingContext = this.BindingContext;
            }
            if (contentView != null)
            {
                Console.WriteLine($"Set Content");
                contentView.Content = view;
                if (view is ITabView)
                {
                    ((ITabView)view).ActivateAsync(true);
                }
            }
        }
        else
        {
            contentView.Content = null;
        }
    }

    private void DeActivateView()
    {
        ContentView contentView = this.FindByName<ContentView>("Contents");
        View view = contentView?.Content;
        if (view != null)
        {
            Console.WriteLine($"LEAVE {view.GetType()}");
            if (view is ITabView)
            {
                ((ITabView)view).ActivateAsync(false);
            }
            view = null;
        }
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        DeActivateView();
    }
}
